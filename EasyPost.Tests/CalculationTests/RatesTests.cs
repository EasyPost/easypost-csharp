using System.Collections.Generic;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.CalculationTests
{
    public class RatesTests
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestGetLowestRateObject()
        {
            List<Rate> rates = new()
            {
                new Rate { Price = "100.00" },
                new Rate { Price = "1.00" }
            };

            Rate lowestRate = Utilities.Rates.GetLowestRate(rates);
            Assert.Equal("1.00", lowestRate.Price);
        }

        [Fact]
        [Testing.Function]
        public void TestGetLowestShipmentSmartRate()
        {
            List<SmartRate> rates = new()
            {
                new SmartRate
                {
                    Rate = 100.00,
                    DeliveryDays = 1,
                    TimeInTransit = new TimeInTransit { Percentile50 = 1 }
                },
                new SmartRate
                {
                    Rate = 1.00,
                    DeliveryDays = 4,
                    TimeInTransit = new TimeInTransit { Percentile50 = 4 }
                }
            };

            SmartRate lowestRate = Utilities.Rates.GetLowestSmartRate(rates, 1, SmartRateAccuracy.Percentile50);
            Assert.Equal(100.00, lowestRate.Rate); // Will choose the $100 rate because it is the only rate that meets the delivery days and accuracy requirement
        }

        [Fact]
        [Testing.Logic]
        public void TestLowestRateFilters()
        {
            List<Rate> rates = new()
            {
                new Rate
                {
                    Price = "1.00",
                    Carrier = "CarrierA",
                    Service = "ServiceA"
                },
                new Rate
                {
                    Price = "1.00",
                    Carrier = "CarrierB",
                    Service = "ServiceB"
                },
                new Rate
                {
                    Price = "100.00",
                    Carrier = "CarrierB",
                    Service = "ServiceB"
                },
                new Rate
                {
                    Price = "100.00",
                    Carrier = "CarrierC",
                    Service = "ServiceC"
                }
            };

            // With no filters, it should go with the first entry with the lowest price
            Rate lowestRate = Utilities.Rates.GetLowestRate(rates);
            Assert.Equal("1.00", lowestRate.Price);
            Assert.Equal("CarrierA", lowestRate.Carrier);

            // With a carrier filter, it should go with the first entry with the carrier and lowest price
            lowestRate = Utilities.Rates.GetLowestRate(rates, new List<string> { "CarrierB" });
            Assert.Equal("1.00", lowestRate.Price);
            Assert.Equal("CarrierB", lowestRate.Carrier);

            // With a service filter, it should go with the first entry with the service and lowest price
            lowestRate = Utilities.Rates.GetLowestRate(rates, includeServices: new List<string> { "ServiceB" });
            Assert.Equal("1.00", lowestRate.Price);
            Assert.Equal("ServiceB", lowestRate.Service);

            // With a carrier and service filter, it should go with the first entry with the carrier and service and lowest price
            lowestRate = Utilities.Rates.GetLowestRate(rates, new List<string> { "CarrierB" }, new List<string> { "ServiceB" });
            Assert.Equal("1.00", lowestRate.Price);
            Assert.Equal("CarrierB", lowestRate.Carrier);

            // It may go with a higher price if filters are involved
            lowestRate = Utilities.Rates.GetLowestRate(rates, new List<string> { "CarrierC" });
            Assert.Equal("100.00", lowestRate.Price);
            Assert.Equal("CarrierC", lowestRate.Carrier);

            lowestRate = Utilities.Rates.GetLowestRate(rates, includeServices: new List<string> { "ServiceC" });
            Assert.Equal("100.00", lowestRate.Price);
            Assert.Equal("ServiceC", lowestRate.Service);
        }

        [Fact]
        [Testing.Exception]
        public void TestLowestRateFilteringError()
        {
            // Make a list of malformed rate objects
            // Problem is avoided if there is only one rate, since the check that count fail is skipped on the first element.
            // If there are multiple rates and anything after the first is malformed, will trigger an exception during comparison.
            List<Rate> rates = new()
            {
                new Rate { Price = "100.00" },
                new Rate { Price = null }
            };

            // Try to get the lowest rate from the list of malformed rates
            Assert.Throws<FilteringError>(() => Utilities.Rates.GetLowestRate(rates));
        }

        #endregion
    }
}
