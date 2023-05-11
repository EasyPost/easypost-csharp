using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#retrieve-estimated-delivery-date-and-total-transit-days-across-all-rates-for-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.RetrieveEstimatedDeliveryDate(string, RetrieveEstimatedDeliveryDate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RetrieveEstimatedDeliveryDate : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The date the <see cref="Models.API.Shipment"/> is planned to ship.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "planned_ship_date")]
        public string? PlannedShipDate { get; set; }

        #endregion
    }
}
