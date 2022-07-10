using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Addresses
    {
        public class Create : EasyPostParameters
        {
            [Parameter("name")]
            public string? Name { get; set; }

            public Create(Dictionary<string, object>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object> ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class All : EasyPostParameters
        {
            [Parameter("name")]
            public string? Name { get; set; }

            public All(Dictionary<string, object>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object> ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }
    }
}
