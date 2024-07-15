using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.ServicesTests.WithParameters
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

            Parameters.Shipment.Create parameters = Fixtures.Parameters.Shipments.Create(data);

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

            Parameters.Shipment.Create parameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(parameters);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.Equal("IOSS", shipment.TaxIdentifiers[0].TaxIdType);
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

            Parameters.Address.Create fromAddressParameters = Fixtures.Parameters.Addresses.Create(fromAddressData);
            Parameters.Address.Create toAddressParameters = Fixtures.Parameters.Addresses.Create(toAddressData);
            Parameters.Parcel.Create parcelParameters = Fixtures.Parameters.Parcels.Create(parcelData);

            Address fromAddress = await Client.Address.Create(fromAddressParameters);
            Address toAddress = await Client.Address.Create(toAddressParameters);
            Parcel parcel = await Client.Parcel.Create(parcelParameters);

            Dictionary<string, object> shipmentData = new()
            {
                { "from_address", new Dictionary<string, object> { { "id", fromAddress.Id } } },
                { "to_address", new Dictionary<string, object> { { "id", toAddress.Id } } },
                { "parcel", new Dictionary<string, object> { { "id", parcel.Id } } },
            };

            Parameters.Shipment.Create parameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(parameters);

            Assert.IsType<Shipment>(shipment);
            Assert.StartsWith("shp_", shipment.Id);
            Assert.StartsWith("adr_", shipment.FromAddress.Id);
            Assert.StartsWith("adr_", shipment.ToAddress.Id);
            Assert.StartsWith("prcl_", shipment.Parcel.Id);
            Assert.Equal("388 Townsend St", shipment.FromAddress.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            Parameters.Shipment.All parameters = Fixtures.Parameters.Shipments.All(data);

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
        public async Task TestAllParameterHandOff()
        {
            UseVCR("all_parameter_hand_off");

            Parameters.Shipment.All filters = new()
            {
                IncludeChildren = true,
                Purchased = false,
            };

            ShipmentCollection shipmentCollection = await Client.Shipment.All(filters);

            // Filters should be cached in in the resultant collection object.
            Assert.NotNull(shipmentCollection.Filters);
            Assert.IsType<Parameters.Shipment.All>(shipmentCollection.Filters);

            Parameters.Shipment.All cachedFilters = (Parameters.Shipment.All)shipmentCollection.Filters;
            Assert.Equal(filters.IncludeChildren, cachedFilters.IncludeChildren);
            Assert.Equal(filters.Purchased, cachedFilters.Purchased);
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
            Parameters.Shipment.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Parameters.Shipment.Insure insureParameters = new()
            {
                Amount = "100",
            };

            shipment = await Client.Shipment.Insure(shipment.Id, insureParameters);

            Assert.Equal("100.00", shipment.Insurance);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Parameters.Shipment.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            // buy with rate ID
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            Parameters.Shipment.Buy buyParameters = new(rate.Id);
            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

            Assert.NotNull(shipment.PostageLabel);

            // buy with rate
            shipment = await Client.Shipment.Create(shipmentParameters);
            rate = shipment.LowestRate();

            buyParameters = new(rate);
            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithEndShipper()
        {
            UseVCR("buy_with_end_shipper");

            Dictionary<string, object> endShipperData = Fixtures.CaAddress1;
            Parameters.EndShipper.Create endShipperParameters = Fixtures.Parameters.EndShippers.Create(endShipperData);
            EndShipper endShipper = await Client.EndShipper.Create(endShipperParameters);

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Parameters.Shipment.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            Parameters.Shipment.Buy buyParameters = new(rate)
            {
                EndShipperId = endShipper.Id,
            };

            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestGenerateLabel()
        {
            UseVCR("generate_label");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;
            Parameters.Shipment.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Parameters.Shipment.GenerateLabel generateLabelParameters = new()
            {
                FileFormat = "ZPL",
            };

            shipment = await Client.Shipment.GenerateLabel(shipment.Id, generateLabelParameters);

            Assert.NotNull(shipment.PostageLabel.LabelZplUrl);
        }

        [Fact]
        [Testing.Function]
        public async Task TestGenerateForm()
        {
            UseVCR("generate_form");

            const string formType = "label_qr_code";

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Parameters.Shipment.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            CustomAssertions.None(shipment.Forms, form => Assert.Equal(formType, form.FormType));

            Rate rate = shipment.LowestRate();

            Parameters.Shipment.Buy buyParameters = new(rate);

            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

            Parameters.Shipment.GenerateForm generateFormParameters = new()
            {
                Type = formType,
            };

            shipment = await Client.Shipment.GenerateForm(shipment.Id, generateFormParameters);

            Assert.NotNull(shipment.Forms);
            CustomAssertions.Any(shipment.Forms, form => Assert.Equal(formType, form.FormType));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Parameters.Shipment.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Parameters.Shipment.RegenerateRates regenerateRatesParameters = new();

            await Client.Shipment.RegenerateRates(shipment.Id, regenerateRatesParameters);

            List<Rate> rates = shipment.Rates;

            Assert.NotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsType<Rate>(rate);
            }
        }

        [Fact]
        [Testing.Function]
        public async Task TestEstimatedDeliveryDates()
        {
            UseVCR("estimated_delivery_dates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.Parameters.Shipments.Create(Fixtures.BasicShipment));

            Parameters.Shipment.RetrieveEstimatedDeliveryDate retrieveEstimatedDeliveryDatesParameters = new()
            {
                PlannedShipDate = Fixtures.PlannedShipDate,
            };

            List<RateWithEstimatedDeliveryDate> ratesWithEstimatedDeliveryDates = await Client.Shipment.RetrieveEstimatedDeliveryDate(shipment.Id, retrieveEstimatedDeliveryDatesParameters);

            foreach (var rate in ratesWithEstimatedDeliveryDates)
            {
                Assert.NotNull(rate.EasyPostTimeInTransitData);
                Assert.NotNull(rate.EasyPostTimeInTransitData.EasyPostEstimatedDeliveryDate);
                Assert.NotNull(rate.EasyPostTimeInTransitData.DaysInTransit);
                Assert.NotNull(rate.EasyPostTimeInTransitData.PlannedShipDate);
            }
        }

        #endregion

        #endregion
    }
}
