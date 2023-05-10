using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
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
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            RefundCollection collection = await Client.Refund.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                RefundCollection nextPageCollection = await Client.Refund.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Refunds[0].Id, nextPageCollection.Refunds[0].Id);
            }
            catch (EndOfPaginationError) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
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
