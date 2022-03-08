using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class RefundTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "refund", true);
        }

        private static List<Refund> CreateBasicRefund()
        {
            Shipment shipment = Shipment.Create(Fixture.OneCallBuyShipment);
            Shipment retrievedShipment = Shipment.Retrieve(shipment.id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return Refund.Create(new Dictionary<string, object>
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
        public void TestCreate()
        {
            VCR.Replay("create");

            List<Refund> refunds = CreateBasicRefund();

            foreach (var item in refunds)
            {
                Assert.IsInstanceOfType(item, typeof(Refund));
            }

            Refund refund = refunds[0];
            Assert.IsTrue(refund.id.StartsWith("rfnd_"));
            Assert.AreEqual("submitted", refund.status);
        }

        [TestMethod]
        public void TestAll()
        {
            VCR.Replay("all");

            RefundCollection refundCollection = Refund.All(new Dictionary<string, object>
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
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");

            RefundCollection refundCollection = Refund.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Refund refund = CreateBasicRefund()[0];

            Refund retrievedRefund = Refund.Retrieve(refund.id);

            Assert.IsInstanceOfType(retrievedRefund, typeof(Refund));
            Assert.AreEqual(refund.id, retrievedRefund.id);
        }
    }
}
