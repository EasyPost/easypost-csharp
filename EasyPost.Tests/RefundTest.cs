using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
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
            UseVCR("all", ApiVersion.V2);

            RefundCollection refundCollection = await Client.Refunds.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Refund> refunds = refundCollection.Refunds;

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
            UseVCR("create", ApiVersion.V2);

            List<Refund> refunds = await CreateBasicRefund();

            foreach (Refund item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }

            Refund refund = refunds[0];
            Assert.IsTrue(refund.Id.StartsWith("rfnd_"));
            Assert.AreEqual("submitted", refund.Status);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            RefundCollection refundCollection = await Client.Refunds.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Refund refund = (await CreateBasicRefund())[0];

            Refund retrievedRefund = await Client.Refunds.Retrieve(refund.Id);

            Assert.IsInstanceOfType(retrievedRefund, typeof(Refund));
            Assert.AreEqual(refund, retrievedRefund);
        }

        private async Task<List<Refund>> CreateBasicRefund()
        {
            Shipment shipment = await Client.Shipments.Create(Fixture.OneCallBuyShipment);
            Shipment retrievedShipment = await Client.Shipments.Retrieve(shipment.Id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return await Client.Refunds.Create(new Dictionary<string, object>
            {
                {
                    "carrier", Fixture.Usps
                },
                {
                    "tracking_codes", new List<string>
                    {
                        retrievedShipment.TrackingCode
                    }
                }
            });
        }
    }
}
