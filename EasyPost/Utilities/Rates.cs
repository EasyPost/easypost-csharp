using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using EasyPost.Exceptions.General;

namespace EasyPost.Utilities
{
    /// <summary>
    ///     Utility methods to filter rate lists.
    /// </summary>
#pragma warning disable CA1724 // Naming conflicts with Parameters.Beta.Rates
    public static class Rates
    {
        /// <summary>
        ///     Get the lowest <see cref="Models.API.Rate"/> from this list.
        /// </summary>
        /// <param name="rates">List of <see cref="Models.API.Rate"/>s to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest <see cref="Models.API.Rate"/> matching the filter.</returns>
        public static Models.API.Rate GetLowest(this IEnumerable<Models.API.Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => GetLowestRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);

        /// <summary>
        ///     Get the lowest <see cref="Models.API.Rate"/> from a list of <see cref="EasyPost.Models.API.Rate"/>s.
        /// </summary>
        /// <param name="rates">List of <see cref="Models.API.Rate"/>s to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest <see cref="Models.API.Rate"/> matching the filter.</returns>
        public static Models.API.Rate GetLowestRate(IEnumerable<Models.API.Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            includeCarriers ??= new List<string>();
            excludeCarriers ??= new List<string>();
            includeServices ??= new List<string>();
            excludeServices ??= new List<string>();

            includeCarriers = includeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            excludeCarriers = excludeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            includeServices = includeServices.Select(s => s.ToLowerInvariant()).ToList();
            excludeServices = excludeServices.Select(s => s.ToLowerInvariant()).ToList();

            Models.API.Rate? lowestRate = null;

            foreach (Models.API.Rate rate in rates)
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
        ///     Get the lowest <see cref="Models.API.Beta.StatelessRate"/> from this list.
        /// </summary>
        /// <param name="rates">List of <see cref="Models.API.Beta.StatelessRate"/>s to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest <see cref="Models.API.Beta.StatelessRate"/> matching the filter.</returns>
        public static Models.API.Beta.StatelessRate GetLowest(this IEnumerable<Models.API.Beta.StatelessRate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => GetLowestStatelessRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);

        /// <summary>
        ///     Get the lowest <see cref="Models.API.Beta.StatelessRate"/> from a list of <see cref="EasyPost.Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        /// <param name="rates">List of <see cref="Models.API.Beta.StatelessRate"/>s to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest <see cref="Models.API.Beta.StatelessRate"/> matching the filter.</returns>
        public static Models.API.Beta.StatelessRate GetLowestStatelessRate(IEnumerable<Models.API.Beta.StatelessRate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            includeCarriers ??= new List<string>();
            excludeCarriers ??= new List<string>();
            includeServices ??= new List<string>();
            excludeServices ??= new List<string>();

            includeCarriers = includeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            excludeCarriers = excludeCarriers.Select(c => c.ToLowerInvariant()).ToList();
            includeServices = includeServices.Select(s => s.ToLowerInvariant()).ToList();
            excludeServices = excludeServices.Select(s => s.ToLowerInvariant()).ToList();

            Models.API.Beta.StatelessRate? lowestRate = null;

            foreach (Models.API.Beta.StatelessRate rate in rates)
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
        ///     Get the lowest <see cref="Models.API.SmartRate"/> from this list.
        /// </summary>
        /// <param name="smartRates">List of <see cref="Models.API.SmartRate"/>s to parse.</param>
        /// <param name="deliveryDays">Delivery days to include in the filter.</param>
        /// <param name="deliveryAccuracy">Delivery accuracy to include in the filter.</param>
        /// <returns>Lowest <see cref="Models.API.SmartRate"/>s matching the filter.</returns>
        public static Models.API.SmartRate GetLowest(this IEnumerable<Models.API.SmartRate> smartRates, int deliveryDays, Models.API.SmartRateAccuracy deliveryAccuracy) => GetLowestSmartRate(smartRates, deliveryDays, deliveryAccuracy);

        /// <summary>
        ///     Get the lowest <see cref="Models.API.SmartRate"/> from a list of <see cref="EasyPost.Models.API.SmartRate"/>s.
        /// </summary>
        /// <param name="smartRates">List of <see cref="Models.API.SmartRate"/> to parse.</param>
        /// <param name="deliveryDays">Delivery days to include in the filter.</param>
        /// <param name="deliveryAccuracy">Delivery accuracy to include in the filter.</param>
        /// <returns>Lowest <see cref="Models.API.SmartRate"/> matching the filter.</returns>
        public static Models.API.SmartRate GetLowestSmartRate(IEnumerable<Models.API.SmartRate> smartRates, int deliveryDays, Models.API.SmartRateAccuracy deliveryAccuracy)
        {
            Models.API.SmartRate? lowestSmartRate = null;

            foreach (Models.API.SmartRate smartRate in smartRates)
            {
                // smartRate will always have a time in transit, don't need to check for null
                int? smartRateAccuracy = smartRate.TimeInTransit!.GetBySmartRateAccuracy(deliveryAccuracy);

                if (smartRateAccuracy == null)
                {
                    // If the SmartRate doesn't have a time in transit for the specified accuracy, skip it
                    continue;
                }

                if (smartRateAccuracy > deliveryDays)
                {
                    // If the SmartRate's time in transit is greater than the specified delivery days, skip it
                    continue;
                }

                if (lowestSmartRate == null)
                {
                    // if lowest SmartRate is null, set it to this SmartRate
                    lowestSmartRate = smartRate;
                    continue;
                }

                if (smartRate.Rate >= lowestSmartRate.Rate)
                {
                    // if this SmartRate is greater than or equal to the lowest SmartRate, skip it
                    continue;
                }

                // if we made it this far, this SmartRate is the lowest SmartRate, set it to the lowest SmartRate
                lowestSmartRate = smartRate;
            }

            if (lowestSmartRate == null)
            {
                // if we didn't find a SmartRate, throw an exception
                throw new FilteringError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.NoObjectFound, "smartRates"));
            }

            // return the lowest SmartRate
            return lowestSmartRate;
        }
    }
#pragma warning restore CA1724
}
