using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Services;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
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

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

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

            Rate rate = shipment.LowestRate();
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

            Assert.True(shipments.Count <= Fixtures.PageSize);
            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
            }
        }

        /// <summary>
        ///     This test confirms that the parameters used to filter the results of the All() method are passed through to the resulting collection object.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Parameters]
        public async Task TestAllParameterHandOff()
        {
            UseVCR("all_parameter_hand_off");

            Dictionary<string, object> filters = new Dictionary<string, object> {
                { "include_children", true },
                { "purchased", false },
            };

            ShipmentCollection shipmentCollection = await Client.Shipment.All(filters);

            Assert.Equal(filters["include_children"], shipmentCollection.IncludeChildren);
            Assert.Equal(filters["purchased"], shipmentCollection.Purchased);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            ShipmentCollection collection = await Client.Shipment.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                ShipmentCollection nextPageCollection = await Client.Shipment.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Shipments[0].Id, nextPageCollection.Shipments[0].Id);
            }
            catch (EndOfPaginationError e) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }

        /// <summary>
        ///     This test confirms that the parameters used to filter the results of the All() method are used to filter the results of the GetNextPage() method.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Parameters]
        public async Task TestGetNextPageParameterHandOff()
        {
            UseVCR("get_next_page_parameter_hand_off");

            Dictionary<string, object> filters = new Dictionary<string, object> {
                { "include_children", true },
                { "purchased", false },
            };

            ShipmentCollection shipmentCollection = await Client.Shipment.All(filters);

            BetaFeatures.Parameters.Shipments.All filtersForNextPage = shipmentCollection.BuildNextPageParameters<BetaFeatures.Parameters.Shipments.All>(shipmentCollection.Shipments);

            Assert.Equal(shipmentCollection.IncludeChildren, filtersForNextPage.IncludeChildren);
            Assert.Equal(shipmentCollection.Purchased, filtersForNextPage.Purchased);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

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
        public async Task TestGetSmartRates()
        {
            UseVCR("get_smart_rates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Assert.NotNull(shipment.Rates);

            List<Smartrate> smartRates = await Client.Shipment.GetSmartrates(shipment.Id);
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
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with rate ID
            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            shipment = await Client.Shipment.Buy(shipment.Id, shipment.LowestRate().Id);

            Assert.NotNull(shipment.PostageLabel);

            // buy with rate
            shipment = await Client.Shipment.Create(Fixtures.FullShipment);
            shipment = await Client.Shipment.Buy(shipment.Id, shipment.LowestRate());

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRate()
        {
            UseVCR("buy_with_no_rate");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await Assert.ThrowsAsync<MissingParameterError>(async () => await Client.Shipment.Buy(shipment.Id, rate: null));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRateId()
        {
            UseVCR("buy_with_no_rate_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await Assert.ThrowsAsync<MissingParameterError>(async () => await Client.Shipment.Buy(shipment.Id, rateId: null));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithCarbonOffset()
        {
            UseVCR("buy_with_carbon_offset");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            shipment = await Client.Shipment.Buy(shipment.Id, shipment.LowestRate(), withCarbonOffset: true);

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

            shipment = await Client.Shipment.Buy(shipment.Id, shipment.LowestRate(), endShipperId: endShipper.Id);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

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

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            shipment = await Client.Shipment.Refund(shipment.Id);

            Assert.Equal("submitted", shipment.RefundStatus);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

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

            Rate baseRate = baseRates!.First();
            Rate newRateWithCarbon = newRatesWithCarbon!.First();

            Assert.Null(baseRate.CarbonOffset);
            Assert.NotNull(newRateWithCarbon.CarbonOffset);
        }

        [Fact]
        [Testing.Function]
        public void TestLowestRateFiltering()
        {
            // Mock rates since these can change from the API and we want to test the local filtering logic, not the API call
            // API call is tested in TestRegenerateRates
            List<Rate> rates = new List<Rate>
            {
                new Rate
                {
                    Service = "Priority",
                    Carrier = "USPS",
                    Price = "7.58",
                },
                new Rate
                {
                    Service = "First",
                    Carrier = "USPS",
                    Price = "6.07",
                },
            };
            Shipment shipment = new Shipment
            {
                Rates = rates,
            };

            // test lowest rate with no filters
            Rate lowestRate = shipment.LowestRate();
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("6.07", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new() { "Priority" };
            lowestRate = shipment.LowestRate(null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("7.58", lowestRate.Price);
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
        public async Task TestLowestSmartRateFiltering()
        {
            // Mock rates since these can change from the API and we want to test the local filtering logic, not the API call
            // API call is tested in TestGetSmartRates
            List<Smartrate> smartrates = new List<Smartrate>
            {
                new Smartrate
                {
                    Service = "Priority",
                    Carrier = "USPS",
                    Rate = 1.00, // this rate is cheaper but doesn't meet the filters
                    TimeInTransit = new TimeInTransit
                    {
                        Percentile90 = 3,
                    },
                },
                new Smartrate
                {
                    Service = "First",
                    Carrier = "USPS",
                    Rate = 6.07,
                    TimeInTransit = new TimeInTransit
                    {
                        Percentile90 = 2,
                    },
                },
            };

            // test lowest Smart Rate with valid filters
            Smartrate lowestSmartRate = Utilities.Rates.GetLowestSmartRate(smartrates, 2, SmartrateAccuracy.Percentile90);
            Assert.Equal("First", lowestSmartRate.Service);
            Assert.Equal(6.07, lowestSmartRate.Rate);
            Assert.Equal("USPS", lowestSmartRate.Carrier);

            // test lowest Smart Rate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsAsync<FilteringError>(() => Task.FromResult(Utilities.Rates.GetLowestSmartRate(smartrates, 0, SmartrateAccuracy.Percentile90)));

            // test lowest Smart Rate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        [Fact]
        [Testing.Properties]
        public async Task TestForms()
        {
            UseVCR("forms");

            Shipment shipment = await Client.Shipment.Create(Fixtures.FullShipment);

            await Client.Shipment.Buy(shipment.Id, shipment.LowestRate().Id);

            Assert.NotNull(shipment.Forms);

            foreach (Form? form in shipment.Forms)
            {
                Assert.NotNull(form.FormType);
                Assert.NotNull(form.FormUrl);
                Assert.True(form.SubmittedElectronically);
            }
        }

        #endregion

        #endregion
    }
}
