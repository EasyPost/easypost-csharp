using EasyPost.Utilities.Annotations;

namespace EasyPost.Beta.Parameters.V2
{
    public static class Webhooks
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.Webhook"/> update API calls.
        /// </summary>
        public sealed class Update : UpdateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "webhook_secret")]
            public string? Secret { get; set; }

            [RequestParameter(Necessity.Optional, "url")]
            public string? Url { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.WebhookService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "webhook_secret")]
            public string? Secret { get; set; }

            [RequestParameter(Necessity.Optional, "url")]
            public string? Url { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.WebhookService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
