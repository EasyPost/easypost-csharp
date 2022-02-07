using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Scotch
{
    public class ReplayingHandler : DelegatingHandler
    {
        private string _cassettePath;

        public ReplayingHandler(HttpMessageHandler innerHandler, string cassettePath)
        {
            base.InnerHandler = innerHandler;
            _cassettePath = cassettePath;
        }

        public ReplayingHandler(string cassettePath)
        {
            base.InnerHandler = new HttpClientHandler();
            _cassettePath = cassettePath;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var interactions = Cassette.ReadCassette(_cassettePath);
            var receivedRequest = await Helpers.ToRequestAsync(request);

            HttpInteraction matchedInteraction = null;

            try
            {
                matchedInteraction = interactions.First(i => Helpers.RequestsMatch(receivedRequest, i.Request));
            }
            catch (InvalidOperationException)
            {
                throw new VCRException($"No interaction found for request {receivedRequest.Method} {receivedRequest.URI}");
            }

            var matchedResponse = matchedInteraction.Response;
            var responseMessage = matchedResponse.ToHttpResponseMessage(request);
            return responseMessage;
        }
    }
}
