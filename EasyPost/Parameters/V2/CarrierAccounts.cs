using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public class CarrierAccounts
    {
        public class Create : EasyPostParameters
        {
            [Parameter(Necessity.Optional, "address", "name")]
            public string? Name { internal get; set; }

            [Parameter(Necessity.Optional, "verify_strict")]
            public List<string>? ToStrictVerify { internal get; set; }

            [Parameter(Necessity.Optional, "verify")]
            public List<string>? ToVerify { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public class Update : EasyPostParameters
        {
            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
