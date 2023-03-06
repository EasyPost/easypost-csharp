using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Webhooks
{
    public class Create : BaseParameters, IWebhookParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "webhook_secret")]
        public string? Secret { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "url")]
        public string? Url { get; set; }

        #endregion
    }
}
