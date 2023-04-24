using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Extensions;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostClient
    {
        public readonly ClientConfiguration Configuration;

        /// <summary>
        ///     Gets the API key used by this client.
        /// </summary>
        public string ApiKeyInUse => Configuration.ApiKey; // public read-only property so users can audit the API key used by the client

        /// <summary>
        ///     Gets the base URL used by this client.
        /// </summary>
        public string ApiBaseInUse => Configuration.ApiBase; // public read-only property so users can audit the base URL used by the client

        /// <summary>
        ///     Gets the connect timeout in milliseconds used by this client.
        /// </summary>
        public int ConnectTimeoutMillisecondsInUse => (int)HttpClient.Timeout.TotalMilliseconds; // public read-only property so users can audit the connect timeout used by the client

        /// <summary>
        ///     Gets the custom HTTP client used by this client.
        /// </summary>
        public HttpClient? CustomHttpClientInUse => Configuration.HttpClient; // public read-only property so users can audit the custom HTTP client they set to be used by the client

        /// <summary>
        ///     Gets the prepared HTTP client used by this client for all API calls.
        /// </summary>
        private HttpClient HttpClient => Configuration.PreparedHttpClient; // this is the actual, prepared HttpClient used to make requests

        /// <summary>
        /// Initializes a new instance of the <see cref="EasyPostClient"/> class.
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`.</param>
        /// <param name="timeoutMilliseconds">Timeout length, in milliseconds, for API calls.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to use if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        protected EasyPostClient(string apiKey, string? baseUrl = null, int? timeoutMilliseconds = null, HttpClient? customHttpClient = null)
        {
            Configuration = new ClientConfiguration(apiKey, baseUrl, timeoutMilliseconds, customHttpClient);
        }

        /// <summary>
        ///     Execute an HTTP request.
        /// </summary>
        /// <param name="request">Request to execute.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> object.</returns>
        internal virtual async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request)
        {
            // This method actually executes the request and returns the response.
            // Everything up to this point has been pre-request, and everything after this point is post-request.
            // This method is "virtual" so it can be overriden (i.e. by a MockClient in testing to avoid making actual HTTP requests).

            // try to execute the request, catch and rethrow an HTTP timeout exception, all other exceptions are thrown as-is
            try
            {
                return await HttpClient.SendAsync(request);
            }
            catch (TaskCanceledException)
            {
                throw new TimeoutError(Constants.ErrorMessages.ApiRequestTimedOut, 408);
            }
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="method">HTTP method to use for the request.</param>
        /// <param name="endpoint">EasyPost API endpoint to use for the request.</param>
        /// <param name="apiVersion">API version to use for the request.</param>
        /// <param name="parameters">Optional parameters to use for the request.</param>
        /// <param name="rootElement">Optional root element for the JSON to begin deserialization at.</param>
        /// <typeparam name="T">Type of object to deserialize response data into. Must be subclass of EasyPostObject.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        internal async Task<T> Request<T>(Http.Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, string? rootElement = null)
            where T : class
        {
            // Build the request
            Dictionary<string, string> headers = Configuration.Headers;
            Request request = new(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

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

#pragma warning disable IDE0270 // Simplify null check
            if (resource is null)
            {
                // Object deserialization failed
                throw new JsonDeserializationError(typeof(T));
            }
#pragma warning restore IDE0270

            PassClientToEasyPostObject(resource);

            return resource;
        }

        private void PassClientToAllEasyPostObjectProperties<T>(T? resource)
            where T : EasyPostObject
        {
            if (resource == null)
            {
                return;
            }

            List<PropertyInfo> properties = new(resource.GetType().GetProperties());
            foreach (PropertyInfo property in properties)
            {
                // Pass the Client into every EasyPostObject in the collection
                PassClientToEasyPostObject(property.GetValue(resource));
            }
        }

        private void PassClientToEasyPostObjectsInList<T>(T? resource)
            where T : IList
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
        private void PassClientToEasyPostObject<T>(T? resource)
            where T : class
        {
            switch (resource)
            {
                case null:
                    break;
                case IList list:
                    PassClientToEasyPostObjectsInList(list);
                    break;
                case PaginatedCollection<EasyPost._base.EasyPostObject> collection:
                    PassClientToAllEasyPostObjectProperties(collection);
                    break;
                case EasyPostObject easyPostObject:
                    easyPostObject.Client = this;
                    PassClientToAllEasyPostObjectProperties(easyPostObject);
                    break;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// /// <param name="method">HTTP method to use for the request.</param>
        /// <param name="endpoint">EasyPost API endpoint to use for the request.</param>
        /// <param name="apiVersion">API version to use for the request.</param>
        /// <param name="parameters">Optional parameters to use for the request.</param>
        /// <returns>Whether request was successful.</returns>
        // ReSharper disable once UnusedMethodReturnValue.Global
        internal async Task<bool> Request(Http.Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null)
        {
            // Build the request
            Dictionary<string, string> headers = Configuration.Headers;
            Request request = new(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage());

            // Check whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
            bool errorRaised = response.ReturnedNoError();

            return errorRaised;
        }

        /// <summary>
        ///     Get a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service class to instantiate.</typeparam>
        /// <returns>A T-type instance.</returns>
        protected T GetService<T>()
            where T : IEasyPostService
        {
            // construct a new service
            ConstructorInfo[] cons = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)cons[0].Invoke(new object[] { this });
        }

        public override bool Equals(object? obj) => obj is EasyPostClient client && Configuration.Equals(client.Configuration);

#pragma warning disable CA1307
        public override int GetHashCode() => Configuration.GetHashCode();
#pragma warning restore CA1307
    }
}
