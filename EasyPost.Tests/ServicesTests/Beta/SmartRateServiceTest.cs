using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.Beta
{
    public class SmartRateServiceTests : UnitTest
    {
        public SmartRateServiceTests() : base("beta_smartrate_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetSmartRates()
        {
            UseVCR("get_smart_rates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Assert.NotNull(shipment.Rates);

            List<SmartRate> smartRates = await Client.Beta.SmartRate.GetSmartRates(shipment.Id);
            SmartRate smartRate = smartRates.First();
            // Must compare IDs because one is a Rate object and one is a SmartRate object
            Assert.Equal(shipment.Rates[0].Id, smartRate.Id);
            Assert.NotNull(smartRate.TimeInTransit.Percentile50);
            Assert.NotNull(smartRate.TimeInTransit.Percentile75);
            Assert.NotNull(smartRate.TimeInTransit.Percentile85);
            Assert.NotNull(smartRate.TimeInTransit.Percentile90);
            Assert.NotNull(smartRate.TimeInTransit.Percentile95);
            Assert.NotNull(smartRate.TimeInTransit.Percentile97);
            Assert.NotNull(smartRate.TimeInTransit.Percentile99);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestEstimateDeliveryDateByShipDate()
        {
            UseVCR("estimated_delivery_date_by_ship_date");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            List<RateWithTimeInTransitDetailsByShipDate> ratesWithEstimatedDeliveryDates = await Client.Beta.SmartRate.EstimateDeliveryDateByShipDate(shipment.Id, Fixtures.PlannedShipDate);

            foreach (var rate in ratesWithEstimatedDeliveryDates)
            {
                Assert.NotNull(rate.TimeInTransitDetails);
                Assert.NotNull(rate.TimeInTransitDetails.EasyPostEstimatedDeliveryDate);
                Assert.NotNull(rate.TimeInTransitDetails.TimeInTransitPercentiles);
                Assert.NotNull(rate.TimeInTransitDetails.PlannedShipDate);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRecommendedShipDateByDeliveryDate()
        {
            UseVCR("recommended_ship_date_by_delivery_date");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            List<RateWithTimeInTransitDetailsByDeliveryDate> ratesWithEstimatedDeliveryDates = await Client.Beta.SmartRate.RecommendShipDateByDeliveryDate(shipment.Id, Fixtures.DesiredDeliveryDate);

            foreach (var rate in ratesWithEstimatedDeliveryDates)
            {
                Assert.NotNull(rate.TimeInTransitDetails);
                Assert.NotNull(rate.TimeInTransitDetails.EasyPostRecommendedShipDate);
                Assert.NotNull(rate.TimeInTransitDetails.TimeInTransitPercentiles);
                Assert.NotNull(rate.TimeInTransitDetails.DesiredDeliveryDate);
            }
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestLowestSmartRateFiltering()
        {
            // Mock rates since these can change from the API and we want to test the local filtering logic, not the API call
            // API call is tested in TestGetSmartRates
            List<SmartRate> smartRates = new List<SmartRate>
            {
                new SmartRate
                {
                    Service = "Priority",
                    Carrier = "USPS",
                    Rate = 1.00, // this rate is cheaper but doesn't meet the filters
                    TimeInTransit = new TimeInTransit
                    {
                        Percentile90 = 3,
                    },
                },
                new SmartRate
                {
                    Service = "First",
                    Carrier = "USPS",
                    Rate = 6.07,
                    TimeInTransit = new TimeInTransit
                    {
                        Percentile90 = 2,
                    },
                },
            };

            // test lowest SmartRate with valid filters
            SmartRate lowestSmartRate = Utilities.Rates.GetLowestSmartRate(smartRates, 2, SmartRateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartRate.Service);
            Assert.Equal(6.07, lowestSmartRate.Rate);
            Assert.Equal("USPS", lowestSmartRate.Carrier);

            // test lowest SmartRate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<FilteringError>(() => Task.FromResult(Utilities.Rates.GetLowestSmartRate(smartRates, 0, SmartRateAccuracy.Percentile90)));

            // test lowest SmartRate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        #endregion
    }
}
