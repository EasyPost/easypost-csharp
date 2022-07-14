using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Shipments
    {
        public sealed class Create : ApiParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public sealed class GenerateRates : ApiParameters
        {
            public GenerateRates(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public sealed class Insure : ApiParameters
        {
            [Parameter(Necessity.Required, "amount")]
            public double? Amount { internal get; set; }

            public Insure(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public sealed class Label : ApiParameters
        {
            [Parameter(Necessity.Required, "file_format")]
            public string? FileFormat { internal get; set; }

            public Label(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public sealed class Buy : ApiParameters
        {
            [Parameter(Necessity.Optional, "insurance")]
            public string? InsuranceValue { internal get; set; }
            [Parameter(Necessity.Required, "rate", "id")]
            public string? RateId { internal get; set; }

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
