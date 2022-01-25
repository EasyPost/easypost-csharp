using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace EasyPost {
    public class Client {
        public string version;

        internal RestClient client;
        internal ClientConfiguration configuration;

        /// <summary>
        ///  Constructor for the EasyPost client.
        /// </summary>
        /// <param name="clientConfiguration">EasyPost.ClientConfiguration object instance to use to configure this client.</param>
        public Client(ClientConfiguration clientConfiguration) {
            System.Net.ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            configuration = clientConfiguration ?? throw new ArgumentNullException("clientConfiguration");

            client = new RestClient(clientConfiguration.ApiBase);

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = info.FileVersion;
        }

        /// <summary>
        /// Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <returns>RestSharp.IRestResponse instance.</returns>
        internal IRestResponse Execute(Request request) {
            return client.Execute(PrepareRequest(request));
        }

        /// <summary>
        /// Execute a request against the EasyPost API.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to execute.</param>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        /// <exception cref="HttpException">An error occurred during the API request.</exception>
        internal T Execute<T>(Request request) where T : new() {
            RestResponse<T> response = (RestResponse<T>)client.Execute<T>(PrepareRequest(request));
            int StatusCode = Convert.ToInt32(response.StatusCode);

            if (StatusCode > 399) {
                Dictionary<string, Dictionary<string, object>> Body;
                List<Error> Errors;

                try {
                    Body = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                    Errors = JsonConvert.DeserializeObject<List<Error>>(JsonConvert.SerializeObject(Body["error"]["errors"]));
                }
                catch {
                    throw new HttpException(StatusCode, "RESPONSE.PARSE_ERROR", response.Content, new List<Error>());
                }

                throw new HttpException(
                    StatusCode,
                    (string)Body["error"]["code"],
                    (string)Body["error"]["message"],
                    Errors
                );
            }

            return response.Data;
        }

        /// <summary>
        /// Prepare a request for execution by attaching required headers.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to prepare.</param>
        /// <returns>RestSharp.RestRequest object instance to execute.</returns>
        internal RestRequest PrepareRequest(Request request) {
            RestRequest restRequest = (RestRequest)request;

            restRequest.AddHeader("user_agent", string.Concat("EasyPost/v2 CSharp/", version));
            restRequest.AddHeader("authorization", "Bearer " + this.configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }
    }
}