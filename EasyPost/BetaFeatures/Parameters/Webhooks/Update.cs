using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Webhooks
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.Webhook.Update"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "webhook_secret")]
        public string? Secret { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "url")]
        public string? Url { get; set; }

        #endregion
    }
}
