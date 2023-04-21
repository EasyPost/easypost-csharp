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
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

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
                CustomAssertions.Pass();
            }
            catch // Any other exception is a failure
            {
                Assert.Fail("Failed to get next page");
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
        public async Task TestGetLowestSmartRate()
        {
            UseVCR("get_lowest_smart_rate");

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            // test lowest Smart Rate with valid filters
            List<Smartrate> smartRates = await shipment.GetSmartrates();
            Smartrate lowestSmartRate = ShipmentService.GetLowestSmartrate(smartRates, 2, SmartrateAccuracy.Percentile90);
            Assert.Equal("Priority", lowestSmartRate.Service);
            Assert.Equal(8.15, lowestSmartRate.Rate);
            Assert.Equal("USPS", lowestSmartRate.Carrier);

            // test lowest Smart Rate with invalid filters (should error due to strict delivery_days)
            Assert.Throws<FilteringError>(() => ShipmentService.GetLowestSmartrate(smartRates, 0, SmartrateAccuracy.Percentile90));

            // test lowest Smart Rate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
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

        #endregion

        #endregion
    }
}
