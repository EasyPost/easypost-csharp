using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EasyPost.Exceptions.General;

namespace EasyPost.Utilities
{
    public static class Rates
    {
        /// <summary>
        ///     Get the lowest rate from this list.
        /// </summary>
        /// <param name="rates">List of rates to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static EasyPost.Models.API.Rate GetLowest(this IEnumerable<EasyPost.Models.API.Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => GetLowestRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);

        /// <summary>
        ///     Get the lowest rate from a list of <see cref="EasyPost.Models.API.Rate"/>s.
        /// </summary>
        /// <param name="rates">List of rates to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static EasyPost.Models.API.Rate GetLowestRate(IEnumerable<EasyPost.Models.API.Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            includeCarriers ??= new List<string>();
            excludeCarriers ??= new List<string>();
            includeServices ??= new List<string>();
            excludeServices ??= new List<string>();

            includeCarriers = includeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            excludeCarriers = excludeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            includeServices = includeServices.Select(s => s.ToLowerInvariant()).ToList();
            excludeServices = excludeServices.Select(s => s.ToLowerInvariant()).ToList();

            EasyPost.Models.API.Rate? lowestRate = null;

            foreach (EasyPost.Models.API.Rate rate in rates)
            {
                if (includeCarriers.Count > 0 || excludeCarriers.Count > 0)
                {
                    // we have a carrier filter

                    // rate will always have a carrier, don't need to check for null
                    if (includeCarriers.Count > 0 && !includeCarriers.Contains(rate.Carrier!.ToLowerInvariant()))
                    {
                        // If we have a list of carriers to include and the rate's carrier isn't in the list, skip it
                        continue;
                    }

                    if (excludeCarriers.Contains(rate.Carrier!.ToLowerInvariant()))
                    {
                        // If the rate's carrier is in the list of carriers to exclude, skip it
                        continue;
                    }
                }

                if (includeServices.Count > 0 || excludeServices.Count > 0)
                {
                    // we have a service filter

                    // rate will always have a service, don't need to check for null
                    if (includeServices.Count > 0 && !includeServices.Contains(rate.Service!.ToLowerInvariant()))
                    {
                        // If we have a list of services to include and the rate's service isn't in the list, skip it
                        continue;
                    }

                    if (excludeServices.Contains(rate.Service!.ToLowerInvariant()))
                    {
                        // If the rate's service is in the list of services to exclude, skip it
                        continue;
                    }
                }

                if (lowestRate == null)
                {
                    // if lowest rate is null, set it to this rate
                    lowestRate = rate;
                    continue;
                }

                if (rate.Price == null || lowestRate.Price == null)
                {
                    // if the rate or lowest rate doesn't have a price, throw an exception
                    throw new FilteringError(Constants.ErrorMessages.NullObjectComparison);
                }

                float rateValue = float.Parse(rate.Price, NumberStyles.Any, CultureInfo.InvariantCulture);
                float lowestRateValue = float.Parse(lowestRate.Price, NumberStyles.Any, CultureInfo.InvariantCulture);

                if (rateValue >= lowestRateValue)
                {
                    // if this rate is greater than or equal to the lowest rate, skip it
                    continue;
                }

                // if we made it this far, this rate is the lowest rate, set it to the lowest rate
                lowestRate = rate;
            }

