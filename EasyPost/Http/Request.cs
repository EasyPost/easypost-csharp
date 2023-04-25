using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using EasyPost._base;
using EasyPost.Utilities.Internal;

namespace EasyPost.Http
{
    // TODO: Make disposable and dispose of the request message
#pragma warning disable CA1001
    internal sealed class Request
#pragma warning restore CA1001
    {
        private readonly HttpRequestMessage _requestMessage;

        private readonly Dictionary<string, object> _parameters;

        private readonly Http.Method _method;

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        internal Request(string domain, string endpoint, Method method, ApiVersion apiVersion, Dictionary<string, object>? parameters = null, Dictionary<string, string>? headers = null)
        {
            // store method for later use
            _method = method;

            // build the request URL, then request message
            Uri url = new($"{domain}/{apiVersion.Value}/{endpoint}");
            _requestMessage = new HttpRequestMessage(_method.HttpMethod, url);

            // store parameters for later use
            _parameters = parameters ?? new Dictionary<string, object>();

            // set the headers (user-agent, auth, and content-type should have been included in the headers dictionary)
            if (headers == null) return;
            foreach (KeyValuePair<string, string> header in headers)
            {
                _requestMessage.Headers.Add(header.Key, header.Value);
            }
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
                { Http.Method.Get.HttpMethod, BuildQueryParameters },
                { Http.Method.Delete.HttpMethod, BuildQueryParameters },
                { Http.Method.Post.HttpMethod, BuildBodyParameters },
                { Http.Method.Put.HttpMethod, BuildBodyParameters },
                { Http.Method.Patch.HttpMethod, BuildBodyParameters },
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
