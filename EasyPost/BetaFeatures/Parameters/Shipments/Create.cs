using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ShipmentService.Create"/> API calls.
    /// </summary>
    public class Create : Parameters, IShipmentParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        public bool AddCarbonOffset { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "customs_info")]
        public EasyPost.Models.API.CustomsInfo? CustomsInfo { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "insurance")]
        public double Insurance { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "is_return")]
        public bool IsReturn { get; set; } = false;

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "options")]
        public EasyPost.Models.API.Options? Options { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
        public List<EasyPost.Models.API.TaxIdentifier>? TaxIdentifiers { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        public EasyPost.Models.API.Parcel? Parcel { get; set; }

        #endregion
    }
}
