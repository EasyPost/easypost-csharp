using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Models.Shared;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost._base
{
    public abstract class EasyPostClient
    {
        public readonly ClientConfiguration Configuration;

        private readonly RestClient _restClient;

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. This will override `apiVersion`</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        protected EasyPostClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null)
        {
            ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            Configuration = new ClientConfiguration(apiKey, baseUrl, customHttpClient);

            RestClientOptions clientOptions = new RestClientOptions
            {
                MaxTimeout = Configuration.ConnectTimeoutMilliseconds,
                BaseUrl = new Uri(Configuration.ApiBase),
                UserAgent = Configuration.UserAgent
            };

            _restClient = Configuration.HttpClient != null ? new RestClient(Configuration.HttpClient, clientOptions) : new RestClient(clientOptions);
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        internal async Task<T> Request<T>(Method method, string url, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class
        {
            // Build the request
            Request request = new Request(url, method, apiVersion, parameters, rootElement);
            RestRequest restRequest = PrepareRequest(request);

            // Execute the request
            RestResponse<T> response = await _restClient.ExecuteAsync<T>(restRequest);

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

        private void PassClientToAllEasyPostObjectProperties<T>(T? resource) where T : EasyPostObject
        {
            if (resource == null)
            {
                return;
            }

            List<PropertyInfo> properties = new List<PropertyInfo>(resource.GetType().GetProperties());
            foreach (PropertyInfo property in properties)
            {
                // Pass the Client into every EasyPostObject in the collection
                PassClientToEasyPostObject(property.GetValue(resource));
            }
        }

        private void PassClientToEasyPostObjectsInList<T>(T? resource) where T : IList
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
        /// <returns>Object with Client added.</returns>
        private void PassClientToEasyPostObject<T>(T? resource) where T : class
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
            }
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <returns>Whether request was successful.</returns>
        internal async Task<bool> Request(Method method, string url, ApiVersion apiVersion, Dictionary<string, object>? parameters = null)
        {
            // Build the request
            Request request = new Request(url, method, apiVersion, parameters);
            RestRequest restRequest = PrepareRequest(request);

            // Execute the request
            RestResponse response = await _restClient.ExecuteAsync(restRequest);

            // Return whether the HTTP request produced an error (4xx or 5xx status code) or not
            return response.ReturnedNoError();
        }

        /// <summary>
        ///     Get a service instance.
        /// </summary>
        /// <typeparam name="T">Type of service class to instantiate.</typeparam>
        /// <returns>A T-type instance.</returns>
        protected T GetService<T>() where T : IEasyPostService
        {
            // construct a new service
            var cons = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
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

        public override int GetHashCode() => Configuration.GetHashCode();
    }
}
