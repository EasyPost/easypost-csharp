using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class RefundServiceTests : UnitTest
    {
        public RefundServiceTests() : base("refund_service")
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

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.Id); // We need to retrieve the shipment so that the tracking_code has time to populate

            List<Refund> refunds = await Client.Refund.Create(new Dictionary<string, object>
            {
                { "carrier", Fixtures.Usps },
                { "tracking_codes", new List<string> { retrievedShipment.TrackingCode } }
            });

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

            RefundCollection refundCollection = await Client.Refund.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Refund> refunds = refundCollection.Refunds;

            Assert.True(refunds.Count <= Fixtures.PageSize);
            foreach (Refund item in refunds)
            {
                Assert.IsType<Refund>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            RefundCollection refundCollection = await Client.Refund.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Refund> refunds = refundCollection.Refunds;

            Refund retrievedRefund = await Client.Refund.Retrieve(refunds[0].Id);

            Assert.IsType<Refund>(retrievedRefund);
            Assert.Equal(refunds[0], retrievedRefund);
        }

        #endregion

        #endregion
    }
}
