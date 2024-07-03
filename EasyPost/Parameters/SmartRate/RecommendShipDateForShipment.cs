using System.Diagnostics.CodeAnalysis;
using EasyPost.Services;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.SmartRate
{
    /// <summary>
    ///     Parameters for <see cref="SmartRateService.RecommendShipDateForShipment(string, RecommendShipDateForShipment, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RecommendShipDateForShipment : BaseParameters<Models.API.RecommendShipDateForShipmentResult>
    {
        #region Request Parameters

        /// <summary>
        ///     The desired date the <see cref="Models.API.Shipment"/> should be delivered.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "desired_delivery_date")]
        public string? DesiredDeliveryDate { get; set; }

        #endregion
    }
}
