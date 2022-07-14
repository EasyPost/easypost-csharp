using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Billing
    {
        public sealed class Fund : RequestParameters
        {
            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "amount")]
            public string? Amount { internal get; set; }

            public Fund(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
