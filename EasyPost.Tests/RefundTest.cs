using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class RefundTest : UnitTest
    {
        public RefundTest() : base("refund")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            RefundCollection refundCollection = await Client.Refund.All(new Dictionary<string, object?>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Refund> refunds = refundCollection.refunds;

            Assert.IsTrue(refunds.Count <= Fixture.PageSize);
            Assert.IsNotNull(refundCollection.HasMore);
            foreach (Refund item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            List<Refund> refunds = await CreateBasicRefund();

            foreach (Refund item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }

            Refund refund = refunds[0];
            Assert.IsTrue(refund.id.StartsWith("rfnd_"));
            Assert.AreEqual("submitted", refund.status);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            RefundCollection refundCollection = await Client.Refund.All(new Dictionary<string, object?>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Refund refund = (await CreateBasicRefund())[0];

            Refund retrievedRefund = await Client.Refund.Retrieve(refund.id);

            Assert.IsInstanceOfType(retrievedRefund, typeof(Refund));
            Assert.AreEqual(refund, retrievedRefund);
        }

        private async Task<List<Refund>> CreateBasicRefund()
        {
            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyShipment);
            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return await Client.Refund.Create(new Dictionary<string, object?>
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
