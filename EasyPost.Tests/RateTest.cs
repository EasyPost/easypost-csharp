using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class RateTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("rate");
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("retrieve");

            Shipment shipment = await client.Shipments.Create(Fixture.BasicShipment);

            Rate rate = await client.Rates.Retrieve(shipment.rates[0].id);

            Assert.IsInstanceOfType(rate, typeof(Rate));
            Assert.IsTrue(rate.id.StartsWith("rate_"));
            Assert.AreEqual(shipment.rates[0], rate);
        }
    }
}
