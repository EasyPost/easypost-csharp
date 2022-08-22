using System;
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
            Assert.StartsWith("shp_", shipment.id);
            Assert.NotNull(shipment.rates);
            Assert.Equal("PNG", shipment.options.label_format);
            Assert.Equal("123", shipment.options.invoice_number);
            Assert.Equal("123", shipment.reference);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateEmptyObjects()
        {
            UseVCR("create_empty_objects");

            Dictionary<string, object> shipmentData = Fixture.BasicShipment;

            shipmentData.Add("customs_info", new Dictionary<string, object>());
            Assert.NotNull(shipmentData["customs_info"]);
            (shipmentData["customs_info"] as Dictionary<string, object>).Add("customs_items", new List<object>());
            shipmentData["options"] = null;
            shipmentData["tax_identifiers"] = null;
            shipmentData["reference"] = "";

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.id);
            Assert.NotNull(shipment.options); // The EasyPost API populates some default values here
            Assert.True(shipment.customs_info.customs_items.Count == 0);
            Assert.Null(shipment.reference);
            Assert.Null(shipment.tax_identifiers);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateShipmentWithCarbonOffset()
        {
            UseVCR("create_shipment_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixture.BasicCarbonOffsetShipment, true);

            Assert.IsType<Shipment>(shipment);

            Rate rate = shipment.LowestRate();
            CarbonOffset carbonOffset = rate.carbon_offset;

            Assert.NotNull(carbonOffset);
            Assert.NotNull(carbonOffset.price);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateTaxIdentifiers()
        {
            UseVCR("create_tax_identifiers");

            Dictionary<string, object> shipmentData = Fixture.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>>
            {
                Fixture.TaxIdentifier
            };

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.id);
            Assert.Equal("IOSS", shipment.tax_identifiers[0].tax_id_type);
        }

        [Fact(Skip = "Test does not play well with VCR")]
        [CrudOperations.Create]
        public async Task TestCreateWithIds()
        {
            UseVCR("create_with_ids");

            Address fromAddress = await Client.Address.Create(Fixture.BasicAddress);
            Address toAddress = await Client.Address.Create(Fixture.BasicAddress);
            Parcel parcel = await Client.Parcel.Create(Fixture.BasicParcel);

            Shipment shipment = await Client.Shipment.Create(new Dictionary<string, object>
            {
                {
                    "from_address", new Dictionary<string, object>
                    {
                        {
                            "id", fromAddress.id
                        }
                    }
                },
                {
                    "to_address", new Dictionary<string, object>
                    {
                        {
                            "id", toAddress.id
                        }
                    }
                },
                {
                    "parcel", new Dictionary<string, object>
                    {
                        {
                            "id", parcel.id
                        }
                    }
                }
            });

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.id);
            Assert.StartsWith("adr_", shipment.from_address.id);
            Assert.StartsWith("adr_", shipment.to_address.id);
            Assert.StartsWith("prcl_", shipment.parcel.id);
            Assert.Equal("388 Townsend St", shipment.from_address.street1);
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [Fact]
        [CrudOperations.Create]
        public async Task TestInsure()
        {
            UseVCR("insure");

            Dictionary<string, object> shipmentData = Fixture.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;

            Shipment shipment = await Client.Shipment.Create(shipmentData);

            shipment = await shipment.Insure(100);

            Assert.Equal("100.00", shipment.insurance);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestOneCallBuyShipmentWithCarbonOffset()
        {
            UseVCR("one_call_buy_shipment_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyCarbonOffsetShipment, true);

            Assert.NotNull(shipment.fees);
            bool carbonOffsetIncluded = shipment.fees.Any(fee => fee.type == "CarbonOffsetFee");
            Assert.True(carbonOffsetIncluded);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            ShipmentCollection shipmentCollection = await Client.Shipment.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Assert.True(shipmentCollection.has_more);
            List<Shipment> shipments = shipmentCollection.shipments;

            Assert.True(shipments.Count <= Fixture.PageSize);
            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
            }
        }

        [Fact]
        [CrudOperations.Read] // not really?
        public async Task TestInstanceLowestSmartrate()
        {
            UseVCR("lowest_smartrate_instance");

            Shipment shipment = await CreateBasicShipment();

            // test lowest smartrate with valid filters
            Smartrate lowestSmartrate = await shipment.LowestSmartrate(1, SmartrateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartrate.service);
            Assert.Equal(5.49, lowestSmartrate.rate);
            Assert.Equal("USPS", lowestSmartrate.carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<Exception>(async () => await shipment.LowestSmartrate(0, SmartrateAccuracy.Percentile90));

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
            Assert.Equal("First", lowestRate.service);
            Assert.Equal("5.49", lowestRate.rate);
            Assert.Equal("USPS", lowestRate.carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new List<string>
            {
                "Priority"
            };
            lowestRate = shipment.LowestRate(null, services);
            Assert.Equal("Priority", lowestRate.service);
            Assert.Equal("7.37", lowestRate.rate);
            Assert.Equal("USPS", lowestRate.carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.Throws<Exception>(() => shipment.LowestRate(carriers));
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await CreateFullShipment();

            Shipment retrievedShipment = await Client.Shipment.Retrieve(shipment.id);

            Assert.IsType<Shipment>(shipment);
            Assert.Equal(shipment, retrievedShipment);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestSmartrate()
        {
            UseVCR("smartrate");

            Shipment shipment = await CreateBasicShipment();

            Assert.NotNull(shipment.rates);

            List<Smartrate> smartRates = await shipment.GetSmartrates();
            Smartrate smartrate = smartRates.First();
            // Must compare IDs because one is a Rate object and one is a Smartrate object
            Assert.Equal(shipment.rates[0].id, smartrate.id);
            Assert.NotNull(smartrate.time_in_transit.percentile_50);
            Assert.NotNull(smartrate.time_in_transit.percentile_75);
            Assert.NotNull(smartrate.time_in_transit.percentile_85);
            Assert.NotNull(smartrate.time_in_transit.percentile_90);
            Assert.NotNull(smartrate.time_in_transit.percentile_95);
            Assert.NotNull(smartrate.time_in_transit.percentile_97);
            Assert.NotNull(smartrate.time_in_transit.percentile_99);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestStaticLowestSmartrate()
        {
            UseVCR("lowest_smartrate_static");

            Shipment shipment = await CreateBasicShipment();

            // test lowest smartrate with valid filters
            List<Smartrate> smartrates = await shipment.GetSmartrates();
            Smartrate lowestSmartrate = ShipmentService.GetLowestSmartrate(smartrates, 1, SmartrateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartrate.service);
            Assert.Equal(5.49, lowestSmartrate.rate);
            Assert.Equal("USPS", lowestSmartrate.carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            Assert.Throws<Exception>(() => ShipmentService.GetLowestSmartrate(smartrates, 0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Shipment shipment = await CreateFullShipment();

            await shipment.Buy(shipment.LowestRate().id);

            Assert.NotNull(shipment.postage_label);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestBuyShipmentWithCarbonOffset()
        {
            UseVCR("buy_shipment_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixture.FullCarbonOffsetShipment);

            await shipment.Buy(shipment.LowestRate(), withCarbonOffset: true);

            Assert.NotNull(shipment.fees);
            bool carbonOffsetIncluded = shipment.fees.Any(fee => fee.type == "CarbonOffsetFee");
            Assert.True(carbonOffsetIncluded);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestConvertLabel()
        {
            UseVCR("convert_label");

            Shipment shipment = await CreateOneCallBuyShipment();

            shipment = await shipment.GenerateLabel("ZPL");

            Assert.NotNull(shipment.postage_label.label_zpl_url);
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

            Assert.Equal("submitted", shipment.refund_status);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Shipment shipment = await CreateFullShipment();

            await shipment.RegenerateRates();

            List<Rate> rates = shipment.rates;

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

            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyCarbonOffsetShipment);
            List<Rate> baseRates = shipment.rates;

            await shipment.RegenerateRates(withCarbonOffset: true);
            List<Rate> newRatesWithCarbon = shipment.rates;

            Rate baseRate = baseRates.First();
            Rate newRateWithCarbon = newRatesWithCarbon.First();

            Assert.Null(baseRate.carbon_offset);
            Assert.NotNull(newRateWithCarbon.carbon_offset);
        }

        #endregion

        private async Task<Shipment> CreateBasicShipment()
        {
            return await Client.Shipment.Create(Fixture.BasicShipment);
        }

        private async Task<Shipment> CreateFullShipment()
        {
            return await Client.Shipment.Create(Fixture.FullShipment);
        }

        private async Task<Shipment> CreateOneCallBuyShipment()
        {
            return await Client.Shipment.Create(Fixture.OneCallBuyShipment);
        }
    }
}
