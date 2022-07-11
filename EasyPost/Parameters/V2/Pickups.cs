using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Pickups
    {
        public class Create : EasyPostParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class Buy : EasyPostParameters
        {
            [Parameter("carrier")]
            public string? Carrier { internal get; set; }

            [Parameter("service")]
            public string? Service { internal get; set; }

            public Buy(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
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
