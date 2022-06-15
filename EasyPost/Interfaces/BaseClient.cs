using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Http;
using EasyPost.Models.V2;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost.Interfaces
{
    public class BaseClient
    {
        private const int DefaultConnectTimeoutMilliseconds = 30000;
        private const int DefaultRequestTimeoutMilliseconds = 60000;

        private readonly ClientConfiguration _configuration;

        private readonly RestClient _restClient;
        private int? _connectTimeoutMilliseconds;
        private int? _requestTimeoutMilliseconds;

        public ApiVersion ApiVersion
        {
            get { return _configuration.ApiVersion; }
        }

        public int ConnectTimeoutMilliseconds
        {
            get => _connectTimeoutMilliseconds ?? DefaultConnectTimeoutMilliseconds;
            set => _connectTimeoutMilliseconds = value;
        }

        public int RequestTimeoutMilliseconds
        {
            get => _requestTimeoutMilliseconds ?? DefaultRequestTimeoutMilliseconds;
            set => _requestTimeoutMilliseconds = value;
        }

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="version">What version of the API to use.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        protected BaseClient(string apiKey, ApiVersion version, HttpClient? customHttpClient = null)
        {
            ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            _configuration = new ClientConfiguration(apiKey, version);


            RestClientOptions clientOptions = new RestClientOptions
            {
                Timeout = ConnectTimeoutMilliseconds,
                BaseUrl = new Uri(_configuration.ApiBase),
                UserAgent = _configuration.UserAgent
            };

            _restClient = customHttpClient != null ? new RestClient(customHttpClient, clientOptions) : new RestClient(clientOptions);
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        /// <exception cref="ApiException">An error occurred during the API request.</exception>
        internal async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class
        {
            // Build the request
            Request request = new Request(url, method, parameters, rootElement);
            RestRequest restRequest = PrepareRequest(request);

            // Execute the request
            RestResponse<T> response = await _restClient.ExecuteAsync<T>(restRequest);

            // Check the response's status code
            if (!response.IsSuccessful)
            {
                // HTTP request threw an error (non-2xx response)
                // RestSharp utilizes .NET HttpStatusCode internally:
                // https://docs.microsoft.com/en-us/uwp/api/windows.web.http.httpresponsemessage.issuccessstatuscode?view=winrt-22621

                ApiException apiException = ApiException.FromRestResponse(response);
                throw apiException;
            }

            // Prepare the list of root elements to use during deserialization
            List<string>? rootElements = null;
            if (request.RootElement != null)
            {
                rootElements = new List<string>
                {
                    request.RootElement
                };
            }

            // Deserialize the response into an object
            T resource = JsonSerialization.ConvertJsonToObject<T>(response, null, rootElements);

            // Copy this client to the object
            if (resource is IList list)
            {
                foreach (object? element in list)
                {
                    (element as EasyPostObject)!.Client = (Client)this;
                }
            }
            else
            {
                (resource as EasyPostObject)!.Client = (Client)this;
            }

            return resource;
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <returns>Whether request was successful.</returns>
        internal async Task<bool> Request(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null)
        {
            // Build the request
            Request request = new Request(url, method, parameters, rootElement);
            RestRequest restRequest = PrepareRequest(request);

            // Execute the request
            RestResponse response = await _restClient.ExecuteAsync(restRequest);

            // Return whether the HTTP request produced an error (non-2xx response) or not
            return response.IsSuccessful;
        }

        /// <summary>
        ///     Get a service instance if the selected API version supports it.
        /// </summary>
        /// <param name="servicePropertyName">Name of the property to get the service.</param>
        /// <typeparam name="T">Type of service class to instantiate.</typeparam>
        /// <returns>A T-type instance.</returns>
        /// <exception cref="Exception">Failed to verify API compatibility.</exception>
        /// <exception cref="ApiVersionNotSupported">Resource not available on the selected API version.</exception>
        protected T GetService<T>(string servicePropertyName) where T : class
        {
            ApiCompatibilityUtilities.CheckServiceCompatible(servicePropertyName, GetType(), this);

            // construct a new service
            var cons = (typeof(T)).GetConstructors(BindingFlags.NonPublic|BindingFlags.Instance);
            return (T)cons[0].Invoke(new object[]
            {
                this
            });
        }

        /// <summary>
        ///     Prepare a request for execution by attaching required headers.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to prepare.</param>
        /// <returns>RestSharp.RestRequest object instance to execute.</returns>
        private RestRequest PrepareRequest(Request request)
        {
            request.Build();

            RestRequest restRequest = (RestRequest)request;
            restRequest.Timeout = RequestTimeoutMilliseconds;
            restRequest.AddHeader("authorization", $"Bearer {_configuration.ApiKey}");
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }
    }
}
