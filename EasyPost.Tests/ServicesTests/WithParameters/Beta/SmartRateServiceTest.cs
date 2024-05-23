using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters.Beta
{
    public class SmartRateServiceTests : UnitTest
    {
        public SmartRateServiceTests() : base("beta_smartrate_service_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestEstimateDeliveryDateByShipDate()
        {
            UseVCR("estimated_delivery_date_by_ship_date");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Parameters.SmartRate.EstimateDeliveryDateByShipDate estimateDeliveryDateByShipDateParameters = new()
            {
                PlannedShipDate = Fixtures.PlannedShipDate,
            };

            List<RateWithTimeInTransitDetailsByShipDate> ratesWithEstimatedDeliveryDates = await Client.Beta.SmartRate.EstimateDeliveryDateByShipDate(shipment.Id, estimateDeliveryDateByShipDateParameters);

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

            Parameters.SmartRate.RecommendShipDateByDeliveryDate recommendShipDateByDeliveryDateParameters = new()
            {
                DesiredDeliveryDate = Fixtures.DesiredDeliveryDate,
            };

            List<RateWithTimeInTransitDetailsByDeliveryDate> ratesWithEstimatedDeliveryDates = await Client.Beta.SmartRate.RecommendShipDateByDeliveryDate(shipment.Id, recommendShipDateByDeliveryDateParameters);

            foreach (var rate in ratesWithEstimatedDeliveryDates)
            {
                Assert.NotNull(rate.TimeInTransitDetails);
                Assert.NotNull(rate.TimeInTransitDetails.EasyPostRecommendedShipDate);
                Assert.NotNull(rate.TimeInTransitDetails.TimeInTransitPercentiles);
                Assert.NotNull(rate.TimeInTransitDetails.DesiredDeliveryDate);
            }
        }

        #endregion

        #endregion
    }
}
