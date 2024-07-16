using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

/// <summary>
///     Class representing estimated transit times for a <see cref="RecommendShipDateForShipmentResult"/>.
/// </summary>
public class TimeInTransitDetailsForShipDateRecommendation
{
    #region JSON Properties

    /// <summary>
    ///     The desired delivery date for the shipment for the associated <see cref="RecommendShipDateForShipmentResult"/>.
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
    ///     The estimated days in transit if shipped on the <see cref="EasyPostRecommendedShipDate"/>.
    /// </summary>
    [JsonProperty("estimated_transit_days")]
    public int? EstimatedTransitDays { get; set; }

    /// <summary>
    ///     Expanded confidence levels for time in transit estimates.
    /// </summary>
    [JsonProperty("days_in_transit")]
    public TimeInTransit? DaysInTransit { get; set; }

    #endregion
}
