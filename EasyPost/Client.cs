using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace EasyPost
{
    public class Client
    {
        private readonly string _libraryVersion;
        private readonly string _dotNetVersion;
        private readonly RestClient _restClient;
        private readonly ClientConfiguration _configuration;
        private string UserAgent => $"EasyPost/v2 CSharpClient/{_libraryVersion} .NET/{_dotNetVersion}";
        private int _connectTimeoutMilliseconds = 30000;
        private int _requestTimeoutMilliseconds = 60000;

        /// <summary>
        /// Prepare a request for execution by attaching required headers.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to prepare.</param>
        /// <returns>RestSharp.RestRequest object instance to execute.</returns>
        private RestRequest PrepareRequest(Request request)
        {
            var restRequest = (RestRequest)request;
            restRequest.Timeout = _requestTimeoutMilliseconds;
            restRequest.AddHeader("user_agent", UserAgent);
            restRequest.AddHeader("authorization", "Bearer " + _configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }

        /// <summary>
        /// Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <returns>RestSharp.IRestResponse instance.</returns>
        internal IRestResponse Execute(Request request)
        {
            return _restClient.Execute(PrepareRequest(request));
        }

        /// <summary>
        /// Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        /// <exception cref="HttpException">An error occurred during the API request.</exception>
        internal T Execute<T>(Request request) where T : new()
        {
            var response = (RestResponse<T>)_restClient.Execute<T>(PrepareRequest(request));
            var statusCode = Convert.ToInt32(response.StatusCode);

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
        ///  Constructor for the EasyPost client.
        /// </summary>
        /// <param name="clientConfiguration">EasyPost.ClientConfiguration object instance to use to configure this client.</param>
        public Client(ClientConfiguration clientConfiguration)
        {
            
            System.Net.ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            _configuration = clientConfiguration ?? throw new ArgumentNullException("clientConfiguration");

            _restClient = new RestClient(clientConfiguration.ApiBase);
            _restClient.Timeout = _connectTimeoutMilliseconds;

            var assembly = Assembly.GetExecutingAssembly();
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);
            _libraryVersion = info.FileVersion;

            var dotNetVersion = Environment.Version.ToString();
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
        /// Return the request time from the client object.
        /// </summary>
        /// <returns> connection timeout in milliseconds</returns>
        public int getConnectionTimeout(){
            return _connectTimeoutMilliseconds;
        }

        /// <summary>
        /// Set the connection timeout with a time in milliseconds.
        /// </summary>
        /// <param name="connectionTimeoutMilliseconds">connection time in milliseconds.</param>
        public void setConnectionTimeout(int connectionTimeoutMilliseconds){
            _connectTimeoutMilliseconds = connectionTimeoutMilliseconds;
        }

        /// <summary>
        /// Return the request time from the client object.
        /// </summary>
        /// <returns> request timeout in milliseconds</returns>
        public int getRequestTimeout(){
            return _requestTimeoutMilliseconds;
        }

        /// <summary>
        /// Set the request timeout with a time in milliseconds.
        /// </summary>
        /// <param name="requestTimeoutMilliseconds">request time in milliseconds.</param>
        public void setRequestTimeout(int requestTimeoutMilliseconds){
            _requestTimeoutMilliseconds = requestTimeoutMilliseconds;
        }
    }
}