using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

/// <summary>
///     Class representing estimated transit times for a <see cref="RateWithEstimatedDeliveryDate"/>.
/// </summary>
[Obsolete("This class will be removed in a future version and replaced with TimeInTransitDetailsForShipDate.")]
public class TimeInTransitDetails
{
    #region JSON Properties

    /// <summary>
    ///     Confidence levels for days in transit estimates.
    /// </summary>
    [JsonProperty("days_in_transit")]
    public TimeInTransit? DaysInTransit { get; set; }

    /// <summary>
    ///     EasyPost's estimated delivery date for the associated <see cref="RateWithEstimatedDeliveryDate"/>.
    /// </summary>
    [JsonProperty("easypost_estimated_delivery_date")]
    public DateTime? EasyPostEstimatedDeliveryDate { get; set; }

    /// <summary>
    ///     The planned departure date for the shipment.
    /// </summary>
    [JsonProperty("planned_ship_date")]
    public DateTime? PlannedShipDate { get; set; }

    #endregion
}
