using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Utilities;
using RestSharp;
using RestSharp.Serializers;

namespace EasyPost.Http
{
    public class Request
    {
        private readonly RestRequest _restRequest;

        private readonly Dictionary<string, object> _parameters;

        private readonly Dictionary<string, object> _urlSegments;
        public string? RootElement { get; set; }

        public Request(string endpoint, Method method, Dictionary<string, object>? parameters = null)
        {
            _restRequest = new RestRequest(endpoint, method);
            _restRequest.AddHeader("Accept", "application/json");
            _parameters = parameters ?? new Dictionary<string, object>();
            _urlSegments = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Add a parameter to the request.
        /// </summary>
        /// <param name="parameters">Dictionary of parameters to add to request</param>
        public void AddParameters(Dictionary<string, object>? parameters = null)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                _parameters.Add(parameter.Key, parameter.Value);
            }
        }

        /// <summary>
        ///     Add a parameter to the request.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        public void AddParameter(string name, object? value)
        {
            if (value == null)
            {
                return;
            }

            _parameters.Add(name, value);
        }

        /// <summary>
        ///     Add a URL segment to the request.
        /// </summary>
        /// <param name="name">Name of segment.</param>
        /// <param name="value">Value of segment.</param>
        public void AddUrlSegment(string name, string value)
        {
            _urlSegments.Add(name, value);
        }

        /// <summary>
        ///     Execute the request.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public async Task<T> Execute<T>(Client client) where T : new()
        {
            BuildRequest();
            return await client.Execute<T>(this, RootElement);
        }

        /// <summary>
        ///     Execute the request.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Execute(Client client)
        {
            BuildRequest();
            return await client.Execute(this);
        }

        /// <summary>
        ///     Build the client and prepare request parameters.
        /// </summary>
        /// <returns>An EasyPost.Client instance.</returns>
        private void BuildRequest()
        {
            BuildParameters();
            BuildUrlSegments();
        }

        /// <summary>
        ///     Build the request URL segments.
        /// </summary>
        private void BuildUrlSegments()
        {
            foreach (KeyValuePair<string, object> segment in _urlSegments)
            {
                _restRequest.AddUrlSegment(segment.Key, Convert.ToString(segment.Value) ?? string.Empty);
            }
        }

        /// <summary>
        ///     Build the request parameters.
        /// </summary>
        private void BuildParameters()
        {
            if (_parameters.Count == 0)
            {
                return;
            }

            switch (_restRequest.Method)
            {
                case Method.Get:
                case Method.Delete:
                    BuildQueryParameters();
                    break;
                case Method.Post:
                case Method.Put:
                    BuildBodyParameters();
                    break;
                case Method.Head:
                case Method.Options:
                case Method.Merge:
                case Method.Copy:
                case Method.Search:
                case Method.Patch:
                default:
                    break;

            }
        }

        /// <summary>
        ///     Build request body.
        /// </summary>
        private void BuildBodyParameters()
        {
            string body = JsonSerialization.ConvertObjectToJson(_parameters) ?? string.Empty;
            _restRequest.AddStringBody(body, ContentType.Json);
        }

        /// <summary>
        ///     Build request query parameters.
        /// </summary>
        private void BuildQueryParameters()
        {
            foreach (KeyValuePair<string, object> pair in _parameters)
            {
                _restRequest.AddParameter(pair.Key, pair.Value, ParameterType.QueryString);
            }
        }

        public static explicit operator RestRequest(Request request) => request._restRequest;
    }
}
