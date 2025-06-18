using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Luma
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/luma#standard-buy">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.BuyLuma(Buy, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    public class Buy : BaseParameters<Models.API.Shipment>
    {
        #region Request Parameters

        /// <summary>
        ///     The name of the ruleset to use for Luma.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "ruleset_name")]
        public string? RulesetName { get; set; }

        /// <summary>
        ///     The planned ship date for the shipment (YYYY-MM-DD).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "planned_ship_date")]
        public string? PlannedShipDate { get; set; }

        /// <summary>
        ///     The deliver by date for the shipment (YYYY-MM-DD).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "deliver_by_date")]
        public string? DeliverByDate { get; set; }

        /// <summary>
        ///     Whether to persist the label after purchase.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "persist_label")]
        public bool? PersistLabel { get; set; }

        #endregion
    }
}
