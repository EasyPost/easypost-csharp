using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
    /// <summary>
    ///     The base class for all EasyPost clients (i.e. <see cref="Client"/> and <see cref="BetaClient"/>).
    /// </summary>
    public abstract class EasyPostClient : IDisposable
    {
        /// <summary>
        ///     The configuration for this client. This is immutable once set and is not accessible to end-users.
        /// </summary>
        private readonly ClientConfiguration _configuration;

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
        ///     Gets the custom <see cref="System.Net.Http.HttpClient"/> used by this client.
        /// </summary>
        public HttpClient? CustomHttpClient => _configuration.CustomHttpClient; // public read-only property so users can audit the custom HTTP client they set to be used by the client

        /// <summary>
        ///     Gets the prepared <see cref="System.Net.Http.HttpClient"/> used by this client for all API calls.
        ///     This is the actual client used to make requests, is inaccessible to end-users, and will never be null.
        /// </summary>
        private HttpClient HttpClient => _configuration.PreparedHttpClient!;

        /// <summary>
        ///     Gets the <see cref="Hooks"/> used by this client.
        /// </summary>
        private Hooks Hooks => _configuration.Hooks;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EasyPostClient"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="ClientConfiguration"/> to use for this client.</param>
        protected EasyPostClient(ClientConfiguration configuration)
        {
            _configuration = configuration;

            // set up the configuration, needed to finalize the HttpClient used to make requests
            _configuration.SetUp();
        }

        /// <summary>
        ///     Execute an HTTP request.
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/> to execute.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> object.</returns>
        public virtual async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // This method actually executes the request and returns the response.
            // Everything up to this point has been pre-request, and everything after this point is post-request.
            // This method is "virtual" so it can be overridden (i.e. by a MockClient in testing to avoid making actual HTTP requests).

            // try to execute the request, catch and rethrow an HTTP timeout exception, all other exceptions are thrown as-is
            try
            {
                // generate a UUID and starting timestamp for this request
                var requestId = Guid.NewGuid();
                var requestTimestamp = Environment.TickCount;
                // if a pre-request event has been set, invoke it
                Hooks.OnRequestBeforeExecution?.Invoke(this, new RequestBeforeExecutionEventArgs(request, requestTimestamp, requestId));
                // execute the request
                var response = await HttpClient.SendAsync(request, cancellationToken: cancellationToken).ConfigureAwait(false);
                // if a post-request event has been set, invoke it
                var responseTimestamp = Environment.TickCount;
                Hooks.OnRequestResponseReceived?.Invoke(this, new RequestResponseReceivedEventArgs(response, requestTimestamp, responseTimestamp, requestId));

                return response;
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
        /// <param name="apiVersion"><see cref="ApiVersion"/> to use for the request.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <param name="parameters">Optional parameters to use for the request.</param>
        /// <param name="rootElement">Optional root element for the resultant JSON to begin deserialization at.</param>
        /// <typeparam name="T">Type of object to deserialize response data into. Must be subclass of <see cref="EasyPostObject"/>.</typeparam>
        /// <returns>An instance of a T-type object.</returns>
        internal async Task<T> RequestAsync<T>(Method method, string endpoint, ApiVersion apiVersion, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, string? rootElement = null)
            where T : class
        {
            // Build the request
            Dictionary<string, string> headers = _configuration.GetHeaders(apiVersion);
            Request request = new(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage(), cancellationToken);

            // Check the response's status code
            if (response.ReturnedError())
            {
                throw await ApiError.FromErrorResponse(response).ConfigureAwait(false);
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
        /// <param name="method">HTTP <see cref="Method"/> to use for the request.</param>
        /// <param name="endpoint">EasyPost API endpoint to use for the request.</param>
        /// <param name="apiVersion"><see cref="ApiVersion"/> to use for the request.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <param name="parameters">Optional parameters to use for the request.</param>
        /// <returns><c>true</c> if the request was successful, <c>false</c> otherwise.</returns>
        // ReSharper disable once UnusedMethodReturnValue.Global
        internal async Task<bool> RequestAsync(Method method, string endpoint, ApiVersion apiVersion, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null)
        {
            // Build the request
            Dictionary<string, string> headers = _configuration.GetHeaders(apiVersion);
            Request request = new(ApiBaseInUse, endpoint, method, apiVersion, parameters, headers);

            // Execute the request
            HttpResponseMessage response = await ExecuteRequest(request.AsHttpRequestMessage(), cancellationToken);

            // Check whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
            bool errorRaised = response.ReturnedNoError();

            // Dispose of the request and response
            request.Dispose();
            response.Dispose();

            return errorRaised;
        }

        /// <summary>
        ///     Compare this <see cref="EasyPostClient"/> to another object for equality.
        /// </summary>
        /// <param name="obj">An object to compare this client against.</param>
        /// <returns><c>true</c> if the two objects are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj) => obj is EasyPostClient client && _configuration.Equals(client._configuration);

        /// <summary>
        ///     Get the hash code for this <see cref="EasyPostClient"/>.
        /// </summary>
        /// <returns>The hash code for this client.</returns>
        public override int GetHashCode() => _configuration.GetHashCode();

        /// <summary>
        ///     Whether this object has been disposed or not.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        ///     Dispose of this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose of this object.
        /// </summary>
        /// <param name="disposing">Whether this object is being disposed.</param>
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

        /// <summary>
        ///     Finalizer for this <see cref="EasyPostClient"/>.
        /// </summary>
        ~EasyPostClient()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(disposing: false);
        }
    }
}
