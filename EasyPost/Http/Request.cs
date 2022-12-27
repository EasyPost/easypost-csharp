using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using EasyPost._base;
using EasyPost.Utilities;

namespace EasyPost.Http
{
    internal class Request
    {
        private readonly HttpRequestMessage _requestMessage;

        private readonly Utilities.Http.Method _method;

        private readonly Dictionary<string, object> _parameters;

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        internal Request(string domain, string endpoint, Utilities.Http.Method method, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, Dictionary<string, string>? headers = null, string? rootElement = null)
        {
            _method = method;

            Uri url = new Uri($"{domain}/{apiVersion.Value}/{endpoint}");
            _requestMessage = new HttpRequestMessage(_method.HttpMethod, url);
            _parameters = parameters ?? new Dictionary<string, object>();
        }

        internal HttpRequestMessage AsHttpRequestMessage()
        {
            // wait until the last possible moment to set the content
            BuildParameters();

            return _requestMessage;
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

            var @switch = new SwitchCase
            {
                // equality of two HttpMethod objects falls back to their inner strings, so we can compare these objects directly
                { Utilities.Http.Method.Get.HttpMethod, BuildQueryParameters },
                { Utilities.Http.Method.Delete.HttpMethod, BuildQueryParameters },
                { Utilities.Http.Method.Post.HttpMethod, BuildBodyParameters },
                { Utilities.Http.Method.Put.HttpMethod, BuildBodyParameters },
                { Utilities.Http.Method.Patch.HttpMethod, BuildBodyParameters }
            };

            @switch.MatchFirst(_method.HttpMethod);
        }

        /// <summary>
        ///     Build request body.
        /// </summary>
        private void BuildBodyParameters()
        {
            string body = JsonSerialization.ConvertObjectToJson(_parameters);
            _requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
        }

        /// <summary>
        ///     Build request query parameters.
        /// </summary>
        private void BuildQueryParameters()
        {
            // add query parameters
            var query = new StringBuilder();
            foreach (KeyValuePair<string, object> pair in _parameters)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (pair.Value == null)
                {
                    continue;
                }

                query.Append($"{pair.Key}={pair.Value}&");
            }

            // remove last '&'
            query.Remove(query.Length - 1, 1);

            // add query to request uri
            _requestMessage.RequestUri = new Uri($"{_requestMessage.RequestUri}?{query}");
        }
    }
}
