using System.Collections.Generic;
using System.Linq;
using EasyPost.Exceptions;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;

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

            foreach (Rate rate in rates)
            {
                if ((includeCarriers.Count > 0 || excludeCarriers.Count > 0) && rate.Carrier == null)
                {
                    // If we are filtering by carrier and the rate doesn't have a carrier, skip it
                    continue;
                }

                if ((includeServices.Count > 0 || excludeServices.Count > 0) && rate.Service == null)
                {
                    // If we are filtering by service and the rate doesn't have a service, skip it
                    continue;
                }

                if (includeCarriers.Count > 0 && !includeCarriers.Contains(rate.Carrier!.ToLower()))
                {
                    // If we have a list of carriers to include and the rate's carrier isn't in the list, skip it
                    continue;
                }

                if (excludeCarriers.Contains(rate.Carrier!.ToLower()))
                {
                    // If the rate's carrier is in the list of carriers to exclude, skip it
                    continue;
                }

                if (includeServices.Count > 0 && !includeServices.Contains(rate.Service!.ToLower()))
                {
                    // If we have a list of services to include and the rate's service isn't in the list, skip it
                    continue;
                }

                if (excludeServices.Contains(rate.Service!.ToLower()))
                {
                    // If the rate's service is in the list of services to exclude, skip it
                    continue;
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

                float rateValue = float.Parse(rate.Price);
                float lowestRateValue = float.Parse(lowestRate.Price);

                if (!(rateValue < lowestRateValue))
                {
                    // if this rate not lower than the lowest rate, skip it
                    continue;
                }

                // if we made it this far, this rate is the lowest rate, set it to the lowest rate
                lowestRate = rate;
            }

            if (lowestRate == null)
            {
                // if we didn't find a rate, throw an exception
                throw new FilteringError(string.Format(Constants.ErrorMessages.NoObjectFound, "rates"));
            }

            // return the lowest rate
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

            foreach (Smartrate? smartrate in smartrates)
            {
                if (smartrate.TimeInTransit == null)
                {
                    // If the smartrate doesn't have a time in transit, skip it
                    continue;
                }

                int? smartrateAccuracy = smartrate.TimeInTransit.GetBySmartrateAccuracy(deliveryAccuracy);

                if (smartrateAccuracy == null)
                {
                    // If the smartrate doesn't have a time in transit for the specified accuracy, skip it
                    continue;
                }

                if (smartrateAccuracy > deliveryDays)
                {
                    // If the smartrate's time in transit is greater than the specified delivery days, skip it
                    continue;
                }

                if (lowestSmartrate == null)
                {
                    // if lowest smartrate is null, set it to this smartrate
                    lowestSmartrate = smartrate;
                    continue;
                }

                if (!(smartrate.Rate < lowestSmartrate.Rate))
                {
                    // if this smartrate is not lower than the lowest smartrate, skip it
                    continue;
                }

                // if we made it this far, this smartrate is the lowest smartrate, set it to the lowest smartrate
                lowestSmartrate = smartrate;
            }

            if (lowestSmartrate == null)
            {
                // if we didn't find a smartrate, throw an exception
                throw new FilteringError(string.Format(Constants.ErrorMessages.NoObjectFound, "smartrates"));
            }

            // return the lowest smartrate
            return lowestSmartrate;
        }
    }
}
