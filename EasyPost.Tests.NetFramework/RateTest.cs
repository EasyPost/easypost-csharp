using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class RateTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            Shipment shipment = Shipment.Create(Fixture.BasicShipment);

            Rate rate = Rate.Retrieve(shipment.rates[0].id);

            Assert.IsInstanceOfType(rate, typeof(Rate));
            Assert.IsTrue(rate.id.StartsWith("rate_"));
            Assert.AreEqual(shipment.rates[0].id, rate.id);
        }
    }
}
