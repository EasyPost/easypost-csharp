using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Webhooks
    {
        public sealed class Update : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "url")]
            public string? Url { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "webhook_secret")]
            public string? Secret { internal get; set; }

            #endregion

            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "url")]
            public string? Url { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "webhook_secret")]
            public string? Secret { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
