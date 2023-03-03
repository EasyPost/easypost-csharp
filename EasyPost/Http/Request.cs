using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Utilities.Internal;
using RestSharp;
using RestSharp.Serializers;

namespace EasyPost.Http
{
    internal sealed class Request
    {
        public readonly string? RootElement;

        private readonly Dictionary<string, object> _parameters;
        private readonly RestRequest _restRequest;

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        public Request(string endpoint, Method method, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, string? rootElement = null)
        {
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
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (pair.Value == null)
                {
                    continue;
                }

                _restRequest.AddParameter(pair.Key, pair.Value, ParameterType.QueryString);
            }
        }

        public static explicit operator RestRequest(Request request) => request._restRequest;
    }
}
