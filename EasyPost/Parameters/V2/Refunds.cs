using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Refunds
    {
        public class Create : EasyPostParameters
        {
            [Parameter("carrier")]
            public string? Carrier { internal get; set; }

            [Parameter("tracking_codes")]
            public List<string>? TrackingCodes { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }
    }
}
