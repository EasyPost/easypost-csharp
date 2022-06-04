using System.Threading.Tasks;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class RateTest : UnitTest
    {
        public RateTest() : base("rate", TestUtils.ApiKey.Test)
        {
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.BasicShipment);

            Rate rate = await V2Client.Rates.Retrieve(shipment.rates[0].id);

            Assert.IsInstanceOfType(rate, typeof(Rate));
            Assert.IsTrue(rate.id.StartsWith("rate_"));
            Assert.AreEqual(shipment.rates[0], rate);
        }
    }
}
