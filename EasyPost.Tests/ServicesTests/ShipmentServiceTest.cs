using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class ShipmentServiceTests : UnitTest
    {
        public ShipmentServiceTests() : base("shipment_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
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
        [Testing.Parameters]
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
        [Testing.Parameters]
        public async Task TestCreateWithCarbonOffset()
        {
            UseVCR("create_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment, true);

            Assert.IsType<Shipment>(shipment);

            Rate rate = Client.Shipment.GetLowestRate(shipment);
            CarbonOffset carbonOffset = rate.CarbonOffset;

            Assert.NotNull(carbonOffset);
            Assert.NotNull(carbonOffset.Price);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
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

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
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
        [Testing.Function]
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
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetLowestSmartrate()
        {
            UseVCR("get_lowest_smartrate");

            Shipment shipment = await CreateBasicShipment();

            // test lowest smartrate with valid filters
            List<Smartrate> smartrates = await Client.Shipment.GetSmartrates(shipment.Id);
            Smartrate lowestSmartrate = Client.Shipment.GetLowestSmartrate(smartrates, 2, SmartrateAccuracy.Percentile90);
            Assert.Equal("Priority", lowestSmartrate.Service);
            Assert.Equal(8.15, lowestSmartrate.Rate);
            Assert.Equal("USPS", lowestSmartrate.Carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            Assert.Throws<FilteringError>(() => Client.Shipment.GetLowestSmartrate(smartrates, 0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await CreateFullShipment();

            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.Id);

            Assert.IsType<Shipment>(shipment);
            Assert.Equal(shipment, retrievedShipment);
        }

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

            shipment = await Client.Shipment.Insure(shipment.Id, 100);

            Assert.Equal("100.00", shipment.Insurance);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetSmartrates()
        {
            UseVCR("get_smartrates");

            Shipment shipment = await CreateBasicShipment();

            Assert.NotNull(shipment.Rates);

            List<Smartrate> smartRates = await Client.Shipment.GetSmartrates(shipment.Id);
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
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with rate ID
            Shipment shipment = await CreateFullShipment();
            shipment = await Client.Shipment.Buy(shipment.Id, Client.Shipment.GetLowestRate(shipment).Id);

            Assert.NotNull(shipment.PostageLabel);

            // buy with rate
            shipment = await CreateFullShipment();
            shipment = await Client.Shipment.Buy(shipment.Id, Client.Shipment.GetLowestRate(shipment));

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRate()
        {
            UseVCR("buy_with_no_rate");

            Shipment shipment = await CreateFullShipment();

            await Assert.ThrowsAsync<MissingParameterError>(async () => await Client.Shipment.Buy(shipment.Id, rate: null));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRateId()
        {
            UseVCR("buy_with_no_rate_id");

            Shipment shipment = await CreateFullShipment();

            await Assert.ThrowsAsync<MissingParameterError>(async () => await Client.Shipment.Buy(shipment.Id, rateId: null));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithCarbonOffset()
        {
            UseVCR("buy_with_carbon_offset");

            Shipment shipment = await CreateFullShipment();

            shipment = await Client.Shipment.Buy(shipment.Id, Client.Shipment.GetLowestRate(shipment), withCarbonOffset: true);

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

            Shipment shipment = await CreateFullShipment();

            shipment = await Client.Shipment.Buy(shipment.Id, Client.Shipment.GetLowestRate(shipment), endShipperId: endShipper.Id);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Shipment shipment = await CreateOneCallBuyShipment();

            shipment = await Client.Shipment.GenerateLabel(shipment.Id, "ZPL");

            Assert.NotNull(shipment.PostageLabel.LabelZplUrl);
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

            Shipment shipment = await CreateOneCallBuyShipment();

            shipment = await Client.Shipment.Refund(shipment.Id);

            Assert.Equal("submitted", shipment.RefundStatus);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Shipment shipment = await CreateFullShipment();

            shipment = await Client.Shipment.RegenerateRates(shipment.Id);

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
        public async Task TestRegenerateRatesWithCarbonOffset()
        {
            UseVCR("regenerate_rates_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            List<Rate> baseRates = shipment.Rates;

            shipment = await Client.Shipment.RegenerateRates(shipment.Id, withCarbonOffset: true);
            List<Rate> newRatesWithCarbon = shipment.Rates;

            Rate baseRate = baseRates.First();
            Rate newRateWithCarbon = newRatesWithCarbon.First();

            Assert.Null(baseRate.CarbonOffset);
            Assert.NotNull(newRateWithCarbon.CarbonOffset);
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Shipment shipment = await CreateFullShipment();

            // test lowest rate with no filters
            Rate lowestRate = Client.Shipment.GetLowestRate(shipment);
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("5.82", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new() { "Priority" };
            lowestRate = Client.Shipment.GetLowestRate(shipment, null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("8.15", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => Client.Shipment.GetLowestRate(shipment, carriers));

            // test lowest rate with empty rate list
            shipment.Rates = new List<Rate>();
            Assert.Throws<FilteringError>(() => Client.Shipment.GetLowestRate(shipment, carriers));

            // test lowest rate with null rate list
            shipment.Rates = null;
            Assert.Throws<MissingPropertyError>(() => Client.Shipment.GetLowestRate(shipment, carriers));
        }

        [Fact]
        [Testing.Function]
        public async Task TestLowestSmartrate()
        {
            UseVCR("lowest_smartrate");

            Shipment shipment = await CreateBasicShipment();
            List<Smartrate> smartrates = await Client.Shipment.GetSmartrates(shipment.Id);

            // test lowest smartrate with valid filters
            Smartrate lowestSmartrate = Client.Shipment.GetLowestSmartrate(smartrates, 2, SmartrateAccuracy.Percentile90);
            Assert.Equal("Priority", lowestSmartrate.Service);
            Assert.Equal(8.15, lowestSmartrate.Rate);
            Assert.Equal("USPS", lowestSmartrate.Carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<FilteringError>(async () => Client.Shipment.GetLowestSmartrate(smartrates, 0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [Testing.Properties]
        public async Task TestForms()
        {
            UseVCR("forms");

            Shipment shipment = await CreateFullShipment();

            await Client.Shipment.Buy(shipment.Id, Client.Shipment.GetLowestRate(shipment).Id);

            Assert.NotNull(shipment.Forms);

            foreach (Form? form in shipment.Forms)
            {
                Assert.NotNull(form.FormType);
                Assert.NotNull(form.FormUrl);
                Assert.True(form.SubmittedElectronically);
            }
        }

        #endregion

        private async Task<Shipment> CreateBasicShipment() => await Client.Shipment.Create(Fixtures.BasicShipment);

        private async Task<Shipment> CreateFullShipment() => await Client.Shipment.Create(Fixtures.FullShipment);

        private async Task<Shipment> CreateOneCallBuyShipment() => await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
    }
}
