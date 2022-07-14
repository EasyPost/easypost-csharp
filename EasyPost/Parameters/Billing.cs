using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Billing
    {
        public sealed class Fund : ApiParameters
        {
            [Parameter(Necessity.Required, "amount")]
            public string? Amount { internal get; set; }

            public Fund(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
