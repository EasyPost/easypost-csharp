using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Extensions;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostClient : IDisposable
    {
        private readonly ClientConfiguration _configuration; // configuration is immutable by end-user once set

        /// <summary>
        ///     Gets the API key used by this client.
        /// </summary>
        public string ApiKeyInUse
        {
            get => _configuration.ApiKey; // public read-only property so users can audit the API key used by the client
            internal set => _configuration.ApiKey = value; // internal setter so the library can alter this client's API key when we need to
        }

        /// <summary>
        ///     Gets the base URL used by this client.
        /// </summary>
        public string ApiBaseInUse => _configuration.ApiBase; // public read-only property so users can audit the base URL used by the client

        /// <summary>
        ///     Gets the timeout used by this client.
        /// </summary>
        public TimeSpan Timeout => _configuration.Timeout; // public read-only property so users can audit the connect timeout used by the client

        /// <summary>
        ///     Gets the custom HTTP client used by this client.
        /// </summary>
        public HttpClient? CustomHttpClient => _configuration.CustomHttpClient; // public read-only property so users can audit the custom HTTP client they set to be used by the client

        /// <summary>
        ///     Gets the prepared HTTP client used by this client for all API calls.
        /// </summary>
        private HttpClient HttpClient => _configuration.PreparedHttpClient!; // this is the actual, prepared HttpClient used to make requests, will never be null

        /// <summary>
        ///     Initializes a new instance of the <see cref="EasyPostClient"/> class.
        /// </summary>
        /// <param name="configuration">Configuration for this client.</param>
        protected EasyPostClient(ClientConfiguration configuration)
        {
            _configuration = configuration;

            // set up the configuration, needed to finalize the HttpClient used to make requests
            _configuration.SetUp();
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
            // This method is "virtual" so it can be overridden (i.e. by a MockClient in testing to avoid making actual HTTP requests).

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
        internal async Task<T> Request<T>(Method method, string endpoint, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, string? rootElement = null)
            where T : class
        {
            // Build the request
            Dictionary<string, string> headers = _configuration.GetHeaders(apiVersion);
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
            
            // Dispose of the request and response
            request.Dispose();
            response.Dispose();

#pragma warning disable IDE0270 // Simplify null check
            if (resource is null)
            {
                // Object deserialization failed
                throw new JsonDeserializationError(typeof(T));
            }
#pragma warning restore IDE0270

            return resource;
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
            Dictionary<string, string> headers = _configuration.GetHeaders(apiVersion);
            Request request = new(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage());

            // Check whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
            bool errorRaised = response.ReturnedNoError();

            // Dispose of the request and response
            request.Dispose();
            response.Dispose();

            return errorRaised;
        }

        public override bool Equals(object? obj) => obj is EasyPostClient client && _configuration.Equals(client._configuration);

#pragma warning disable CA1307
        public override int GetHashCode() => _configuration.GetHashCode();
#pragma warning restore CA1307

        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                // Dispose managed state (managed objects).

                // Dispose the configuration
                _configuration.Dispose();
            }

            // Free native resources (unmanaged objects) and override a finalizer below.
            _isDisposed = true;
        }

        ~EasyPostClient()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(disposing: false);
        }
    }
}
