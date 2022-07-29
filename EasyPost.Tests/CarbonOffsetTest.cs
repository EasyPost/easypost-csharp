using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CarbonOffsetTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("carbon_offset");
        }

        [TestMethod]
        public async Task TestCreateShipment()
        {
            _vcr.SetUpTest("create_shipment");

            Shipment shipment = await Shipment.Create(Fixture.BasicCarbonOffsetShipment, true);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));

            Rate rate = shipment.LowestRate();
            CarbonOffset carbonOffset = rate.carbon_offset;

            Assert.IsNotNull(carbonOffset);
            Assert.IsNotNull(carbonOffset.price);
        }

        [TestMethod]
        public async Task TestBuyShipment()
        {
            _vcr.SetUpTest("buy_shipment");

            Shipment shipment = await Shipment.Create(Fixture.FullCarbonOffsetShipment);

            await shipment.Buy(shipment.LowestRate(), null, true);

            Assert.IsNotNull(shipment.fees);
            bool carbonOffsetIncluded = shipment.fees.Any(fee => fee.type == "CarbonOffsetFee");
            Assert.IsTrue(carbonOffsetIncluded);
        }

        [TestMethod]
        public async Task TestOneCallBuyShipment()
        {
            _vcr.SetUpTest("one_call_buy_shipment");

            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyCarbonOffsetShipment, true);

            Assert.IsNotNull(shipment.fees);
            bool carbonOffsetIncluded = shipment.fees.Any(fee => fee.type == "CarbonOffsetFee");
            Assert.IsTrue(carbonOffsetIncluded);
        }

        [TestMethod]
        public async Task TestRegenerateRates()
        {
            _vcr.SetUpTest("regenerate_rates");

            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyCarbonOffsetShipment);
            List<Rate> baseRates = shipment.rates;

            await shipment.RegenerateRates(null, true);
            List<Rate> newRatesWithCarbon = shipment.rates;

            Rate baseRate = baseRates.First();
            Rate newRateWithCarbon = newRatesWithCarbon.First();

            Assert.IsNull(baseRate.carbon_offset);
            Assert.IsNotNull(newRateWithCarbon.carbon_offset);
        }
    }
}
