using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scotch
{
    public class Request
    {
        public string Method { get; set; }
        public string URI { get; set; }
        public IDictionary<string, string> RequestHeaders { get; set; }
        public IDictionary<string, string> ContentHeaders { get; set; }
        public string Body { get; set; }
    }

    public class Status
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }

    public class Response
    {
        public Status Status { get; set; }
        public IDictionary<string, string> ResponseHeaders { get; set; }
        public IDictionary<string, string> ContentHeaders { get; set; }
        public string Body { get; set; }

        public Version HttpVersion { get; set; }

        public HttpResponseMessage ToHttpResponseMessage(HttpRequestMessage requestMessage)
        {
            var result = new HttpResponseMessage(Status.Code);
            result.ReasonPhrase = Status.Message;
            result.Version = HttpVersion;
            foreach (var h in ResponseHeaders)
            {
                result.Headers.TryAddWithoutValidation(h.Key, h.Value.ToString());
            }

            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(Body));
            foreach (var h in ContentHeaders)
            {
                content.Headers.TryAddWithoutValidation(h.Key, h.Value.ToString());
            }

            result.Content = content;
            result.RequestMessage = requestMessage;
            return result;
        }
    }

    public class HttpInteraction
    {
        public Request Request { get; set; }
        public Response Response { get; set; }
        public DateTimeOffset RecordedAt { get; set; }
    }

    public static class Helpers
    {
        public static IDictionary<string, string> ToHeaders(HttpHeaders headers, List<string> headersToHide = null)
        {
            if (headersToHide == null)
            {
                headersToHide = new List<string>();
            }

            IDictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var h in headers)
            {
                if (headersToHide.Contains(h.Key))
                {
                    dict.Add(h.Key, "********");
                }
                else
                {
                    dict.Add(h.Key, string.Join(",", h.Value));
                }
            }

            return dict;
        }

        public static async Task<string> ToStringAsync(HttpContent content)
        {
            if (content == null)
            {
                return "";
            }

            return await content.ReadAsStringAsync();
        }

        public static IDictionary<string, string> ToContentHeaders(HttpContent content)
        {
            if (content == null)
            {
                return new Dictionary<string, string>();
            }

            return ToHeaders(content.Headers);
        }

        public static async Task<Request> ToRequestAsync(HttpRequestMessage request, List<string> headersToHide = null)
        {
            var requestBody = await ToStringAsync(request.Content);
            return new Request
            {
                Method = request.Method.ToString(),
                URI = request.RequestUri.ToString(),
                RequestHeaders = ToHeaders(request.Headers, headersToHide),
                ContentHeaders = ToContentHeaders(request.Content),
                Body = requestBody
            };
        }

        public static async Task<Response> ToResponseAsync(HttpResponseMessage response, List<string> headersToHide = null)
        {
            var responseBody = await ToStringAsync(response.Content);
            return new Response
            {
                Status = new Status
                {
                    Code = response.StatusCode,
                    Message = response.ReasonPhrase
                },
                ResponseHeaders = ToHeaders(response.Headers, headersToHide),
                ContentHeaders = ToContentHeaders(response.Content),
                Body = responseBody,
                HttpVersion = response.Version
            };
        }

        public static bool RequestsMatch(Request receivedRequest, Request recordedRequest)
        {
            return receivedRequest.Method.Equals(recordedRequest.Method, StringComparison.OrdinalIgnoreCase)
                   && receivedRequest.URI.Equals(recordedRequest.URI, StringComparison.OrdinalIgnoreCase);
        }
    }
}
