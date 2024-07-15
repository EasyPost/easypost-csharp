using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

/// <summary>
///     Class representing estimated transit times for a <see cref="EstimateDeliveryDateForZipPairResult"/> or <see cref="RateWithEstimatedDeliveryDate"/>.
/// </summary>
public class TimeInTransitDetailsForDeliveryDateEstimate
{
    #region JSON Properties

    /// <summary>
    ///     The planned departure date for the shipment.
    /// </summary>
    [JsonProperty("planned_ship_date")]
    public DateTime? PlannedShipDate { get; set; }

    /// <summary>
    ///     EasyPost's estimated delivery date for the associated <see cref="EstimateDeliveryDateForZipPairResult"/> or <see cref="RateWithEstimatedDeliveryDate"/>.
    /// </summary>
    [JsonProperty("easypost_estimated_delivery_date")]
    public DateTime? EasyPostEstimatedDeliveryDate { get; set; }

    /// <summary>
    ///     Expanded confidence levels for time in transit estimates.
    /// </summary>
    [JsonProperty("days_in_transit")]
    public TimeInTransit? DaysInTransit { get; set; }

    /// <summary>
    ///     Convert this object to a deprecated <see cref="TimeInTransitDetails"/> object.
    /// </summary>
    /// <returns>A <see cref="TimeInTransitDetails"/> object copy.</returns>
    internal TimeInTransitDetails? AsDeprecatedTimeInTransitDetails() => new TimeInTransitDetails
    {
        DaysInTransit = DaysInTransit,
        EasyPostEstimatedDeliveryDate = EasyPostEstimatedDeliveryDate,
        PlannedShipDate = PlannedShipDate,
    };

    #endregion
}
