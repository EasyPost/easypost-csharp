using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Luma
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/luma#one-call-buy">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.CreateAndBuyLuma(Buy, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateAndBuy : BaseParameters<Models.API.Shipment>
    {
        #region Request Parameters

        /// <summary>
        ///     The destination <see cref="Models.API.Address"/> (or a <see cref="Address.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     The origin <see cref="Models.API.Address"/> (or a <see cref="Address.Create"/> parameter set) for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     The physical <see cref="Models.API.Parcel"/> (or <see cref="Parcel.Create"/> parameter set) being transported in the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        public IParcelParameter? Parcel { get; set; }

        /// <summary>
        ///     IDs of the <see cref="Models.API.CarrierAccount"/>s to use to create the new <see cref="Models.API.Shipment"/>.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        public List<string>? CarrierAccountIds { get; set; }

        /// <summary>
        ///     An insurance value for the new <see cref="Models.API.Shipment"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "insurance")]
        public double? Insurance { get; set; }

        /// <summary>
        ///     The name of the ruleset to use for Luma.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "shipment", "ruleset_name")]
        public string? RulesetName { get; set; }

        /// <summary>
        ///     The planned ship date for the shipment (YYYY-MM-DD).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "shipment", "planned_ship_date")]
        public string? PlannedShipDate { get; set; }

        /// <summary>
        ///     The deliver by date for the shipment (YYYY-MM-DD).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "deliver_by_date")]
        public string? DeliverByDate { get; set; }

        /// <summary>
        ///     Whether to persist the label after purchase.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "persist_label")]
        public bool? PersistLabel { get; set; }

        #endregion
    }
}
