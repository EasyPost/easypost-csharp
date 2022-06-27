using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost.Tests.Services
{
    internal class WebhookTestService
    {
        private const string BaseUrl = "https://webhook.site";

        private readonly TestUtils.VCR _vcr;
        private string _webhookId;

        internal string WebhookUrl
        {
            get { return $"{BaseUrl}/{_webhookId}"; }
        }

        private string WebhookTokenUrl => $"{BaseUrl}/token/{_webhookId}";

        internal WebhookTestService(TestUtils.VCR vcr)
        {
            _vcr = vcr;
        }

        /// <summary>
        ///     Get the latest webhook event on the webhook endpoint.
        /// </summary>
        /// <returns>Payload of the latest event.</returns>
        internal async Task<Dictionary<string, object>> GetLatestEvent()
        {
            List<Dictionary<string, object>> requests = await GetRequests();
            return requests == null ? new Dictionary<string, object>() : requests[requests.Count - 1];
        }

        /// <summary>
        ///     Delete all stored webhook events.
        /// </summary>
        internal async Task Reset()
        {
            RestRequest request = new RestRequest($"{WebhookTokenUrl}/request", Method.Delete);
            await new RestClient().ExecuteAsync(request);
        }

        internal async Task SetUp()
        {
            RestRequest request = new RestRequest($"{BaseUrl}/token", Method.Post);
            RestResponse response = await new RestClient(_vcr.Client).ExecuteAsync(request);
            Dictionary<string, object> data = JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>(response.Content);
            _webhookId = data["uuid"].ToString();
        }

        /// <summary>
        ///     Get all events on the webhook endpoint.
        /// </summary>
        /// <returns>List of payloads from all events.</returns>
        private async Task<List<Dictionary<string, object>>> GetRequests()
        {
            RestRequest request = new RestRequest($"{WebhookTokenUrl}/requests");
            RestResponse response = await new RestClient(_vcr.Client).ExecuteAsync(request);
            return JsonSerialization.ConvertJsonToObject<List<Dictionary<string, object>>>(response.Content, null, new List<string>()
            {
                "data"
            });
        }

        internal static Dictionary<string, object> GetHeadersForWebhook(Dictionary<string, object> webhook)
        {
            return JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>(webhook["headers"].ToString());
        }
    }
}
