using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.Beta
{
    public static class EndShippers
    {
        public class Update : EasyPostParameters
        {
            [Parameter("name")]
            public string? Name { internal get; set; }

            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class Create : EasyPostParameters
        {
            [Parameter("name")]
            public string? Name { internal get; set; }

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