            if (lowestRate == null)
            {
                // if we didn't find a rate, throw an exception
                throw new FilteringError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.NoObjectFound, "rates"));
            }

            // return the lowest rate
            return lowestRate;
        }

        /// <summary>
        ///     Get the lowest rate from this list.
        /// </summary>
        /// <param name="rates">List of rates to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static EasyPost.Models.API.Beta.StatelessRate GetLowest(this IEnumerable<EasyPost.Models.API.Beta.StatelessRate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => GetLowestStatelessRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);

        /// <summary>
        ///     Get the lowest rate from a list of <see cref="EasyPost.Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        /// <param name="rates">List of rates to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static EasyPost.Models.API.Beta.StatelessRate GetLowestStatelessRate(IEnumerable<EasyPost.Models.API.Beta.StatelessRate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            includeCarriers ??= new List<string>();
            excludeCarriers ??= new List<string>();
            includeServices ??= new List<string>();
            excludeServices ??= new List<string>();

            includeCarriers = includeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            excludeCarriers = excludeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            includeServices = includeServices.Select(s => s.ToLowerInvariant()).ToList();
            excludeServices = excludeServices.Select(s => s.ToLowerInvariant()).ToList();

            EasyPost.Models.API.Beta.StatelessRate? lowestRate = null;

            foreach (EasyPost.Models.API.Beta.StatelessRate rate in rates)
            {
                if (includeCarriers.Count > 0 || excludeCarriers.Count > 0)
                {
                    // we have a carrier filter

                    // rate will always have a carrier, don't need to check for null
                    if (includeCarriers.Count > 0 && !includeCarriers.Contains(rate.Carrier!.ToLowerInvariant()))
                    {
                        // If we have a list of carriers to include and the rate's carrier isn't in the list, skip it
                        continue;
                    }

                    if (excludeCarriers.Contains(rate.Carrier!.ToLowerInvariant()))
                    {
                        // If the rate's carrier is in the list of carriers to exclude, skip it
                        continue;
                    }
                }

                if (includeServices.Count > 0 || excludeServices.Count > 0)
                {
                    // we have a service filter

                    // rate will always have a service, don't need to check for null
                    if (includeServices.Count > 0 && !includeServices.Contains(rate.Service!.ToLowerInvariant()))
                    {
                        // If we have a list of services to include and the rate's service isn't in the list, skip it
                        continue;
                    }

                    if (excludeServices.Contains(rate.Service!.ToLowerInvariant()))
                    {
                        // If the rate's service is in the list of services to exclude, skip it
                        continue;
                    }
                }

                if (lowestRate == null)
                {
                    // if lowest rate is null, set it to this rate
                    lowestRate = rate;
                    continue;
                }

                if (rate.Price == null || lowestRate.Price == null)
                {
                    // if the rate or lowest rate doesn't have a price, throw an exception
                    throw new FilteringError(Constants.ErrorMessages.NullObjectComparison);
                }

                float rateValue = float.Parse(rate.Price, NumberStyles.Any, CultureInfo.InvariantCulture);
                float lowestRateValue = float.Parse(lowestRate.Price, NumberStyles.Any, CultureInfo.InvariantCulture);

                if (rateValue >= lowestRateValue)
                {
                    // if this rate is greater than or equal to the lowest rate, skip it
                    continue;
                }

                // if we made it this far, this rate is the lowest rate, set it to the lowest rate
                lowestRate = rate;
            }

            if (lowestRate == null)
            {
                // if we didn't find a rate, throw an exception
                throw new FilteringError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.NoObjectFound, "rates"));
            }

            // return the lowest rate
            return lowestRate;
        }

        /// <summary>
        ///     Get the lowest SmartRate from this list.
        /// </summary>
        /// <param name="smartRates">List of smartRates to parse.</param>
        /// <param name="deliveryDays">Delivery days to include in the filter.</param>
        /// <param name="deliveryAccuracy">Delivery accuracy to include in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static EasyPost.Models.API.Smartrate GetLowest(this IEnumerable<EasyPost.Models.API.Smartrate> smartRates, int deliveryDays, EasyPost.Models.API.SmartrateAccuracy deliveryAccuracy) => GetLowestSmartRate(smartRates, deliveryDays, deliveryAccuracy);

        /// <summary>
        ///     Get the lowest SmartRate from a list of <see cref="EasyPost.Models.API.Smartrate"/>s.
        /// </summary>
        /// <param name="smartRates">List of SmartRates to parse.</param>
        /// <param name="deliveryDays">Delivery days to include in the filter.</param>
        /// <param name="deliveryAccuracy">Delivery accuracy to include in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static EasyPost.Models.API.Smartrate GetLowestSmartRate(IEnumerable<EasyPost.Models.API.Smartrate> smartRates, int deliveryDays, EasyPost.Models.API.SmartrateAccuracy deliveryAccuracy)
        {
            EasyPost.Models.API.Smartrate? lowestSmartRate = null;

            foreach (EasyPost.Models.API.Smartrate smartRate in smartRates)
            {
                // smartRate will always have a time in transit, don't need to check for null
                int? smartRateAccuracy = smartRate.TimeInTransit!.GetBySmartrateAccuracy(deliveryAccuracy);

                if (smartRateAccuracy == null)
                {
                    // If the smartRate doesn't have a time in transit for the specified accuracy, skip it
                    continue;
                }

                if (smartRateAccuracy > deliveryDays)
                {
                    // If the smartRate's time in transit is greater than the specified delivery days, skip it
                    continue;
                }

                if (lowestSmartRate == null)
                {
                    // if lowest smartRate is null, set it to this smartRate
                    lowestSmartRate = smartRate;
                    continue;
                }

                if (smartRate.Rate >= lowestSmartRate.Rate)
                {
                    // if this smartRate is greater than or equal to the lowest smartRate, skip it
                    continue;
                }

                // if we made it this far, this smartRate is the lowest smartRate, set it to the lowest smartRate
                lowestSmartRate = smartRate;
            }

            if (lowestSmartRate == null)
            {
                // if we didn't find a smartRate, throw an exception
                throw new FilteringError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.NoObjectFound, "smartrates"));
            }

            // return the lowest smartRate
            return lowestSmartRate;
        }
    }
}
