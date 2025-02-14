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
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/webhooks">webhook-related functionality</a>.
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
        ///     Create a <see cref="Webhook"/>.
        ///     <a href="https://docs.easypost.com/docs/webhooks#create-a-webhook">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Webhook"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Webhook"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Webhook> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("webhook");
            return await RequestAsync<Webhook>(Method.Post, "webhooks", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Webhook"/>.
        ///     <a href="https://docs.easypost.com/docs/webhooks#create-a-webhook">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Webhook"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Webhook"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Webhook> Create(Parameters.Webhook.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Webhook>(Method.Post, "webhooks", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="Webhook"/>s.
        ///     <a href="https://docs.easypost.com/docs/webhooks#retrieve-all-webhooks">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Webhook"/>s returned.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="Webhook"/> instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default) => await RequestAsync<List<Webhook>>(Method.Get, "webhooks", cancellationToken, parameters, "webhooks");

        /// <summary>
        ///     List all <see cref="Webhook"/>s.
        ///     <a href="https://docs.easypost.com/docs/webhooks#retrieve-all-webhooks">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Webhook"/>s returned.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="Webhook"/> instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Webhook>> All(Parameters.Webhook.All parameters, CancellationToken cancellationToken = default) => await RequestAsync<List<Webhook>>(Method.Get, "webhooks", cancellationToken, parameters.ToDictionary(), "webhooks");

        /// <summary>
        ///     Retrieve a <see cref="Webhook"/>.
        ///     <a href="https://docs.easypost.com/docs/webhooks#retrieve-a-webhook">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Webhook"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The <see cref="Webhook"/>.</returns>
        [CrudOperations.Read]
        public async Task<Webhook> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Webhook>(Method.Get, $"webhooks/{id}", cancellationToken);

        /// <summary>
        ///     Enable a disabled <see cref="Webhook"/> or alter its secret.
        ///     <a href="https://docs.easypost.com/docs/webhooks#update-a-webhook">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Webhook"/> to update.</param>
        /// <param name="parameters">Data to update <see cref="Webhook"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Webhook"/>.</returns>
        [CrudOperations.Update]
        public async Task<Webhook> Update(string id, Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            parameters = parameters?.Wrap("webhook");
            return await RequestAsync<Webhook>(Method.Put, $"webhooks/{id}", cancellationToken, parameters);
        }

        /// <summary>
        ///     Enable a disabled <see cref="Webhook"/> or alter its secret.
        ///     <a href="https://docs.easypost.com/docs/webhooks#update-a-webhook">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Webhook"/> to update.</param>
        /// <param name="parameters">Optional data to update <see cref="Webhook"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Webhook"/>.</returns>
        [CrudOperations.Update]
        public async Task<Webhook> Update(string id, Parameters.Webhook.Update? parameters = null, CancellationToken cancellationToken = default)
        {
            // TODO: Validate, do we need to wrap the params here too or is it implicitly done somehow?
            return await RequestAsync<Webhook>(Method.Put, $"webhooks/{id}", cancellationToken, parameters?.ToDictionary());
        }

        /// <summary>
        ///     Delete a <see cref="Webhook"/>.
        ///     <a href="https://docs.easypost.com/docs/webhooks#delete-a-webhook">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Webhook"/> to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>None.</returns>
        [CrudOperations.Delete]
        public async Task Delete(string id, CancellationToken cancellationToken = default) => await RequestAsync(Method.Delete, $"webhooks/{id}", cancellationToken);

        /// <summary>
        ///     Validate a received webhook's HMAC signature.
        ///     <a href="https://docs.easypost.com/docs/webhooks#hmac-validation">Related API documentation</a>.
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
