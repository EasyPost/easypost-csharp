using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public class CarrierAccounts
    {
        public class Create : ApiParameters
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

        public class Update : ApiParameters
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
