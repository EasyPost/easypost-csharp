using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class WebhookService : Service
    {
        internal WebhookService(BaseClient client) : base(client)
        {
        }


        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        public async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null)
        {
            return await List<List<Webhook>>("webhooks", parameters);
        }

        /// <summary>
        ///     Create a Webhook.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Webhook instance.</returns>
        public async Task<Webhook> Create(Dictionary<string, object> parameters)
        {
            return await Create<Webhook>("webhooks", new Dictionary<string, object>
            {
                {
                    "webhook", parameters
                }
            });
        }

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public async Task<Webhook> Retrieve(string id)
        {
            return await Get<Webhook>($"webhooks/{id}");
        }
    }
}
