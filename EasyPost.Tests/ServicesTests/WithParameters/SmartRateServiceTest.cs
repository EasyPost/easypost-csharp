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
        public async Task TestEstimateDeliveryDate()
        {
            UseVCR("estimate_delivery_date");

            Dictionary<string, object> address1Data = Fixtures.CaAddress1;
            Dictionary<string, object> address2Data = Fixtures.CaAddress2;
            Parameters.Address.Create address1Parameters = Fixtures.Parameters.Addresses.Create(address1Data);
            Parameters.Address.Create address2Parameters = Fixtures.Parameters.Addresses.Create(address2Data);

            Parameters.SmartRate.EstimateDeliveryDateForZipPair estimateDeliveryDateForZipPairParameters = new()
            {
                OriginPostalCode = address1Parameters.Zip,
                DestinationPostalCode = address2Parameters.Zip,
                PlannedShipDate = Fixtures.PlannedShipDate,
                Carriers = ["USPS", "FedEx", "UPS", "DHL"],
            };

            EstimateDeliveryDateForZipPairResult result = await Client.SmartRate.EstimateDeliveryDate(estimateDeliveryDateForZipPairParameters);

            Assert.Equal(result.OriginPostalCode, estimateDeliveryDateForZipPairParameters.OriginPostalCode);
            Assert.Equal(result.DestinationPostalCode, estimateDeliveryDateForZipPairParameters.DestinationPostalCode);
            Assert.Equal(result.PlannedShipDate, estimateDeliveryDateForZipPairParameters.PlannedShipDate);
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
        public async Task TestRecommendShipDate()
        {
            UseVCR("recommend_ship_date");

            Dictionary<string, object> address1Data = Fixtures.CaAddress1;
            Dictionary<string, object> address2Data = Fixtures.CaAddress2;
            Parameters.Address.Create address1Parameters = Fixtures.Parameters.Addresses.Create(address1Data);
            Parameters.Address.Create address2Parameters = Fixtures.Parameters.Addresses.Create(address2Data);

            Parameters.SmartRate.RecommendShipDateForZipPair recommendShipDateForZipPairParameters = new()
            {
                OriginPostalCode = address1Parameters.Zip,
                DestinationPostalCode = address2Parameters.Zip,
                DesiredDeliveryDate = Fixtures.DesiredDeliveryDate,
                Carriers = ["USPS", "FedEx", "UPS", "DHL"],
            };

            RecommendShipDateForZipPairResult result = await Client.SmartRate.RecommendShipDate(recommendShipDateForZipPairParameters);

            Assert.Equal(result.OriginPostalCode, recommendShipDateForZipPairParameters.OriginPostalCode);
            Assert.Equal(result.DestinationPostalCode, recommendShipDateForZipPairParameters.DestinationPostalCode);
            Assert.Equal(result.DesiredDeliveryDate, recommendShipDateForZipPairParameters.DesiredDeliveryDate);
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
