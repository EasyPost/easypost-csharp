using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class RefundServiceTests : UnitTest
    {
        public RefundServiceTests() : base("refund_service_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.Id); // We need to retrieve the shipment so that the tracking_code has time to populate

            Dictionary<string, object> refundData = new Dictionary<string, object>
            {
                { "carrier", Fixtures.Usps },
                { "tracking_codes", new List<string> { retrievedShipment.TrackingCode } },
            };

            BetaFeatures.Parameters.Refunds.Create refundParameters = Fixtures.Parameters.Refunds.Create(refundData);

            List<Refund> refunds = await Client.Refund.Create(refundParameters);

            foreach (Refund item in refunds)
            {
                Assert.IsType<Refund>(item);
            }

            Refund refund = refunds[0];
            Assert.StartsWith("rfnd_", refund.Id);
            Assert.Equal("submitted", refund.Status);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Refunds.All parameters = Fixtures.Parameters.Refunds.All(data);

            RefundCollection refundCollection = await Client.Refund.All(parameters);

            List<Refund> refunds = refundCollection.Refunds;

            Assert.True(refunds.Count <= Fixtures.PageSize);
            foreach (Refund item in refunds)
            {
                Assert.IsType<Refund>(item);
            }
        }

        #endregion

        #endregion
    }
}
