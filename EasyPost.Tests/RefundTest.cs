using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class RefundTest : UnitTest
    {
        public RefundTest() : base("refund")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            List<Refund> refunds = await CreateBasicRefund();

            foreach (Refund item in refunds)
            {
                Assert.IsType<Refund>(item);
            }

            Refund refund = refunds[0];
            Assert.StartsWith("rfnd_", refund.id);
            Assert.Equal("submitted", refund.status);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            RefundCollection refundCollection = await Client.Refund.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Refund> refunds = refundCollection.refunds;

            Assert.True(refundCollection.has_more);
            Assert.True(refunds.Count <= Fixture.PageSize);
            foreach (Refund item in refunds)
            {
                Assert.IsType<Refund>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            RefundCollection refundCollection = await Client.Refund.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Refund refund = (await CreateBasicRefund())[0];

            Refund retrievedRefund = await Client.Refund.Retrieve(refund.id);

            Assert.IsType<Refund>(retrievedRefund);
            Assert.Equal(refund, retrievedRefund);
        }

        #endregion

        private async Task<List<Refund>> CreateBasicRefund()
        {
            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyShipment);
            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return await Client.Refund.Create(new Dictionary<string, object>
            {
                {
                    "carrier", Fixture.Usps
                },
                {
                    "tracking_codes", new List<string>
                    {
                        retrievedShipment.tracking_code
                    }
                }
            });
        }
    }
}
