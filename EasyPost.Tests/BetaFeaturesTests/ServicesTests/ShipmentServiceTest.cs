using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.BetaFeatures.Parameters.Shipments;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class ShipmentServiceTests : UnitTest
    {
        public ShipmentServiceTests() : base("shipment_service_with_parameters")
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

            Dictionary<string, object> data = Fixtures.FullShipment;

            BetaFeatures.Parameters.Shipments.Create parameters = Fixtures.Parameters.Shipments.Create(data);

            Shipment shipment = await Client.Shipment.Create(parameters);

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
        public async Task TestCreateWithTaxIdentifiers()
        {
            UseVCR("create_with_tax_identifiers");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>> { Fixtures.TaxIdentifier };

            BetaFeatures.Parameters.Shipments.Create parameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(parameters);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.Equal("IOSS", shipment.TaxIdentifiers[0].TaxIdType);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateWithCarbonOffset()
        {
            UseVCR("create_with_carbon_offset");

            Dictionary<string, object> data = Fixtures.BasicShipment;
            data["carbon_offset"] = true;

            BetaFeatures.Parameters.Shipments.Create parameters = Fixtures.Parameters.Shipments.Create(data);

            Shipment shipment = await Client.Shipment.Create(parameters);

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

            Dictionary<string, object> fromAddressData = Fixtures.CaAddress1;
            Dictionary<string, object> toAddressData = Fixtures.CaAddress2;
            Dictionary<string, object> parcelData = Fixtures.BasicParcel;

            BetaFeatures.Parameters.Addresses.Create fromAddressParameters = Fixtures.Parameters.Addresses.Create(fromAddressData);
            BetaFeatures.Parameters.Addresses.Create toAddressParameters = Fixtures.Parameters.Addresses.Create(toAddressData);
            BetaFeatures.Parameters.Parcels.Create parcelParameters = Fixtures.Parameters.Parcels.Create(parcelData);

            Address fromAddress = await Client.Address.Create(fromAddressParameters);
            Address toAddress = await Client.Address.Create(toAddressParameters);
            Parcel parcel = await Client.Parcel.Create(parcelParameters);

            Dictionary<string, object> shipmentData = new Dictionary<string, object>
            {
                { "from_address", new Dictionary<string, object> { { "id", fromAddress.Id } } },
                { "to_address", new Dictionary<string, object> { { "id", toAddress.Id } } },
                { "parcel", new Dictionary<string, object> { { "id", parcel.Id } } },
            };

            BetaFeatures.Parameters.Shipments.Create parameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Dictionary<string, object> dictionary = parameters.ToDictionary();

            Shipment shipment = await Client.Shipment.Create(parameters);

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

            Dictionary<string, object> data = Fixtures.OneCallBuyShipment;
            data["carbon_offset"] = true;

            BetaFeatures.Parameters.Shipments.Create parameters = Fixtures.Parameters.Shipments.Create(data);

            Shipment shipment = await Client.Shipment.Create(parameters);

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

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Shipments.All parameters = Fixtures.Parameters.Shipments.All(data);

            ShipmentCollection shipmentCollection = await Client.Shipment.All(parameters);

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
        public async Task TestAllParameterHandOff() {
            UseVCR("all_parameter_hand_off");

            BetaFeatures.Parameters.Shipments.All filters = new All
            {
                IncludeChildren = true,
                Purchased = false,
            };

            ShipmentCollection shipmentCollection = await Client.Shipment.All(filters);

            Assert.Equal(filters.IncludeChildren, shipmentCollection.IncludeChildren);
            Assert.Equal(filters.Purchased, shipmentCollection.Purchased);
        }

        #endregion

        #endregion
    }
}
