using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Pickups
    {
        public class Create : ApiParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public class Buy : ApiParameters
        {
            [Parameter(Necessity.Required, "carrier")]
            public string? Carrier { internal get; set; }

            [Parameter(Necessity.Required, "service")]
            public string? Service { internal get; set; }

            public Buy(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
