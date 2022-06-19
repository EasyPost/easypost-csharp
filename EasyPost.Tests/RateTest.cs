using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class RateTest : UnitTest
    {
        public RateTest() : base("rate")
        {
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Shipment shipment = await Client.Shipments.Create(Fixture.BasicShipment);

            Rate rate = await Client.Rates.Retrieve(shipment.Rates[0].Id);

            Assert.IsInstanceOfType(rate, typeof(Rate));
            Assert.IsTrue(rate.Id.StartsWith("rate_"));
            Assert.AreEqual(shipment.Rates[0], rate);
        }
    }
}
