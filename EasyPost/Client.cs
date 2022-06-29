using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost
{
    public class Client
    {
        private const int DefaultConnectTimeoutMilliseconds = 30000;
        private const int DefaultRequestTimeoutMilliseconds = 60000;

        internal readonly ClientConfiguration Configuration;

        private readonly string _dotNetVersion;
        private readonly string _libraryVersion;
        private readonly string _osDetails;

        private readonly RestClient _restClient;
        private int? _connectTimeoutMilliseconds;
        private int? _requestTimeoutMilliseconds;

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

        private string UserAgent => $"EasyPost/v2 CSharpClient/{_libraryVersion} .NET/{_dotNetVersion} {_osDetails}";

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="clientConfiguration">EasyPost.ClientConfiguration object instance to use to configure this client.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not advised for general use.</param>
        public Client(ClientConfiguration clientConfiguration, HttpClient? customHttpClient = null)
        {
            ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            Configuration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));

            _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
            _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
            _osDetails = $"OS/{RuntimeInfo.OperationSystemInfo.Name} OSVersion/{RuntimeInfo.OperationSystemInfo.Version} OSArch/{RuntimeInfo.OperationSystemInfo.Architecture}";

            RestClientOptions clientOptions = new RestClientOptions
            {
                Timeout = ConnectTimeoutMilliseconds,
                BaseUrl = new Uri(clientConfiguration.ApiBase),
                UserAgent = UserAgent
            };

            _restClient = customHttpClient != null ? new RestClient(customHttpClient, clientOptions) : new RestClient(clientOptions);
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <returns>Whether request was successful.</returns>
        internal async Task<bool> Execute(Request request)
        {
            RestResponse response = await _restClient.ExecuteAsync(PrepareRequest(request));
            return response.IsSuccessful;
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <param name="rootElement">Key of root element of the JSON response. Used while deserializing.</param>
        /// <returns>An instance of a T type object.</returns>
        /// <exception cref="HttpException">An error occurred during the API request.</exception>
        internal async Task<T> Execute<T>(Request request, string? rootElement = null) where T : new()
        {
            RestResponse<T> response = await _restClient.ExecuteAsync<T>(PrepareRequest(request));
            int statusCode = Convert.ToInt32(response.StatusCode);

            List<string>? rootElements = null;
            if (rootElement != null)
            {
                rootElements = new List<string>
                {
                    rootElement
                };
            }

            if (statusCode < 400)
            {
                return JsonSerialization.ConvertJsonToObject<T>(response, null, rootElements);
            }

            Dictionary<string, Dictionary<string, object>> body;
            List<Error> errors;

            try
            {
                body = JsonSerialization.ConvertJsonToObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                errors = JsonSerialization.ConvertJsonToObject<List<Error>>(response.Content, null, new List<string>
                {
                    "error",
                    "errors"
                });
            }
            catch
            {
                throw new HttpException(statusCode, "RESPONSE.PARSE_ERROR", response.Content, new List<Error>());
            }

            throw new HttpException(
                statusCode,
                (string)body["error"]["code"],
                (string)body["error"]["message"],
                errors
            );
        }

        /// <summary>
        ///     Prepare a request for execution by attaching required headers.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to prepare.</param>
        /// <returns>RestSharp.RestRequest object instance to execute.</returns>
        private RestRequest PrepareRequest(Request request)
        {
            RestRequest restRequest = (RestRequest)request;
            restRequest.Timeout = RequestTimeoutMilliseconds;
            restRequest.AddHeader("authorization", "Bearer " + Configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }
    }
}
