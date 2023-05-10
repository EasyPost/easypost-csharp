using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.Shipment;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

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

            Create parameters = Fixtures.Parameters.Shipments.Create(data);

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

            Create parameters = Fixtures.Parameters.Shipments.Create(shipmentData);

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

            Create parameters = Fixtures.Parameters.Shipments.Create(data);

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

            Parameters.Address.Create fromAddressParameters = Fixtures.Parameters.Addresses.Create(fromAddressData);
            Parameters.Address.Create toAddressParameters = Fixtures.Parameters.Addresses.Create(toAddressData);
            Parameters.Parcel.Create parcelParameters = Fixtures.Parameters.Parcels.Create(parcelData);

            Address fromAddress = await Client.Address.Create(fromAddressParameters);
            Address toAddress = await Client.Address.Create(toAddressParameters);
            Parcel parcel = await Client.Parcel.Create(parcelParameters);

            Dictionary<string, object> shipmentData = new Dictionary<string, object>
            {
                { "from_address", new Dictionary<string, object> { { "id", fromAddress.Id } } },
                { "to_address", new Dictionary<string, object> { { "id", toAddress.Id } } },
                { "parcel", new Dictionary<string, object> { { "id", parcel.Id } } },
            };

            Create parameters = Fixtures.Parameters.Shipments.Create(shipmentData);

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

            Create parameters = Fixtures.Parameters.Shipments.Create(data);

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

            All parameters = Fixtures.Parameters.Shipments.All(data);

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

            All filters = new All
            {
                IncludeChildren = true,
                Purchased = false,
            };

            ShipmentCollection shipmentCollection = await Client.Shipment.All(filters);

            // Filters should be cached in in the resultant collection object.
            Assert.NotNull(shipmentCollection.Filters);
            Assert.IsType<All>(shipmentCollection.Filters);

            All cachedFilters = (All)shipmentCollection.Filters;
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
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Insure insureParameters = new Insure
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
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            // buy with rate ID
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            Buy buyParameters = new Buy(rate.Id);
            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

            Assert.NotNull(shipment.PostageLabel);

            // buy with rate
            shipment = await Client.Shipment.Create(shipmentParameters);
            rate = shipment.LowestRate();

            buyParameters = new Buy(rate);
            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

            Assert.NotNull(shipment.PostageLabel);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithCarbonOffset()
        {
            UseVCR("buy_with_carbon_offset");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            Buy buyParameters = new Buy(rate)
            {
                CarbonOffset = true,
            };

            shipment = await Client.Shipment.Buy(shipment.Id, buyParameters);

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

            Dictionary<string, object> endShipperData = Fixtures.CaAddress1;
            Parameters.EndShipper.Create endShipperParameters = Fixtures.Parameters.EndShippers.Create(endShipperData);
            EndShipper endShipper = await Client.EndShipper.Create(endShipperParameters);

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            Rate rate = shipment.LowestRate();

            Buy buyParameters = new Buy(rate)
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
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            GenerateLabel generateLabelParameters = new GenerateLabel
            {
                FileFormat = "ZPL",
            };

            shipment = await Client.Shipment.GenerateLabel(shipment.Id, generateLabelParameters);

            Assert.NotNull(shipment.PostageLabel.LabelZplUrl);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Dictionary<string, object> shipmentData = Fixtures.FullShipment;
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            RegenerateRates regenerateRatesParameters = new RegenerateRates();

            await Client.Shipment.RegenerateRates(shipment.Id, regenerateRatesParameters);

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

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;
            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);
            Shipment shipment = await Client.Shipment.Create(shipmentParameters);
            List<Rate> baseRates = shipment.Rates;

            RegenerateRates regenerateRatesParameters = new RegenerateRates
            {
                CarbonOffset = true,
            };
            shipment = await Client.Shipment.RegenerateRates(shipment.Id, regenerateRatesParameters);

            List<Rate> newRatesWithCarbon = shipment.Rates;

            Rate baseRate = baseRates!.First();
            Rate newRateWithCarbon = newRatesWithCarbon!.First();

            Assert.Null(baseRate.CarbonOffset);
            Assert.NotNull(newRateWithCarbon.CarbonOffset);
        }

        [Fact]
        [Testing.Function]
        public async Task TestRetrieveEstimatedDeliveryDates()
        {
            UseVCR("estimated_delivery_dates");

            Shipment shipment = await Client.Shipment.Create(Fixtures.Parameters.Shipments.Create(Fixtures.BasicShipment));

            RetrieveEstimatedDeliveryDate retrieveEstimatedDeliveryDatesParameters = new RetrieveEstimatedDeliveryDate
            {
                PlannedShipDate = Fixtures.PlannedShipDate,
            };

            List<RateWithEstimatedDeliveryDate> ratesWithEstimatedDeliveryDates = await Client.Shipment.RetrieveEstimatedDeliveryDate(shipment.Id, retrieveEstimatedDeliveryDatesParameters);

            foreach (var rate in ratesWithEstimatedDeliveryDates)
            {
                Assert.NotNull(rate.EasyPostTimeInTransitData);
            }
        }

        #endregion

        #endregion
    }
}
