using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Refunds
    {
        public sealed class Create : ApiParameters
        {
            [Parameter(Necessity.Required, "carrier")]
            public string? Carrier { internal get; set; }

            [Parameter(Necessity.Required, "tracking_codes")]
            public List<string>? TrackingCodes { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
