using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Webhooks
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#update-a-webhook">Parameters</a> for <see cref="EasyPost.Services.WebhookService.Update(string, Update, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The new secret value that will be used to generate a HMAC-SHA256 signature, included in the headers for each webhook payload event.
        ///     Use this to verify an incoming webhook is from EasyPost, via <see cref="Services.WebhookService.ValidateWebhook(byte[], Dictionary{string,object}, string)"/>
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "webhook_secret")]
        public string? Secret { get; set; }

        #endregion
    }
}
