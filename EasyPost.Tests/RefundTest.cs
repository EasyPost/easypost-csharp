using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class RefundTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("refund");
        }

        private static async Task<List<Refund>> CreateBasicRefund(V2Client v2Client)
        {
            Shipment shipment = await v2Client.Shipments.Create(Fixture.OneCallBuyShipment);
            Shipment retrievedShipment = await v2Client.Shipments.Retrieve(shipment.id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return await v2Client.Refunds.Create(new Dictionary<string, object>
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

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            List<Refund> refunds = await CreateBasicRefund(v2Client);

            foreach (var item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }

            Refund refund = refunds[0];
            Assert.IsTrue(refund.id.StartsWith("rfnd_"));
            Assert.AreEqual("submitted", refund.status);
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client v2Client = _vcr.SetUpTest("all");

            RefundCollection refundCollection = await v2Client.Refunds.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Refund> refunds = refundCollection.refunds;

            Assert.IsTrue(refunds.Count <= Fixture.PageSize);
            Assert.IsNotNull(refundCollection.has_more);
            foreach (var item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            RefundCollection refundCollection = await v2Client.Refunds.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Refund refund = (await CreateBasicRefund(v2Client))[0];

            Refund retrievedRefund = await v2Client.Refunds.Retrieve(refund.id);

            Assert.IsInstanceOfType(retrievedRefund, typeof(Refund));
            Assert.AreEqual(refund, retrievedRefund);
        }
    }
}
