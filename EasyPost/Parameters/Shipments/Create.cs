using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipments
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IShipmentParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Whether or not to include a carbon offset fee in the new <see cref="Models.API.Shipment"/>'s cost.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carbon_offset")]
        // "carbon_offset" is not included when shipment creation parameters are used in a non-shipment creation request.
        public bool CarbonOffset { get; set; } = false; // non-nullable, will always be included (default: false)

        /// <summary>
        ///     <see cref="Models.API.CustomsInfo"/> (or a <see cref="Parameters.CustomsInfo.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "customs_info")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "customs_info")]
        public ICustomsInfoParameter? CustomsInfo { get; set; }

        /// <summary>
        ///     An insurance value for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "insurance")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "insurance")]
        public double? Insurance { get; set; }

        /// <summary>
        ///     Whether the new <see cref="Models.API.Shipment"/> is a return.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "is_return")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "is_return")]
        public bool? IsReturn { get; set; }

        /// <summary>
        ///     Additional <see cref="Models.API.Options"/> for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "options")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "options")]
        public Models.API.Options? Options { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     A list of <see cref="Models.API.TaxIdentifier"/>s (or <see cref="Parameters.TaxIdentifiers.Create"/> parameter sets) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "tax_identifiers")]
        public List<ITaxIdentifierParameter>? TaxIdentifiers { get; set; }

        /// <summary>
        ///     The destination <see cref="Models.API.Address"/> (or a <see cref="Parameters.Addresses.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     The origin <see cref="Models.API.Address"/> (or a <see cref="Parameters.Addresses.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     The physical <see cref="Models.API.Parcel"/> (or <see cref="Parameters.Parcels.Create"/> parameter set) being transported in the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "parcel")]
        public IParcelParameter? Parcel { get; set; }

        /// <summary>
        ///     The service level for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "service")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "service")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "service")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "service")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "service")]
        public string? Service { get; set; }

        /// <summary>
        ///     The carrier for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "carrier")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "carrier")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "carrier")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     IDs of the <see cref="Models.API.CarrierAccount"/>s to use to create the new <see cref="Models.API.Shipment"/>.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(Batches.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(ScanForms.Create), Necessity.Optional, "carrier_accounts")]
        public List<string>? CarrierAccountIds { get; set; }

        #endregion
    }
}
