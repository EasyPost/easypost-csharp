using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
#pragma warning disable xUnit1004
    public class ShipmentTests : UnitTest
    {
        public ShipmentTests() : base("shipment_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestInsure()
        {
            UseVCR("insure");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            BetaFeatures.Parameters.Shipments.Insure insureParameters = new BetaFeatures.Parameters.Shipments.Insure
            {
                Amount = "100",
            };

            shipment = await shipment.Insure(insureParameters);

            Assert.Equal("100.00", shipment.Insurance);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            // buy with rate ID
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            BetaFeatures.Parameters.Shipments.Buy buyParameters = new BetaFeatures.Parameters.Shipments.Buy(rate.Id);
            await shipment.Buy(buyParameters);

            Assert.NotNull(shipment.PostageLabel);

            // buy with rate
            shipment = await Client.Shipment.Create(shipmentParameters);
            rate = shipment.LowestRate();

            buyParameters = new BetaFeatures.Parameters.Shipments.Buy(rate);
            await shipment.Buy(buyParameters);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithCarbonOffset()
        {
            UseVCR("buy_with_carbon_offset");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            BetaFeatures.Parameters.Shipments.Buy buyParameters = new BetaFeatures.Parameters.Shipments.Buy(rate)
            {
                CarbonOffset = true,
            };

            await shipment.Buy(buyParameters);

            Assert.NotNull(shipment.Fees);
            bool carbonOffsetIncluded = shipment.Fees.Any(fee => fee.Type == "CarbonOffsetFee");
            Assert.True(carbonOffsetIncluded);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithEndShipper()
        {
            UseVCR("buy_with_end_shipper");

            Dictionary<string, object> endShipperData = Fixtures.CaAddress1;
            BetaFeatures.Parameters.EndShippers.Create endShipperParameters = Fixtures.Parameters.EndShippers.Create(endShipperData);
            EndShipper endShipper = await Client.EndShipper.Create(endShipperParameters);

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            BetaFeatures.Parameters.Shipments.Buy buyParameters = new BetaFeatures.Parameters.Shipments.Buy(rate)
            {
                EndShipperId = endShipper.Id,
            };

            await shipment.Buy(buyParameters);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            BetaFeatures.Parameters.Shipments.GenerateLabel generateLabelParameters = new BetaFeatures.Parameters.Shipments.GenerateLabel
            {
                FileFormat = "ZPL",
            };

            shipment = await shipment.GenerateLabel(generateLabelParameters);

            Assert.NotNull(shipment.PostageLabel.LabelZplUrl);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            BetaFeatures.Parameters.Shipments.RegenerateRates regenerateRatesParameters = new BetaFeatures.Parameters.Shipments.RegenerateRates();

            await shipment.RegenerateRates(regenerateRatesParameters);

            List<Rate> rates = shipment.Rates;

            Assert.NotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsType<Rate>(rate);
            }
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestRegenerateRatesWithCarbonOffset()
        {
            UseVCR("regenerate_rates_with_carbon_offset");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;
            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            List<Rate> baseRates = shipment.Rates;

            BetaFeatures.Parameters.Shipments.RegenerateRates regenerateRatesParameters = new BetaFeatures.Parameters.Shipments.RegenerateRates
            {
                CarbonOffset = true,
            };
            await shipment.RegenerateRates(regenerateRatesParameters);

            List<Rate> newRatesWithCarbon = shipment.Rates;

            Rate baseRate = baseRates!.First();
            Rate newRateWithCarbon = newRatesWithCarbon!.First();

            Assert.Null(baseRate.CarbonOffset);
            Assert.NotNull(newRateWithCarbon.CarbonOffset);
        }

        #endregion

        #endregion
    }
}
