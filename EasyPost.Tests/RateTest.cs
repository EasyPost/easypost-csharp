using System.Threading.Tasks;
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
            _vcr.SetUpTest("retrieve");

            Shipment shipment = await Shipment.Create(Fixture.BasicShipment);

            Rate rate = await Rate.Retrieve(shipment.rates[0].id);

            Assert.IsInstanceOfType(rate, typeof(Rate));
            Assert.IsTrue(rate.id.StartsWith("rate_"));
            Assert.AreEqual(shipment.rates[0], rate);
        }
    }
}
