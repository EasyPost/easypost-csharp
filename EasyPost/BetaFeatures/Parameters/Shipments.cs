using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters
{
    public static class Shipments
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.ShipmentService.Create"/> API calls.
        /// </summary>
        public class Create : Parameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "carbon_offset")]
            public bool AddCarbonOffset { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
            public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "customs_info")]
            public EasyPost.Models.API.CustomsInfo? CustomsInfo { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "from_address")]
            public EasyPost.Models.API.Address? FromAddress { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "insurance")]
            public double Insurance { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "is_return")]
            public bool IsReturn { get; set; } = false;

            [RequestParameter(Necessity.Optional, "shipment", "options")]
            public EasyPost.Models.API.Options? Options { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "parcel")]
            public EasyPost.Models.API.Parcel? Parcel { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
            public List<EasyPost.Models.API.TaxIdentifier>? TaxIdentifiers { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "to_address")]
            public EasyPost.Models.API.Address? ToAddress { get; set; }

            [RequestParameter(Necessity.Optional, "shipment", "to_address")]

            // TODO: Fix placement of sub-parameters.
            // This will be placed at "shipment" -> "to_address", and then "address" -> ... because of the Address class's own serialization rules.
            // Needs instead to be "shipment" -> "to_address" -> ... because of the way the API expects it.
            public Addresses.Create? ToAddressParameters { get; set; }

            #endregion
        }
    }
}
