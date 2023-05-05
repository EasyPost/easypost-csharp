using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#webhooks">webhook-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebhookService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WebhookService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
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
        public async Task<Webhook> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("webhook");
            return await RequestAsync<Webhook>(Method.Post, "webhooks", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Webhook"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Webhooks.Create"/> parameter set.</param>
        /// <returns><see cref="Webhook"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Webhook> Create(BetaFeatures.Parameters.Webhooks.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Webhook>(Method.Post, "webhooks", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <param name="parameters">A optional dictionary of parameters to include in the API request.</param>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default) => await RequestAsync<List<Webhook>>(Method.Get, "webhooks", cancellationToken, parameters, "webhooks");

        /// <summary>
        ///     List all <see cref="Webhook"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Webhooks.All"/> parameter set.</param>
        /// <returns>List of <see cref="Webhook"/> instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(BetaFeatures.Parameters.Webhooks.All parameters, CancellationToken cancellationToken = default) => await RequestAsync<List<Webhook>>(Method.Get, "webhooks", cancellationToken, parameters.ToDictionary(), "webhooks");

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<Webhook> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Webhook>(Method.Get, $"webhooks/{id}", cancellationToken);

        /// <summary>
        ///     Update a Webhook. A disabled webhook will be enabled.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the webhook with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     * { "webhook_secret", string } Secret token to include as a header when sending a webhook.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>The updated Webhook.</returns>
        [CrudOperations.Update]
        public async Task<Webhook> Update(string id, Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Webhook>(Method.Put, $"webhooks/{id}", cancellationToken, parameters);
        }

        /// <summary>
        ///     Update this <see cref="Webhook"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Webhooks.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="Webhook"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Webhook> Update(string id, BetaFeatures.Parameters.Webhooks.Update parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Webhook>(Method.Put, $"webhooks/{id}", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete(string id, CancellationToken cancellationToken = default) => await RequestAsync(Method.Delete, $"webhooks/{id}", cancellationToken);

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
        public Event ValidateWebhook(byte[] data, Dictionary<string, object?> headers, string secret)
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
