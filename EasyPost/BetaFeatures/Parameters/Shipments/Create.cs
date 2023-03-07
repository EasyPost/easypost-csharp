using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ShipmentService.Create"/> API calls.
    /// </summary>
    public class Create : BaseParameters, IShipmentParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]

        // non-nullable, will always be included (default: false)
        public bool CarbonOffset { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        public List<ICarrierAccountParameter>? CarrierAccounts { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "customs_info")]
        public ICustomsInfoParameter? CustomsInfo { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "insurance")]
        public double? Insurance { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "is_return")]
        public bool IsReturn { get; set; } = false;

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "options")]
        public EasyPost.Models.API.Options? Options { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        public string? Reference { get; set; }

        // TODO: How is a user supposed to create this object?
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
        public List<EasyPost.Models.API.TaxIdentifier>? TaxIdentifiers { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        public IParcelParameter? Parcel { get; set; }

        #endregion
    }
}
