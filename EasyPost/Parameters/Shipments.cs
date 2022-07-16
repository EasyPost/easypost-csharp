using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API;

namespace EasyPost.Parameters
{
    public static class Shipments
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "reference")]
            public string? Reference { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "to_address")]
            public Address? ToAddress { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "from_address")]
            public Address? FromAddress { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "parcel")]
            public Parcel? Parcel { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
            public List<CarrierAccount>? CarrierAccounts { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "is_return")]
            public bool? IsReturn { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "customs_info")]
            public Models.API.CustomsInfo? CustomsInfo { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "options")]
            public Options? Options { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "service")]
            public string? Service { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
            public List<TaxIdentifier>? TaxIdentifiers { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }

        public sealed class GenerateRates : RequestParameters
        {
            // TODO: What are these parameters?

            public GenerateRates(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }

        public sealed class Insure : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "amount")]
            public decimal? Amount { internal get; set; }

            #endregion

            public Insure(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
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
        }

        public sealed class Buy : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "insurance")]
            public string? InsuranceValue { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "rate")]
            public Rate? Rate { internal get; set; }

            #endregion

            public Buy(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }
    }
}
