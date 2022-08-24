using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Utilities;
using RestSharp;
using RestSharp.Serializers;

namespace EasyPost.Http
{
    internal class Request
    {
        public readonly string? RootElement;

        private readonly Dictionary<string, object> _parameters;
        private readonly RestRequest _restRequest;

        public Request(string endpoint, Method method, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null)
        {
            apiVersion ??= ApiVersion.General;
            endpoint = $"{apiVersion.Value}/{endpoint}";

            _restRequest = new RestRequest(endpoint, method);

            _parameters = parameters ?? new Dictionary<string, object>();

            RootElement = rootElement;
        }

        /// <summary>
        ///     Build the request parameters.
        /// </summary>
        internal void BuildParameters()
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
                case Method.Patch:
                    BuildBodyParameters();
                    break;
                case Method.Head:
                case Method.Options:
                case Method.Merge:
                case Method.Copy:
                case Method.Search:
                default:
                    break;
            }
        }

        /// <summary>
        ///     Build request body.
        /// </summary>
        private void BuildBodyParameters()
        {
            string body = JsonSerialization.ConvertObjectToJson(_parameters);
            _restRequest.AddStringBody(body, ContentType.Json);
        }

        /// <summary>
        ///     Build request query parameters.
        /// </summary>
        private void BuildQueryParameters()
        {
            foreach (KeyValuePair<string, object> pair in _parameters)
            {
                if (pair.Value == null)
                {
                    continue;
                }

                _restRequest.AddParameter(pair.Key, pair.Value, ParameterType.QueryString);
            }
        }

        public static explicit operator RestRequest(Request request) => request._restRequest;

        private static bool StatusCodeBetween(RestResponseBase response, int min, int max)
        {
            int statusCode = (int)response.StatusCode;
            return statusCode >= min && statusCode <= max;
        }
    }
}
