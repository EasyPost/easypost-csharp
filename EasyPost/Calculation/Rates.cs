using System.Collections.Generic;
using System.Linq;
using EasyPost.Exceptions;
using EasyPost.Models.V2;

namespace EasyPost.Calculation
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
        public static Rate GetLowestObjectRate(IEnumerable<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
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

            foreach (Rate rate in from rate in rates where includeCarriers.Count <= 0 || includeCarriers.Contains(rate.carrier.ToLower()) where excludeCarriers.Count <= 0 || !excludeCarriers.Contains(rate.carrier.ToLower()) where includeServices.Count <= 0 || includeServices.Contains(rate.service.ToLower()) where excludeServices.Count <= 0 || !excludeServices.Contains(rate.service.ToLower()) select rate)
            {
                // if lowest rate is null, set it to this rate
                if (lowestRate == null)
                {
                    lowestRate = rate;
                    continue;
                }

                if (rate.rate == null || lowestRate.rate == null)
                {
                    throw new FilterFailure("Could not compare null elements.");
                }

                float rateValue = float.Parse(rate.rate);
                float lowestRateValue = float.Parse(lowestRate.rate);

                // if this rate is lower than the lowest rate, set it to this rate
                if (!(rateValue < lowestRateValue))
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

        /// <summary>
        ///     Get the lowest smartrate from a list of rates
        /// </summary>
        /// <param name="smartrates">List of smartrates to parse.</param>
        /// <param name="deliveryDays">Delivery days to include in the filter.</param>
        /// <param name="deliveryAccuracy">Delivery accuracy to include in the filter.</param>
        /// <returns>Lowest rate matching the filter.</returns>
        public static Smartrate GetLowestShipmentSmartrate(IEnumerable<Smartrate> smartrates, int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            Smartrate? lowestSmartrate = null;

            foreach (Smartrate? smartrate in from smartrate in smartrates let smartrateAccuracy = smartrate.time_in_transit.GetBySmartrateAccuracy(deliveryAccuracy) where smartrateAccuracy != null where !(smartrateAccuracy > deliveryDays) select smartrate)
            {
                // if lowest smartrate is null, set it to this smartrate
                if (lowestSmartrate == null)
                {
                    lowestSmartrate = smartrate;
                    continue;
                }

                // if this smartrate is lower than the lowest smartrate, set it to this smartrate
                if (!(smartrate.rate < lowestSmartrate.rate))
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