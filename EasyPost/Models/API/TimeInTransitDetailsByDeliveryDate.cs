using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

/// <summary>
///     Class representing estimated transit times for a <see cref="RateWithTimeInTransitDetailsByDeliveryDate"/>.
/// </summary>
public class TimeInTransitDetailsByDeliveryDate
{
    #region JSON Properties

    /// <summary>
    ///     The desired delivery date for the shipment for the associated <see cref="RateWithTimeInTransitDetailsByDeliveryDate"/>.
    /// </summary>
    [JsonProperty("desired_delivery_date")]
    public DateTime? DesiredDeliveryDate { get; set; }

    /// <summary>
    ///     EasyPost's recommended ship date for the shipment to arrive by the <see cref="DesiredDeliveryDate"/>.
    /// </summary>
    [JsonProperty("ship_on_date")]
    public DateTime? EasyPostRecommendedShipDate { get; set; }

    /// <summary>
    ///     Confidence level for the <see cref="EasyPostRecommendedShipDate"/>.
    /// </summary>
    [JsonProperty("delivery_date_confidence")]
    public float? DeliveryDateConfidence { get; set; }

    /// <summary>
    ///     The likelihood that the shipment will be late if shipped on the <see cref="EasyPostRecommendedShipDate"/>.
    /// </summary>
    [JsonProperty("likelihood_shipment_is_late")]
    public float? LikelihoodShipmentIsLate { get; set; }

    /// <summary>
    ///     The likelihood that the shipment will be early if shipped on the <see cref="EasyPostRecommendedShipDate"/>.
    /// </summary>
    [JsonProperty("likelihood_shipment_is_early")]
    public float? LikelihoodShipmentIsEarly { get; set; }

    /// <summary>
    ///     The estimated days in transit if shipped on the <see cref="EasyPostRecommendedShipDate"/>.
    /// </summary>
    [JsonProperty("estimated_transit_days")]
    public int? EstimatedTransitDays { get; set; }

    /// <summary>
    ///     Expanded confidence levels for time in transit estimates.
    /// </summary>
    [JsonProperty("days_in_transit")]
    public TimeInTransit? TimeInTransitPercentiles { get; set; }

    #endregion
}
