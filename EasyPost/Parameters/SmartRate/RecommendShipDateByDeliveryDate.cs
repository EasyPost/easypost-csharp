using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.SmartRate
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.Beta.SmartRateService.RecommendShipDateByDeliveryDate(string, RecommendShipDateByDeliveryDate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RecommendShipDateByDeliveryDate : BaseParameters<Models.API.RateWithTimeInTransitDetailsByDeliveryDate>
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
