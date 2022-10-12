using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Services;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class ShipmentTest : UnitTest
    {
        public ShipmentTest() : base("shipment")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Shipment shipment = await CreateFullShipment();

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.NotNull(shipment.Rates);
            Assert.Equal("PNG", shipment.Options.LabelFormat);
            Assert.Equal("123", shipment.Options.InvoiceNumber);
            Assert.Equal("123", shipment.Reference);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateEmptyObjects()
        {
            UseVCR("create_empty_objects");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            shipmentData.Add("customs_info", new Dictionary<string, object>());
            Assert.NotNull(shipmentData["customs_info"]);
            (shipmentData["customs_info"] as Dictionary<string, object>).Add("customs_items", new List<object>());
            shipmentData["options"] = null;
            shipmentData["tax_identifiers"] = null;
            shipmentData["reference"] = "";

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.NotNull(shipment.Options); // The EasyPost API populates some default values here
            Assert.True(shipment.CustomsInfo.CustomsItems.Count == 0);
            Assert.Null(shipment.Reference);
            Assert.Null(shipment.TaxIdentifiers);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateShipmentWithCarbonOffset()
        {
            UseVCR("create_shipment_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment, true);

            Assert.IsType<Shipment>(shipment);

            Rate rate = shipment.LowestRate();
            CarbonOffset carbonOffset = rate.CarbonOffset;

            Assert.NotNull(carbonOffset);
            Assert.NotNull(carbonOffset.Price);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateTaxIdentifiers()
        {
            UseVCR("create_tax_identifiers");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>> { Fixtures.TaxIdentifier };

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.Equal("IOSS", shipment.TaxIdentifiers[0].TaxIdType);
        }

        [Fact(Skip = "Test does not play well with VCR. Runs as expected when not using VCR.")]
        [CrudOperations.Create]
        public async Task TestCreateWithIds()
        {
            UseVCR("create_with_ids");

            Address fromAddress = await Client.Address.Create(Fixtures.CaAddress1);
            Address toAddress = await Client.Address.Create(Fixtures.CaAddress2);
            Parcel parcel = await Client.Parcel.Create(Fixtures.BasicParcel);

            Shipment shipment = await Client.Shipment.Create(new Dictionary<string, object>
            {
                { "from_address", new Dictionary<string, object> { { "id", fromAddress.Id } } },
                { "to_address", new Dictionary<string, object> { { "id", toAddress.Id } } },
                { "parcel", new Dictionary<string, object> { { "id", parcel.Id } } }
            });

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.StartsWith("adr_", shipment.FromAddress.Id);
            Assert.StartsWith("adr_", shipment.ToAddress.Id);
            Assert.StartsWith("prcl_", shipment.Parcel.Id);
            Assert.Equal("388 Townsend St", shipment.FromAddress.Street1);
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [Fact]
        [CrudOperations.Create]
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
        [CrudOperations.Create]
        public async Task TestOneCallBuyShipmentWithCarbonOffset()
        {
            UseVCR("one_call_buy_shipment_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment, true);

            Assert.NotNull(shipment.Fees);
            bool carbonOffsetIncluded = shipment.Fees.Any(fee => fee.Type == "CarbonOffsetFee");
            Assert.True(carbonOffsetIncluded);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            ShipmentCollection shipmentCollection = await Client.Shipment.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Shipment> shipments = shipmentCollection.Shipments;

            Assert.True(shipmentCollection.HasMore);
            Assert.True(shipments.Count <= Fixtures.PageSize);
            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
            }
        }

        [Fact]
        [CrudOperations.Read] // not really a Read operation, but most logical attribute to maintain CRUD placement
        public async Task TestInstanceLowestSmartrate()
        {
            UseVCR("lowest_smartrate_instance");

            Shipment shipment = await CreateBasicShipment();

            // test lowest smartrate with valid filters
            Smartrate lowestSmartrate = await shipment.LowestSmartrate(2, SmartrateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartrate.Service);
            Assert.Equal(5.57, lowestSmartrate.Rate);
            Assert.Equal("USPS", lowestSmartrate.Carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<Exceptions.General.FilteringError>(async () => await shipment.LowestSmartrate(0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Shipment shipment = await CreateFullShipment();

            // test lowest rate with no filters
            Rate lowestRate = shipment.LowestRate();
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("5.57", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new List<string> { "Priority" };
            lowestRate = shipment.LowestRate(null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("7.90", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string> { "BAD_CARRIER" };
            Assert.Throws<Exceptions.General.FilteringError>(() => shipment.LowestRate(carriers));
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await CreateFullShipment();

            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.Id);

            Assert.IsType<Shipment>(shipment);
            Assert.Equal(shipment, retrievedShipment);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestSmartrate()
        {
            UseVCR("smartrate");

            Shipment shipment = await CreateBasicShipment();

            Assert.NotNull(shipment.Rates);

            List<Smartrate> smartRates = await shipment.GetSmartrates();
            Smartrate smartrate = smartRates.First();
            // Must compare IDs because one is a Rate object and one is a Smartrate object
            Assert.Equal(shipment.Rates[0].Id, smartrate.Id);
            Assert.NotNull(smartrate.TimeInTransit.Percentile50);
            Assert.NotNull(smartrate.TimeInTransit.Percentile75);
            Assert.NotNull(smartrate.TimeInTransit.Percentile85);
            Assert.NotNull(smartrate.TimeInTransit.Percentile90);
            Assert.NotNull(smartrate.TimeInTransit.Percentile95);
            Assert.NotNull(smartrate.TimeInTransit.Percentile97);
            Assert.NotNull(smartrate.TimeInTransit.Percentile99);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestStaticLowestSmartrate()
        {
            UseVCR("lowest_smartrate_static");

            Shipment shipment = await CreateBasicShipment();

            // test lowest smartrate with valid filters
            List<Smartrate> smartrates = await shipment.GetSmartrates();
            Smartrate lowestSmartrate = ShipmentService.GetLowestSmartrate(smartrates, 2, SmartrateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartrate.Service);
            Assert.Equal(5.57, lowestSmartrate.Rate);
            Assert.Equal("USPS", lowestSmartrate.Carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            Assert.Throws<Exceptions.General.FilteringError>(() => ShipmentService.GetLowestSmartrate(smartrates, 0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Shipment shipment = await CreateFullShipment();

            await shipment.Buy(shipment.LowestRate().Id);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestBuyShipmentWithCarbonOffset()
        {
            UseVCR("buy_shipment_with_carbon_offset");

            Shipment shipment = await CreateFullShipment();

            await shipment.Buy(shipment.LowestRate(), withCarbonOffset: true);

            Assert.NotNull(shipment.Fees);
            bool carbonOffsetIncluded = shipment.Fees.Any(fee => fee.Type == "CarbonOffsetFee");
            Assert.True(carbonOffsetIncluded);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestConvertLabel()
        {
            UseVCR("convert_label");

            Shipment shipment = await CreateOneCallBuyShipment();

            shipment = await shipment.GenerateLabel("ZPL");

            Assert.NotNull(shipment.PostageLabel.LabelZplUrl);
        }

        // Refunding a test shipment must happen within seconds of the shipment being created as test shipments naturally
        // follow a flow of created -> delivered to cycle through tracking events in test mode - as such anything older
        // than a few seconds in test mode may not be refundable.
        [Fact]
        [CrudOperations.Update]
        public async Task TestRefund()
        {
            UseVCR("refund");

            Shipment shipment = await CreateOneCallBuyShipment();

            shipment = await shipment.Refund();

            Assert.Equal("submitted", shipment.RefundStatus);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Shipment shipment = await CreateFullShipment();

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
        public async Task TestRegenerateRatesWithCarbonOffset()
        {
            UseVCR("regenerate_rates_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            List<Rate> baseRates = shipment.Rates;

            await shipment.RegenerateRates(withCarbonOffset: true);
            List<Rate> newRatesWithCarbon = shipment.Rates;

            Rate baseRate = baseRates.First();
            Rate newRateWithCarbon = newRatesWithCarbon.First();

            Assert.Null(baseRate.CarbonOffset);
            Assert.NotNull(newRateWithCarbon.CarbonOffset);
        }

        #endregion

        private async Task<Shipment> CreateBasicShipment()
        {
            return await Client.Shipment.Create(Fixtures.BasicShipment);
        }

        private async Task<Shipment> CreateFullShipment()
        {
            return await Client.Shipment.Create(Fixtures.FullShipment);
        }

        private async Task<Shipment> CreateOneCallBuyShipment()
        {
            return await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
        }
    }
}
