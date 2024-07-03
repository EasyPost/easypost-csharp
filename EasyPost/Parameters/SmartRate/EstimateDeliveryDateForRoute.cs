using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Services;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.SmartRate
{
    /// <summary>
    ///     Parameters for <see cref="SmartRateService.EstimateDeliveryDateForRoute(EstimateDeliveryDateForRoute, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EstimateDeliveryDateForRoute : BaseParameters<Models.API.EstimateDeliveryDateForShipmentResult>
    {
        #region Request Parameters

        /// <summary>
        ///     The origin postal code of the parcel(s).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "from_zip")]
        public string? OriginPostalCode { get; set; }

        /// <summary>
        ///     The destination postal code of the parcel(s).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "to_zip")]
        public string? DestinationPostalCode { get; set; }

        /// <summary>
        ///     The names of the carriers to estimate delivery dates for.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carriers")]
        public List<string>? Carriers { get; set; }

        /// <summary>
        ///     The date when the carrier would take possession of the parcel(s).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "planned_ship_date")]
        public string? PlannedShipDate { get; set; }

        /// <summary>
        ///     Whether to include potential Saturday delivery dates in the estimations.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "saturday_delivery")]
        public bool? SaturdayDelivery { get; set; }

        #endregion
    }
}
