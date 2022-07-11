using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Shipments
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

        public class GenerateRates : EasyPostParameters
        {
            public GenerateRates(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class Insure : EasyPostParameters
        {
            [Parameter("amount")]
            public double? Amount { internal get; set; }

            public Insure(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class Label : EasyPostParameters
        {
            [Parameter("file_format")]
            public string? FileFormat { internal get; set; }

            public Label(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
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
            [Parameter("insurance")]
            public string? InsuranceValue { internal get; set; }
            [Parameter("rate", "id")]
            public string? RateId { internal get; set; }

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
