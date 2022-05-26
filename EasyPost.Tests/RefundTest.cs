using System.Collections.Generic;
using System.Threading.Tasks;
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

        private static async Task<List<Refund>> CreateBasicRefund()
        {
            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyShipment);
            Shipment retrievedShipment = await Shipment.Retrieve(shipment.id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return await Refund.Create(new Dictionary<string, object>
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
            _vcr.SetUpTest("create");

            List<Refund> refunds = await CreateBasicRefund();

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
            _vcr.SetUpTest("all");

            RefundCollection refundCollection = await Refund.All(new Dictionary<string, object>
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
            _vcr.SetUpTest("retrieve");

            Refund refund = (await CreateBasicRefund())[0];

            Refund retrievedRefund = await Refund.Retrieve(refund.id);

            Assert.IsInstanceOfType(retrievedRefund, typeof(Refund));
            Assert.AreEqual(refund, retrievedRefund);
        }
    }
}
