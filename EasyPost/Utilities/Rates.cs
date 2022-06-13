using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Utilities
{
    public static class Rates
    {
        /// <summary>
        ///     Get the lowest rate from a list of rates
        /// </summary>
        /// <param name="rates">List of rates to parse.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static Rate GetLowestObjectRate(List<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            includeCarriers ??= new List<string>();
            excludeCarriers ??= new List<string>();
            includeServices ??= new List<string>();
            excludeServices ??= new List<string>();

            includeCarriers = includeCarriers.Select(c => c.ToLower()).ToList();
            excludeCarriers = excludeCarriers.Select(c => c.ToLower()).ToList();
            includeServices = includeServices.Select(s => s.ToLower()).ToList();
            excludeServices = excludeServices.Select(s => s.ToLower()).ToList();

            Rate? lowestRate = null;

            foreach (Rate rate in rates)
            {
                // If the rate's carrier is not in the include list, don't consider it
                if (includeCarriers.Count > 0 && !includeCarriers.Contains(rate.carrier.ToLower()))
                {
                    continue;
                }

                // If the rate's carrier is in the exclude list, don't consider it
                if (excludeCarriers.Count > 0 && excludeCarriers.Contains(rate.carrier.ToLower()))
                {
                    continue;
                }

                // If the rate's service is not in the include list, don't consider it
                if (includeServices.Count > 0 && !includeServices.Contains(rate.service.ToLower()))
                {
                    continue;
                }

                // If the rate's service is in the exclude list, don't consider it
                if (excludeServices.Count > 0 && excludeServices.Contains(rate.service.ToLower()))
                {
                    continue;
                }

                // if lowest rate is null, set it to this rate
                if (lowestRate == null)
                {
                    lowestRate = rate;
                    continue;
                }

                float rateValue = float.Parse(rate.rate);
                float lowestRateValue = float.Parse(lowestRate.rate);

                // if this rate is higher than or the same as the lowest rate, skip it
                if (lowestRateValue <= rateValue)
                {
                    continue;
                }

                lowestRate = rate;
            }

            if (lowestRate == null)
            {
                throw new FilterFailure("No rates found.");
            }

            return lowestRate;
        }

        public static Smartrate GetLowestShipmentSmartrate(List<Smartrate> smartrates, int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            Smartrate? lowestSmartrate = null;

            foreach (Smartrate smartrate in smartrates)
            {
                int? smartrateAccuracy = smartrate.time_in_transit.GetBySmartrateAccuracy(deliveryAccuracy);

                // this smartrate does not have a value for the delivery accuracy we're looking for
                if (smartrateAccuracy == null)
                {
                    continue;
                }

                // this smartrate is not within the accuracy range we're looking for
                if (smartrateAccuracy > deliveryDays)
                {
                    continue;
                }

                // if lowest smartrate is null, set it to this smartrate
                if (lowestSmartrate == null)
                {
                    lowestSmartrate = smartrate;
                    continue;
                }

                // if this smartrate is higher or the same as the lowest smartrate, skip it
                if (lowestSmartrate.rate <= smartrate.rate)
                {
                    continue;
                }

                lowestSmartrate = smartrate;
            }

            if (lowestSmartrate == null)
            {
                throw new FilterFailure("No smartrates found.");
            }

            return lowestSmartrate;
        }
    }
}
