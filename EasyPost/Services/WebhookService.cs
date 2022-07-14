using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;

namespace EasyPost.Services
{
    public class WebhookService : EasyPostService
    {
        internal WebhookService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<List<Webhook>> All(All? parameters = null)
        {
            return await List<List<Webhook>>("webhooks", parameters, "webhooks");
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
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Webhook> Create(Webhooks.Create parameters)
        {
            return await Create<Webhook>("webhooks", parameters);
        }

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Webhook> Retrieve(string? id)
        {
            return await Get<Webhook>($"webhooks/{id}");
        }
    }
}
