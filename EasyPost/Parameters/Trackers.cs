using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Trackers
    {
        public sealed class Create : RequestParameters
        {
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "tracker", "carrier")]
            public string? Carrier { internal get; set; }
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "tracker", "tracking_code")]
            public string? TrackingCode { internal get; set; }

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
