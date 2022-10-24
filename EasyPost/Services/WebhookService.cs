using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebhookService : EasyPostService
    {
        internal WebhookService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Webhook.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the webhook with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Webhook instance.</returns>
        [CrudOperations.Create]
        public async Task<Webhook> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("webhook");
            return await Create<Webhook>("webhooks", parameters);
        }

        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null)
        {
            return await List<List<Webhook>>("webhooks", parameters, "webhooks");
        }

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<Webhook> Retrieve(string? id)
        {
            return await Get<Webhook>($"webhooks/{id}");
        }

        /// <summary>
        ///     Validate a received webhook's HMAC signature.
        /// </summary>
        /// <param name="data">Byte data of the received webhook request.</param>
        /// <param name="headers">Dictionary of headers from the received webhook request.</param>
        /// <param name="secret">Secret used to sign webhooks.</param>
        /// <returns>An EasyPost.Event instance if webhook is valid.</returns>
        /// <exception cref="SignatureVerificationError">If webhook has an invalid signature.</exception>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        // users could technically access this method without using a Client object, but we want to discourage that.
        public Event ValidateWebhook(byte[] data, Dictionary<string, object?> headers, string secret)
        {
            const string signatureHeader = "X-Hmac-Signature";

            string? providedSignature;
            if (headers.ContainsKey(signatureHeader))
            {
                providedSignature = headers[signatureHeader]?.ToString();
            }
            else
            {
                throw new SignatureVerificationError();
            }

            string computedHexDigest = data.CalculateHMACSHA256HexDigest(secret, NormalizationForm.FormKD);

            string computedHashSignature = $"hmac-sha256-hex={computedHexDigest}";

            if (!Cryptography.SignaturesMatch(computedHashSignature, providedSignature))
            {
                throw new SignatureVerificationError();
            }

            return JsonSerialization.ConvertJsonToObject<Event>(data.AsString());
        }

        #endregion
    }
}
