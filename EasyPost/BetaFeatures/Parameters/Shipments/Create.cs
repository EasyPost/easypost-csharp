using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ShipmentService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IShipmentParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        // "carbon_offset" is not included when shipment creation parameters are used in a non-shipment creation request.
        public bool CarbonOffset { get; set; } = false; // non-nullable, will always be included (default: false)

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "carrier_accounts")]
        public List<EasyPost.Models.API.CarrierAccount>? CarrierAccounts { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "customs_info")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "customs_info")]
        public ICustomsInfoParameter? CustomsInfo { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "insurance")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "insurance")]
        public double? Insurance { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "is_return")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "is_return")]
        public bool? IsReturn { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "options")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "options")]
        public EasyPost.Models.API.Options? Options { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "tax_identifiers")]
        public List<ITaxIdentifierParameter>? TaxIdentifiers { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "parcel")]
        public IParcelParameter? Parcel { get; set; }

        #endregion
    }
}
