using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class SmartRateServiceTests : UnitTest
    {
        public SmartRateServiceTests() : base("smartrate_service_with_parameters")
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

            List<SmartRate> smartRates = await Client.SmartRate.GetSmartRates(shipment.Id);
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
        public async Task TestEstimateDeliveryDateForShipment()
        {
            UseVCR("estimate_delivery_date_by_ship_date");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Parameters.SmartRate.EstimateDeliveryDateForShipment estimateDeliveryDateForShipmentParameters = new()
            {
                PlannedShipDate = Fixtures.PlannedShipDate,
            };

            List<EstimateDeliveryDateForShipmentResult> ratesWithEstimatedDeliveryDates = await Client.SmartRate.EstimateDeliveryDateForShipment(shipment.Id, estimateDeliveryDateForShipmentParameters);

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
        public async Task TestRecommendShipDateForShipment()
        {
            UseVCR("recommend_ship_date_by_delivery_date");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Parameters.SmartRate.RecommendShipDateForShipment recommendShipDateForShipmentParameters = new()
            {
                DesiredDeliveryDate = Fixtures.DesiredDeliveryDate,
            };

            List<RecommendShipDateForShipmentResult> ratesWithEstimatedDeliveryDates = await Client.SmartRate.RecommendShipDateForShipment(shipment.Id, recommendShipDateForShipmentParameters);

            foreach (var rate in ratesWithEstimatedDeliveryDates)
            {
                Assert.NotNull(rate.TimeInTransitDetails);
                Assert.NotNull(rate.TimeInTransitDetails.EasyPostRecommendedShipDate);
                Assert.NotNull(rate.TimeInTransitDetails.TimeInTransitPercentiles);
                Assert.NotNull(rate.TimeInTransitDetails.DesiredDeliveryDate);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestEstimateDeliveryDateForRoute()
        {
            UseVCR("estimate_delivery_date_by_route");

            Dictionary<string, object> address1Data = Fixtures.CaAddress1;
            Dictionary<string, object> address2Data = Fixtures.CaAddress2;
            Parameters.Address.Create address1Parameters = Fixtures.Parameters.Addresses.Create(address1Data);
            Parameters.Address.Create address2Parameters = Fixtures.Parameters.Addresses.Create(address2Data);

            Parameters.SmartRate.EstimateDeliveryDateForRoute estimateDeliveryDateForRouteParameters = new()
            {
                OriginPostalCode = address1Parameters.Zip,
                DestinationPostalCode = address2Parameters.Zip,
                PlannedShipDate = Fixtures.PlannedShipDate,
                Carriers = ["USPS", "FedEx", "UPS", "DHL"],
            };

            EstimateDeliveryDateForRouteResult result = await Client.SmartRate.EstimateDeliveryDateForRoute(estimateDeliveryDateForRouteParameters);

            Assert.Equal(result.OriginPostalCode, estimateDeliveryDateForRouteParameters.OriginPostalCode);
            Assert.Equal(result.DestinationPostalCode, estimateDeliveryDateForRouteParameters.DestinationPostalCode);
            Assert.Equal(result.PlannedShipDate, estimateDeliveryDateForRouteParameters.PlannedShipDate);
            Assert.NotNull(result.Estimates);
            Assert.NotEmpty(result.Estimates);
            foreach (var estimate in result.Estimates)
            {
                Assert.NotNull(estimate.Carrier);
                Assert.NotNull(estimate.Service);
                Assert.NotNull(estimate.EasyPostTimeInTransitData);
                Assert.NotNull(estimate.EasyPostTimeInTransitData.TimeInTransitPercentiles);
                Assert.NotNull(estimate.EasyPostTimeInTransitData.TimeInTransitPercentiles.Percentile75);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRecommendShipDateForRoute()
        {
            UseVCR("recommend_ship_date_by_route");

            Dictionary<string, object> address1Data = Fixtures.CaAddress1;
            Dictionary<string, object> address2Data = Fixtures.CaAddress2;
            Parameters.Address.Create address1Parameters = Fixtures.Parameters.Addresses.Create(address1Data);
            Parameters.Address.Create address2Parameters = Fixtures.Parameters.Addresses.Create(address2Data);

            Parameters.SmartRate.RecommendShipDateForRoute recommendShipDateForRouteParameters = new()
            {
                OriginPostalCode = address1Parameters.Zip,
                DestinationPostalCode = address2Parameters.Zip,
                DesiredDeliveryDate = Fixtures.DesiredDeliveryDate,
                Carriers = ["USPS", "FedEx", "UPS", "DHL"],
            };

            RecommendShipDateForRouteResult result = await Client.SmartRate.RecommendShipDateForRoute(recommendShipDateForRouteParameters);

            Assert.Equal(result.OriginPostalCode, recommendShipDateForRouteParameters.OriginPostalCode);
            Assert.Equal(result.DestinationPostalCode, recommendShipDateForRouteParameters.DestinationPostalCode);
            Assert.Equal(result.DesiredDeliveryDate, recommendShipDateForRouteParameters.DesiredDeliveryDate);
            Assert.NotNull(result.Estimates);
            Assert.NotEmpty(result.Estimates);
            foreach (var estimate in result.Estimates)
            {
                Assert.NotNull(estimate.Carrier);
                Assert.NotNull(estimate.Service);
                Assert.NotNull(estimate.EasyPostTimeInTransitData);
                Assert.NotNull(estimate.EasyPostTimeInTransitData.TimeInTransitPercentiles);
                Assert.NotNull(estimate.EasyPostTimeInTransitData.TimeInTransitPercentiles.Percentile75);
            }
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestLowestSmartRateFiltering()
        {
            /***
             * Mock rates since these can change from the API and we want to test the local filtering logic, not the API call.
             * The API call is tested in <see cref="TestGetSmartRates"/>
             */
            List<SmartRate> smartRates =
            [
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
                }

            ];

            // test lowest SmartRate with valid filters
            SmartRate lowestSmartRate = Utilities.Rates.GetLowestSmartRate(smartRates, 2, SmartRateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartRate.Service);
            Assert.Equal(6.07, lowestSmartRate.Rate);
            Assert.Equal("USPS", lowestSmartRate.Carrier);

            // test lowest SmartRate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<FilteringError>(() => Task.FromResult(Utilities.Rates.GetLowestSmartRate(smartRates, 0, SmartRateAccuracy.Percentile90)));
        }

        #endregion
    }
}
