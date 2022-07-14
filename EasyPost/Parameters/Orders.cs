using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Orders
    {
        public sealed class Create : RequestParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Buy : RequestParameters
        {
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "carrier")]
            public string? Carrier { internal get; set; }
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "service")]
            public string? Service { internal get; set; }

            public Buy(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
