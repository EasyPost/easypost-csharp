using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public class CarrierAccounts
    {
        public class Create : EasyPostParameters
        {
            [Parameter("address", "name")]
            public string? Name { internal get; set; }

            [Parameter("verify_strict")]
            public List<string>? ToStrictVerify { internal get; set; }

            [Parameter("verify")]
            public List<string>? ToVerify { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class Update : EasyPostParameters
        {
            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
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
