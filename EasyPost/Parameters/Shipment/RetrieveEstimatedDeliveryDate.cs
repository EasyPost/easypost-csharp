using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/shipments/shipping-smartrate#delivery-date">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.RetrieveEstimatedDeliveryDate(string, RetrieveEstimatedDeliveryDate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RetrieveEstimatedDeliveryDate : BaseParameters<Models.API.Shipment>
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
