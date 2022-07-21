using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Webhook : Resource
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? disabled_at { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }

        #endregion

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete()
        {
            Request request = new Request("webhooks/{id}", Method.Delete);
            request.AddUrlSegment("id", id);
            return await request.Execute();
        }

        /// <summary>
        ///     Update a Webhook. A disabled webhook will be enabled.
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the webhook with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     * { "webhook_secret", string } Secret token to include as a header when sending a webhook.
        ///     All invalid keys will be ignored.
        /// </param>
        /// </summary>
        public async Task Update(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("webhooks/{id}", Method.Patch);
            request.AddUrlSegment("id", id);
            request.AddParameters(parameters);

            Merge(await request.Execute<Webhook>());
        }


        /// <summary>
        ///     Get a list of webhooks.
        /// </summary>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        public static async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("webhooks", Method.Get);
            request.AddParameters(parameters);

            WebhookList webhookList = await request.Execute<WebhookList>();
            return webhookList.webhooks;
        }

        /// <summary>
        ///     Create a Webhook.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the webhook with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Webhook instance.</returns>
        public static async Task<Webhook> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("webhooks", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "webhook", parameters
                }
            });

            return await request.Execute<Webhook>();
        }

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<Webhook> Retrieve(string id)
        {
            Request request = new Request("webhooks/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Webhook>();
        }

        public static Event ValidateWebhook(byte[] data, Dictionary<string, object?> headers, string secret)
        {
            // Extract the signature from the headers
            string? providedSignature = headers["X-Hmac-Signature"]?.ToString();

            if (providedSignature == null)
            {
                throw new Exception("Webhook received does not contain an HMAC signature.");
            }

            // https://stackoverflow.com/a/12253723/13343799
            // ReSharper disable once CommentTypo
            string computedHexDigest = data.CalculateHMACSHA256HexDigest(secret, NormalizationForm.FormKD); // normalize with NFKD profile

            // Add prefix to the hex digest.
            string computedHashSignature = $"hmac-sha256-hex={computedHexDigest}";

            // compare the computed signature with the provided signature
            if (!Cryptography.SignaturesMatch(computedHashSignature, providedSignature))
            {
                throw new Exception("Webhook received did not originate from EasyPost or had a webhook secret mismatch.");
            }

            return JsonSerialization.ConvertJsonToObject<Event>(data.AsString());
        }
    }
}
