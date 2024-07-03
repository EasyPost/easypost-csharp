using System.Diagnostics.CodeAnalysis;
using EasyPost.Services;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.SmartRate
{
    /// <summary>
    ///     Parameters for <see cref="SmartRateService.EstimateDeliveryDateForShipment(string, EstimateDeliveryDateForShipment, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EstimateDeliveryDateForShipment : BaseParameters<Models.API.EstimateDeliveryDateForShipmentResult>
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
