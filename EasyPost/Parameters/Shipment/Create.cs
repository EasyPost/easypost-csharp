using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Shipment>, IShipmentParameter
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
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "customs_info")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "customs_info")]
        public ICustomsInfoParameter? CustomsInfo { get; set; }

        /// <summary>
        ///     An insurance value for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "insurance")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "insurance")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "insurance")]
        public double? Insurance { get; set; }

        /// <summary>
        ///     Whether the new <see cref="Models.API.Shipment"/> is a return.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "is_return")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "is_return")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "is_return")]
        public bool? IsReturn { get; set; }

        /// <summary>
        ///     Additional <see cref="Models.API.Options"/> for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "options")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "options")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "options")]
        public Models.API.Options? Options { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "reference")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     A list of <see cref="Models.API.TaxIdentifier"/>s (or <see cref="TaxIdentifier.Create"/> parameter sets) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "tax_identifiers")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "tax_identifiers")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "tax_identifiers")]
        public List<ITaxIdentifierParameter>? TaxIdentifiers { get; set; }

        /// <summary>
        ///     The destination <see cref="Models.API.Address"/> (or a <see cref="Address.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "to_address")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     The origin <see cref="Models.API.Address"/> (or a <see cref="Address.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "from_address")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     The physical <see cref="Models.API.Parcel"/> (or <see cref="Parcel.Create"/> parameter set) being transported in the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "parcel")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "parcel")]
        public IParcelParameter? Parcel { get; set; }

        /// <summary>
        ///     The service level for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "service")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "service")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "service")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "service")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "service")]
        public string? Service { get; set; }

        /// <summary>
        ///     The carrier for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "carrier")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "carrier")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "carrier")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     IDs of the <see cref="Models.API.CarrierAccount"/>s to use to create the new <see cref="Models.API.Shipment"/>.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(Batch.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "carrier_accounts")]
        [NestedRequestParameter(typeof(ScanForm.Create), Necessity.Optional, "carrier_accounts")]
        public List<string>? CarrierAccountIds { get; set; }

        #endregion
    }
}
