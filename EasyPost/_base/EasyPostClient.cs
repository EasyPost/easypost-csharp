using System;
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
    public abstract class EasyPostClient : IDisposable
    {
        internal ClientConfiguration Configuration; // configuration is internal to blackbox it from end-users (can be modified for referral purposes, but be careful)

        /// <summary>
        ///     Gets the API key used by this client.
        /// </summary>
        public string ApiKeyInUse => Configuration.ApiKey;  // public read-only property so users can audit the API key used by the client

        /// <summary>
        ///     Gets the base URL used by this client.
        /// </summary>
        public string ApiBaseInUse => Configuration.ApiBase; // public read-only property so users can audit the base URL used by the client

        /// <summary>
        ///     Gets the connect timeout in milliseconds used by this client.
        /// </summary>
        public int ConnectTimeoutMillisecondsInUse => Configuration.ConnectTimeoutMilliseconds; // public read-only property so users can audit the connect timeout used by the client

        /// <summary>
        ///     Gets the custom HTTP client used by this client.
        /// </summary>
        public HttpClient? CustomHttpClientInUse => Configuration.CustomHttpClient; // public read-only property so users can audit the custom HTTP client they set to be used by the client


        /// <summary>
        ///     Gets the prepared HTTP client used by this client for all API calls.
        /// </summary>
        private HttpClient HttpClient => Configuration.PreparedHttpClient!; // this is the actual, prepared HttpClient used to make requests

        /// <summary>
        ///     Initializes a new instance of the <see cref="EasyPostClient"/> class.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to use if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        /// <param name="connectTimeoutMilliseconds">Timeout for all API calls made by the client.</param>
        [Obsolete("This constructor is deprecated and will be removed in a future release. Please use the constructor that takes a ClientConfiguration object instead.")]
        protected EasyPostClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null, int? connectTimeoutMilliseconds = null)
        {
            ClientConfiguration configuration = new(apiKey);

            if (baseUrl != null)
            {
                configuration.ApiBase = baseUrl;
            }

            if (customHttpClient != null)
            {
                configuration.CustomHttpClient = customHttpClient;
            }

            if (connectTimeoutMilliseconds != null)
            {
                configuration.ConnectTimeoutMilliseconds = connectTimeoutMilliseconds.Value;
            }

            Configuration = configuration;
            Configuration.SetUpClient(); // need to run this AFTER configuration is constructed due to property access
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EasyPostClient"/> class.
        /// </summary>
        /// <param name="configuration">Configuration to use with this client.</param>
        protected EasyPostClient(ClientConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.SetUpClient(); // need to run this AFTER configuration is constructed due to property access
        }

        /// <summary>
        ///     Execute an HTTP request.
        /// </summary>
        /// <param name="request">Request to execute.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> object.</returns>
        // ReSharper disable once MemberCanBeProtected.Global
        internal virtual async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request) =>

            // This method actually executes the request and returns the response.
            // Everything up to this point has been pre-request, and everything after this point is post-request.
            // This method is "virtual" so it can be overriden (i.e. by a MockClient in testing to avoid making actual HTTP requests).
            await HttpClient.SendAsync(request);

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
            var headers = new Dictionary<string, string>
            {
                // User-Agent is already set as a default header for the HttpClient
                { "Authorization", $"Bearer {ApiKeyInUse}" },
                { "Content-Type", "application/json" },
            };
            Request request = new Request(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

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

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <returns>Whether request was successful.</returns>
        internal async Task<bool> Request(Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null)
        {
            // Build the request
            var headers = new Dictionary<string, string>
            {
                // User-Agent is already set as a default header for the HttpClient
                { "Authorization", $"Bearer {ApiKeyInUse}" },
                { "Content-Type", "application/json" }
            };
            Request request = new Request(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage());

            // Return whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
            return response.ReturnedNoError();
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
                case PaginatedCollection<EasyPostObject> collection:
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

        public override bool Equals(object? obj) => obj is EasyPostClient client && Configuration.Equals(client.Configuration);

#pragma warning disable CA1307
        public override int GetHashCode() => Configuration.GetHashCode();
#pragma warning restore CA1307

        public void Dispose()
        {
            Configuration.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
