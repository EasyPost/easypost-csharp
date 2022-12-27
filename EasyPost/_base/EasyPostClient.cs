using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.Shared;
using EasyPost.Utilities;

namespace EasyPost._base
{
    public abstract class EasyPostClient: IDisposable
    {
        public readonly ClientConfiguration Configuration;

        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to use if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        protected EasyPostClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null)
        {
            ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            Configuration = new ClientConfiguration(apiKey, baseUrl);

            _httpClient = customHttpClient ?? new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", Configuration.UserAgent); // we set the user agent here once so it's not needlessly calculated for every request
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 0, milliseconds: Configuration.ConnectTimeoutMilliseconds); // set the default timeout for the client
        }

        /// <summary>
        ///     Execute an HTTP request.
        /// </summary>
        /// <param name="request">Request to execute</param>
        /// <returns>An HttpResponseMessage object.</returns>
        internal virtual async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request)
        {
            // This method actually executes the request and returns the response.
            // Everything up to this point has been pre-request, and everything after this point is post-request.
            // This method is "virtual" so it can be overriden (i.e. by a MockClient in testing to avoid making actual HTTP requests).
            return await _httpClient.SendAsync(request);
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize response data into. Must be subclass of EasyPostObject.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        internal async Task<T> Request<T>(Utilities.Http.Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class
        {
            // Build the request
            var headers = new Dictionary<string, string>
            {
                // User-Agent is already set as a default header for the HttpClient
                { "Authorization", $"Bearer {Configuration.ApiKey}" },
                { "Content-Type", "application/json" }
            };
            Request request = new Request(Configuration.ApiBase, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage());

            // Check the response's status code
            if (response.ReturnedError())
            {
                throw await ApiError.FromErrorResponse(response);
            }

            // Prepare the list of root elements to use during deserialization
            List<string>? rootElements = null;
            if (rootElement != null)
            {
                rootElements = new List<string> { rootElement };
            }

            // Deserialize the response into an object
            T resource = await JsonSerialization.ConvertJsonToObject<T>(response, null, rootElements);

            if (resource is null)
            {
                // Object deserialization failed
                throw new JsonDeserializationError(typeof(T));
            }

            PassClientToEasyPostObject(resource);

            return resource;
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <returns>Whether request was successful.</returns>
        internal async Task<bool> Request(Utilities.Http.Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null)
        {
            // Build the request
            var headers = new Dictionary<string, string>
            {
                // User-Agent is already set as a default header for the HttpClient
                { "Authorization", $"Bearer {Configuration.ApiKey}" },
                { "Content-Type", "application/json" }
            };
            Request request = new Request(Configuration.ApiBase, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage());

            // Return whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
            return response.ReturnedNoError();
        }

        private void PassClientToAllEasyPostObjectProperties<T>(T? resource) where T : EasyPostObject
        {
            if (resource == null)
            {
                return;
            }

            List<PropertyInfo> properties = new List<PropertyInfo>(resource.GetType().GetProperties());
            foreach (PropertyInfo property in properties)
            {
                // Pass the Client into every EasyPostObject in the collection
                PassClientToEasyPostObject(property.GetValue(resource));
            }
        }

        private void PassClientToEasyPostObjectsInList<T>(T? resource) where T : IList
        {
            if (resource == null)
            {
                return;
            }

            foreach (object? item in resource)
            {
                // pass the Client into every EasyPostObject in the list
                PassClientToEasyPostObject(item);
            }
        }

        /// <summary>
        ///     Copy this Client into a new EasyPostObject instance.
        /// </summary>
        /// <param name="resource">Object to add this Client to.</param>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <returns>Object with Client added.</returns>
        private void PassClientToEasyPostObject<T>(T? resource) where T : class
        {
            switch (resource)
            {
                case null:
                    break;
                case IList list:
                    PassClientToEasyPostObjectsInList(list);
                    break;
                case Collection collection:
                    PassClientToAllEasyPostObjectProperties(collection);
                    break;
                case EasyPostObject easyPostObject:
                    easyPostObject.Client = this;
                    PassClientToAllEasyPostObjectProperties(easyPostObject);
                    break;
            }
        }

        /// <summary>
        ///     Get a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service class to instantiate.</typeparam>
        /// <returns>A T-type instance.</returns>
        protected T GetService<T>() where T : IEasyPostService
        {
            // construct a new service
            var cons = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)cons[0].Invoke(new object[] { this });
        }

        public override bool Equals(object? obj) => obj is EasyPostClient client && Configuration.Equals(client.Configuration);

        public override int GetHashCode() => Configuration.GetHashCode();


        /// <summary>
        ///     Make a copy of this client, with the ability to override API key, API base, and HttpClient.
        /// </summary>
        /// <param name="overrideApiKey">Optional alternate API key to use.</param>
        /// <param name="overrideApiBase">Optional alternate API base to use.</param>
        /// <param name="overrideHttpClient">Optional alternate HttpClient to use.</param>
        /// <typeparam name="T">Type of client to duplicate.</typeparam>
        /// <returns>A T-type client object.</returns>
        // NOTE: If you ever need to initialize a new client (i.e. temporarily switch API keys), use this function to do so.
        // This will preserve all other configuration options (e.g. request timeout, VCR, etc.)
        [Obsolete("This method does not always function as expected. Create a new Client instead.")]
        public T Clone<T>(string? overrideApiKey = null, string? overrideApiBase = null, HttpClient? overrideHttpClient = null) where T : EasyPostClient
        {
            // TODO: You can't reuse the same HTTP client to re-initialize an EasyPost client, because the HTTP client is already initialized and can't be modified.
            // From a testing perspective, this means any VCR client will not be passed into the new client.
            // We should investigate a re-initialization/cloning process for HttpClient.
            var constructors = typeof(T).GetConstructors();

            if (constructors == null || constructors.Length == 0)
            {
                throw new Exception("Could not clone client. No constructors found.");
            }

            T clone = (T)constructors[0].Invoke(new object?[] { overrideApiKey ?? Configuration.ApiKey, overrideApiBase ?? Configuration.ApiBase, overrideHttpClient });

            // We need to manually copy over other configuration options
            clone.Configuration.ConnectTimeoutMilliseconds = Configuration.ConnectTimeoutMilliseconds;

            return clone;
        }

        public void Dispose() => _httpClient.Dispose();
    }
}
