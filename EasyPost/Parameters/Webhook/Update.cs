using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Webhook
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/webhooks#update-a-webhook">Parameters</a> for <see cref="EasyPost.Services.WebhookService.Update(string, Update, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Update : BaseParameters<Models.API.Webhook>
    {
        #region Request Parameters

        /// <summary>
        ///     The new secret value that will be used to generate a HMAC-SHA256 signature, included in the headers for each webhook payload event.
        ///     Use this to verify an incoming webhook is from EasyPost, via <see cref="Services.WebhookService.ValidateWebhook(byte[], Dictionary{string,object}, string)"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "webhook_secret")]
        public string? Secret { get; set; }


        [TopLevelRequestParameter(Necessity.Optional, "custom_headers")]
        public List<WebhookCustomHeader>? CustomHeaders { get; set; }

        #endregion
    }
}
