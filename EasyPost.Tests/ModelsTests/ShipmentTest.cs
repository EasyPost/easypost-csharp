using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class ShipmentTests : UnitTest
    {
        public ShipmentTests() : base("shipment")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateWithTaxIdentifiers()
        {
            UseVCR("create_with_tax_identifiers");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>> { Fixtures.TaxIdentifier };

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.Equal("IOSS", shipment.TaxIdentifiers[0].TaxIdType);
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestInsure()
        {
            UseVCR("insure");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            shipment = await shipment.Insure(100);

            Assert.Equal("100.00", shipment.Insurance);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestInsureWithNoId()
        {
            UseVCR("insure_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            shipment.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await shipment.Insure(100));
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetSmartRates()
        {
            UseVCR("get_smart_rates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Assert.NotNull(shipment.Rates);

            List<Smartrate> smartRates = await shipment.GetSmartrates();
            Smartrate smartRate = smartRates.First();
            // Must compare IDs because one is a Rate object and one is a Smartrate object
            Assert.Equal(shipment.Rates[0].Id, smartRate.Id);
            Assert.NotNull(smartRate.TimeInTransit.Percentile50);
            Assert.NotNull(smartRate.TimeInTransit.Percentile75);
            Assert.NotNull(smartRate.TimeInTransit.Percentile85);
            Assert.NotNull(smartRate.TimeInTransit.Percentile90);
            Assert.NotNull(smartRate.TimeInTransit.Percentile95);
            Assert.NotNull(smartRate.TimeInTransit.Percentile97);
            Assert.NotNull(smartRate.TimeInTransit.Percentile99);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestGetSmartRatesWithNoId()
        {
            UseVCR("get_smart_rates_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);
            shipment.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await shipment.GetSmartrates());
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with rate ID
            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            await shipment.Buy(shipment.LowestRate().Id);

            Assert.NotNull(shipment.PostageLabel);

            // buy with rate
            shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            await shipment.Buy(shipment.LowestRate());

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoId()
        {
            UseVCR("buy_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            shipment.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await shipment.Buy(shipment.LowestRate().Id));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRate()
        {
            UseVCR("buy_with_no_rate");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await Assert.ThrowsAsync<MissingParameterError>(async () => await shipment.Buy(rate: null));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRateId()
        {
            UseVCR("buy_with_no_rate_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await Assert.ThrowsAsync<MissingParameterError>(async () => await shipment.Buy(rateId: null));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithCarbonOffset()
        {
            UseVCR("buy_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await shipment.Buy(shipment.LowestRate(), withCarbonOffset: true);

            Assert.NotNull(shipment.Fees);
            bool carbonOffsetIncluded = shipment.Fees.Any(fee => fee.Type == "CarbonOffsetFee");
            Assert.True(carbonOffsetIncluded);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithEndShipper()
        {
            UseVCR("buy_with_end_shipper");

            EndShipper endShipper = await Client.EndShipper.Create(Fixtures.CaAddress1);

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await shipment.Buy(shipment.LowestRate(), endShipperId: endShipper.Id);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            shipment = await shipment.GenerateLabel("ZPL");

            Assert.NotNull(shipment.PostageLabel.LabelZplUrl);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestGenerateLabelWithNoId()
        {
            UseVCR("generate_label_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            shipment.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await shipment.GenerateLabel("ZPL"));
        }

        // Refunding a test shipment must happen within seconds of the shipment being created as test shipments naturally
        // follow a flow of created -> delivered to cycle through tracking events in test mode - as such anything older
        // than a few seconds in test mode may not be refundable.
        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRefund()
        {
            UseVCR("refund");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            shipment = await shipment.Refund();

            Assert.Equal("submitted", shipment.RefundStatus);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestRefundWithNoId()
        {
            UseVCR("refund_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            shipment.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await shipment.Refund());
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await shipment.RegenerateRates();

            List<Rate> rates = shipment.Rates;

            Assert.NotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsType<Rate>(rate);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestRegenerateRatesWithNoId()
        {
            UseVCR("regenerate_rates_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            shipment.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await shipment.RegenerateRates());
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestRegenerateRatesWithCarbonOffset()
        {
            UseVCR("regenerate_rates_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            List<Rate> baseRates = shipment.Rates;

            await shipment.RegenerateRates(withCarbonOffset: true);
            List<Rate> newRatesWithCarbon = shipment.Rates;

            Rate baseRate = baseRates!.First();
            Rate newRateWithCarbon = newRatesWithCarbon!.First();

            Assert.Null(baseRate.CarbonOffset);
            Assert.NotNull(newRateWithCarbon.CarbonOffset);
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            // test lowest rate with no filters
            Rate lowestRate = shipment.LowestRate();
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("5.82", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new() { "Priority" };
            lowestRate = shipment.LowestRate(null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("8.15", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => shipment.LowestRate(carriers));

            // test lowest rate with empty rate list
            shipment.Rates = new List<Rate>();
            Assert.Throws<FilteringError>(() => shipment.LowestRate(carriers));

            // test lowest rate with null rate list
            shipment.Rates = null;
            Assert.Throws<MissingPropertyError>(() => shipment.LowestRate(carriers));
        }

        [Fact]
        [Testing.Function]
        public async Task TestLowestSmartRate()
        {
            UseVCR("lowest_smart_rate");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            // test lowest smart rate with valid filters
            Smartrate lowestSmartRate = await shipment.LowestSmartrate(2, SmartrateAccuracy.Percentile90);
            Assert.Equal("Priority", lowestSmartRate.Service);
            Assert.Equal(8.15, lowestSmartRate.Rate);
            Assert.Equal("USPS", lowestSmartRate.Carrier);

            // test lowest smart rate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<FilteringError>(async () => await shipment.LowestSmartrate(0, SmartrateAccuracy.Percentile90));

            // test lowest smart rate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [Testing.Properties]
        public async Task TestForms()
        {
            UseVCR("forms");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await shipment.Buy(shipment.LowestRate().Id);

            Assert.NotNull(shipment.Forms);

            foreach (Form? form in shipment.Forms)
            {
                Assert.NotNull(form.FormType);
                Assert.NotNull(form.FormUrl);
                Assert.True(form.SubmittedElectronically);
            }
        }

        #endregion
    }
}
