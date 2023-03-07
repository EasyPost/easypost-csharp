using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Webhooks
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.WebhookService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IWebhookParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "webhook_secret")]
        public string? Secret { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "url")]
        public string? Url { get; set; }

        #endregion
    }
}
