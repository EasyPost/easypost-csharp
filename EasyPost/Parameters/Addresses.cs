using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Addresses
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

        public class Verify : ApiParameters
        {
            [Parameter(Necessity.Optional, "carrier")]
            public string? Carrier { internal get; set; }

            public Verify(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
