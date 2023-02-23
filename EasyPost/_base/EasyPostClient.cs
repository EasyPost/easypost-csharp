using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Extensions;
using RestSharp;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostClient
    {
        public readonly ClientConfiguration Configuration;

        private readonly RestClient _restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EasyPostClient"/> class.
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        protected EasyPostClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null)
        {
            Configuration = new ClientConfiguration(apiKey, baseUrl, customHttpClient);

            RestClientOptions clientOptions = new()
            {
                MaxTimeout = Configuration.ConnectTimeoutMilliseconds,
                BaseUrl = new Uri(Configuration.ApiBase),
                UserAgent = Configuration.UserAgent,
            };

            _restClient = Configuration.HttpClient != null ? new RestClient(Configuration.HttpClient, clientOptions) : new RestClient(clientOptions);
        }

        /// <summary>
        ///     Execute an HTTP request.
        /// </summary>
        /// <param name="request">Request to execute.</param>
        /// <typeparam name="T">Type of object to serialize response into.</typeparam>
        /// <returns>A RestResponse containing a T-type object.</returns>
        internal virtual async Task<RestResponse<T>> ExecuteRequest<T>(RestRequest request) =>

            // This method actually executes the request and returns the response.
            // Everything up to this point has been pre-request, and everything after this point is post-request.
            // This method is "virtual" so it can be overriden (i.e. by a MockClient in testing to avoid making actual HTTP requests).
            await _restClient.ExecuteAsync<T>(request);

        /// <summary>
        ///     Execute an HTTP request.
        /// </summary>
        /// <param name="request">Request to execute.</param>
        /// <returns>A RestResponse object.</returns>
        // ReSharper disable once MemberCanBeProtected.Global
        internal virtual async Task<RestResponse> ExecuteRequest(RestRequest request) =>

            // This method actually executes the request and returns the response.
            // Everything up to this point has been pre-request, and everything after this point is post-request.
            // This method is "virtual" so it can be overriden (i.e. by a MockClient in testing to avoid making actual HTTP requests).
            await _restClient.ExecuteAsync(request);

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
        internal async Task<T> Request<T>(Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, string? rootElement = null)
            where T : class
        {
            // Build the request
            Request request = new(endpoint, method, apiVersion, parameters, rootElement);
            RestRequest restRequest = PrepareRequest(request);

            // Execute the request
            RestResponse<T> response = await ExecuteRequest<T>(restRequest);

            // Check the response's status code
            if (response.ReturnedError())
            {
                throw ApiError.FromErrorResponse(response);
            }

            // Prepare the list of root elements to use during deserialization
            List<string>? rootElements = null;
            if (request.RootElement != null)
            {
                rootElements = new List<string> { request.RootElement };
            }

            // Deserialize the response into an object
            T resource = JsonSerialization.ConvertJsonToObject<T>(response, null, rootElements);

            if (resource is null)
            {
                // Object deserialization failed
                throw new JsonDeserializationError(typeof(T));
            }

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
                case Collection collection:
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
        internal async Task<bool> Request(Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null)
        {
            // Build the request
            Request request = new(endpoint, method, apiVersion, parameters);
            RestRequest restRequest = PrepareRequest(request);

            // Execute the request
            // RestSharp does not throw exception directly (https://restsharp.dev/error-handling.html), we'll need to check the response status code
            RestResponse response = await ExecuteRequest(restRequest);

            // Return whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
            return response.ReturnedNoError();
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

        /// <summary>
        ///     Prepare a request for execution by attaching required headers.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to prepare.</param>
        /// <returns>RestSharp.RestRequest object instance to execute.</returns>
        private RestRequest PrepareRequest(Request request)
        {
            request.BuildParameters();

            RestRequest restRequest = (RestRequest)request;
            restRequest.Timeout = Configuration.RequestTimeoutMilliseconds;
            restRequest.AddHeader("authorization", $"Bearer {Configuration.ApiKey}");
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }

        public override bool Equals(object? obj) => obj is EasyPostClient client && Configuration.Equals(client.Configuration);

#pragma warning disable CA1307
        public override int GetHashCode() => Configuration.GetHashCode();
#pragma warning restore CA1307

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
        public T Clone<T>(string? overrideApiKey = null, string? overrideApiBase = null, HttpClient? overrideHttpClient = null)
            where T : EasyPostClient
        {
            // TODO: You can't reuse the same HTTP client to re-initialize an EasyPost client, because the HTTP client is already initialized and can't be modified.
            // From a testing perspective, this means any VCR client will not be passed into the new client.
            // We should investigate a re-initialization/cloning process for HttpClient.
            ConstructorInfo[] constructors = typeof(T).GetConstructors();

            if (constructors == null || constructors.Length == 0)
            {
                throw new EasyPostError("Could not clone client. No constructors found.");
            }

            T clone = (T)constructors[0].Invoke(new object?[] { overrideApiKey ?? Configuration.ApiKey, overrideApiBase ?? Configuration.ApiBase, overrideHttpClient });

            // We need to manually copy over other configuration options
            clone.Configuration.ConnectTimeoutMilliseconds = Configuration.ConnectTimeoutMilliseconds;
            clone.Configuration.RequestTimeoutMilliseconds = Configuration.RequestTimeoutMilliseconds;

            return clone;
        }
    }
}
