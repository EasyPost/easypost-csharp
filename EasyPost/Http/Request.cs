using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Web;
using EasyPost._base;
using EasyPost.Utilities.Internal;

namespace EasyPost.Http
{
#pragma warning disable CA1001
    internal class Request : IDisposable
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
            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

            // build query string from parameters
            foreach (KeyValuePair<string, object> param in _parameters)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (param.Value == null)
                {
                    continue;
                }

                query[param.Key] = param.Value switch
                {
                    // TODO: Handle special conversions for other types
                    // DateTime dateTime => dateTime.ToString("o", CultureInfo.InvariantCulture),
                    var _ => param.Value.ToString(),
                };
            }

            // short circuit if no query parameters
            if (query.Count == 0)
            {
                return;
            }

            // rebuild the request URL with the query string appended
            var uriBuilder = new UriBuilder(_requestMessage.RequestUri!)
            {
                Query = query.ToString(),
            };
            _requestMessage.RequestUri = new Uri(uriBuilder.ToString());
        }
        
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                // Dispose managed state (managed objects).

                // Dispose the request message
                _requestMessage.Dispose();
            }

            // Free native resources (unmanaged objects) and override a finalizer below.
            _isDisposed = true;
        }

        ~Request()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(disposing: false);
        }
    }
}
