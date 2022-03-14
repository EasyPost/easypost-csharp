using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class RateTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "rate", true);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");

            Shipment shipment = await Shipment.Create(Fixture.BasicShipment);

            Rate rate = await Rate.Retrieve(shipment.rates[0].id);

            Assert.IsInstanceOfType(rate, typeof(Rate));
            Assert.IsTrue(rate.id.StartsWith("rate_"));
            Assert.AreEqual(shipment.rates[0].id, rate.id);
        }
    }
}
