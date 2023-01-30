using System;
using System.Collections.Generic;

namespace EasyPost.Calculation
{
    [Obsolete("This class is deprecated. Please use EasyPost.Utilities.Rates instead.", false)]
    public static class Rates
    {
        /// <summary>
        ///     Get the lowest rate from a list of rates.
        ///
        ///     Deprecated. Use <see cref="EasyPost.Utilities.Rates.GetLowestRate(IEnumerable{EasyPost.Models.API.Rate}, List{string}?, List{string}?, List{string}?, List{string}?)"/> instead.
        /// </summary>
        /// <param name="rates">List of rates to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        [Obsolete("This method is deprecated. Please use EasyPost.Utilities.Rates.GetLowestRate() instead. This method will be removed in a future version.", false)]
        public static EasyPost.Models.API.Rate GetLowestObjectRate(IEnumerable<EasyPost.Models.API.Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => Utilities.Rates.GetLowestRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);

        /// <summary>
        ///     Get the lowest smartRate from a list of rates.
        ///
        ///     Deprecated. Use <see cref="EasyPost.Utilities.Rates.GetLowestSmartRate(IEnumerable{EasyPost.Models.API.Smartrate}, int, EasyPost.Models.API.SmartrateAccuracy)"/> instead.
        /// </summary>
        /// <param name="smartrates">List of smartRates to parse.</param>
        /// <param name="deliveryDays">Delivery days to include in the filter.</param>
        /// <param name="deliveryAccuracy">Delivery accuracy to include in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        [Obsolete("This method is deprecated. Please use EasyPost.Utilities.Rates.GetLowestSmartRate() instead. This method will be removed in a future version.", false)]
        public static EasyPost.Models.API.Smartrate GetLowestShipmentSmartrate(IEnumerable<EasyPost.Models.API.Smartrate> smartrates, int deliveryDays, EasyPost.Models.API.SmartrateAccuracy deliveryAccuracy) => Utilities.Rates.GetLowestSmartRate(smartrates, deliveryDays, deliveryAccuracy);
    }
}
