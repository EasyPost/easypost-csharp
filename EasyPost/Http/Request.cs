using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Request(string endpoint, Method method, Dictionary<string, object>? parameters = null, string? rootElement = null)
        {
            _restRequest = new RestRequest(endpoint, method);

            _parameters = parameters ?? new Dictionary<string, object>();

            RootElement = rootElement;
        }

        /// <summary>
        ///     Build the request parameters.
        /// </summary>
        internal void Build()
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
