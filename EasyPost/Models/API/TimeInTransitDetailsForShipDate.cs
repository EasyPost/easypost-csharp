using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

/// <summary>
///     Class representing estimated transit times for a <see cref="EstimateDeliveryDateForShipmentResult"/>.
/// </summary>
public class TimeInTransitDetailsForShipDate
{
    #region JSON Properties

    /// <summary>
    ///     The planned departure date for the shipment.
    /// </summary>
    [JsonProperty("planned_ship_date")]
    public DateTime? PlannedShipDate { get; set; }

    /// <summary>
    ///     EasyPost's estimated delivery date for the associated <see cref="EstimateDeliveryDateForShipmentResult"/>.
    /// </summary>
    [JsonProperty("easypost_estimated_delivery_date")]
    public DateTime? EasyPostEstimatedDeliveryDate { get; set; }

    /// <summary>
    ///     Expanded confidence levels for time in transit estimates.
    /// </summary>
    [JsonProperty("days_in_transit")]
    public TimeInTransit? TimeInTransitPercentiles { get; set; }

    #endregion
}
