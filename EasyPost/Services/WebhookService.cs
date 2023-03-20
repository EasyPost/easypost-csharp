using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebhookService : EasyPostService
    {
        internal WebhookService(EasyPostClient client)
            : base(client)
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
        ///     Create a <see cref="Webhook"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Webhooks.Create"/> parameter set.</param>
        /// <returns><see cref="Webhook"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Webhook> Create(BetaFeatures.Parameters.Webhooks.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<Webhook>("webhooks", parameters.ToDictionary());
        }

        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <param name="parameters">A optional dictionary of parameters to include in the API request.</param>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null) => await List<List<Webhook>>("webhooks", parameters, "webhooks");

        /// <summary>
        ///     List all <see cref="Webhook"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Webhooks.All"/> parameter set.</param>
        /// <returns>List of <see cref="Webhook"/> instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(BetaFeatures.Parameters.Webhooks.All parameters) => await List<List<Webhook>>("webhooks", parameters.ToDictionary(), "webhooks");

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<Webhook> Retrieve(string? id) => await Get<Webhook>($"webhooks/{id}");

        /// <summary>
        ///     Validate a received webhook's HMAC signature.
        /// </summary>
        /// <param name="data">Byte data of the received webhook request.</param>
        /// <param name="headers">Dictionary of headers from the received webhook request.</param>
        /// <param name="secret">Secret used to sign webhooks.</param>
        /// <returns>An <see cref="Event"/> instance if webhook is valid.</returns>
        /// <exception cref="SignatureVerificationError">If webhook has an invalid signature.</exception>
        // ReSharper disable once MemberCanBeMadeStatic.Global
        // users could technically access this method without using a Client object, but we want to discourage that.
#pragma warning disable CA1822 // TODO: Will be altered when instance methods are removed.
        public Event ValidateWebhook(byte[] data, Dictionary<string, object?> headers, string secret)
#pragma warning restore CA1822
        {
            const string signatureHeader = "X-Hmac-Signature";

            string? providedSignature = headers.TryGetValue(signatureHeader, out object? value) ? value?.ToString() : throw new SignatureVerificationError();

            string computedHexDigest = data.CalculateHMACSHA256HexDigest(secret, NormalizationForm.FormKD);

            string computedHashSignature = $"hmac-sha256-hex={computedHexDigest}";

            return Cryptography.SignaturesMatch(computedHashSignature, providedSignature) ? JsonSerialization.ConvertJsonToObject<Event>(data.AsString()) : throw new SignatureVerificationError();
        }

        #endregion
    }
}
