using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Trackers
    {
        public class Create : EasyPostParameters
        {
            [Parameter(Necessity.Required, "tracker", "carrier")]
            public string? Carrier { internal get; set; }
            [Parameter(Necessity.Required, "tracker", "tracking_code")]
            public string? TrackingCode { internal get; set; }

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
