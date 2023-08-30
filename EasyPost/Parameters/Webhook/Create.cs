using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Webhook
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-webhook">Parameters</a> for <see cref="EasyPost.Services.WebhookService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Webhook>, IWebhookParameter
    {
        #region Request Parameters

        /// <summary>
        ///     A secret value that will be used to generate a HMAC-SHA256 signature, included in the headers for each webhook payload event.
        ///     Use this to verify an incoming webhook is from EasyPost, via <see cref="Services.WebhookService.ValidateWebhook(byte[], Dictionary{string,object}, string)"/>
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "webhook_secret")]
        public string? Secret { get; set; }

        /// <summary>
        ///     The URL to receive webhook events at.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "url")]
        public string? Url { get; set; }

        #endregion
    }
}
