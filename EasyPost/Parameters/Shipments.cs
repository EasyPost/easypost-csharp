using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Shipments
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "shipment")]
            public Models.API.Shipment? Shipment { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class GenerateRates : RequestParameters
        {
            // TODO: What are these parameters?

            public GenerateRates(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Insure : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "amount")]
            public double? Amount { internal get; set; }

            #endregion

            public Insure(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Label : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "file_format")]
            public string? FileFormat { internal get; set; }

            #endregion

            public Label(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Buy : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "insurance")]
            public string? InsuranceValue { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "rate", "id")]
            public string? RateId { internal get; set; }

            #endregion

            public Buy(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
