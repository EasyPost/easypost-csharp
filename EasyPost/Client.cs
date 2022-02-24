using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Client
    {
        private const int DefaultConnectTimeoutMilliseconds = 30000;
        private const int DefaultRequestTimeoutMilliseconds = 60000;

        private readonly ClientConfiguration _configuration;

        private readonly string _dotNetVersion;
        private readonly string _libraryVersion;

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

        private string UserAgent => $"EasyPost/v2 CSharpClient/{_libraryVersion} .NET/{_dotNetVersion}";

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="clientConfiguration">EasyPost.ClientConfiguration object instance to use to configure this client.</param>
        public Client(ClientConfiguration clientConfiguration)
        {
            ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            _configuration = clientConfiguration ?? throw new ArgumentNullException("clientConfiguration");

            _restClient = new RestClient(clientConfiguration.ApiBase);
            _restClient.Timeout = ConnectTimeoutMilliseconds;

            try
            {
                // ref Example 1: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assemblyname.version?view=netframework-4.8#examples
                _libraryVersion = typeof(Client).Assembly.GetName().Version.ToString();
            } catch (Exception)
            {
                _libraryVersion = "Unknown";
            }


            string dotNetVersion = Environment.Version.ToString();
            if (dotNetVersion == "4.0.30319.42000")
            {
                /*
                 * We're on a v4.6+ version (or pre-.NET Core 3.0, which we don't support),
                 * but we can't get the exact version.
                 * See: https://docs.microsoft.com/en-us/dotnet/api/system.environment.version?view=net-6.0#remarks
                 */
                dotNetVersion = "4.6 or higher";
            }

            _dotNetVersion = dotNetVersion;
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <returns>RestSharp.IRestResponse instance.</returns>
        internal IRestResponse Execute(Request request) => _restClient.Execute(PrepareRequest(request));

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        /// <exception cref="HttpException">An error occurred during the API request.</exception>
        internal T Execute<T>(Request request) where T : new()
        {
            RestResponse<T> response = (RestResponse<T>)_restClient.Execute<T>(PrepareRequest(request));
            int statusCode = Convert.ToInt32(response.StatusCode);

            if (statusCode < 400)
            {
                return response.Data;
            }

            Dictionary<string, Dictionary<string, object>> body;
            List<Error> errors;

            try
            {
                body = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                errors = JsonConvert.DeserializeObject<List<Error>>(
                    JsonConvert.SerializeObject(body["error"]["errors"]));
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
            restRequest.AddHeader("user_agent", UserAgent);
            restRequest.AddHeader("authorization", "Bearer " + _configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }
    }
}
