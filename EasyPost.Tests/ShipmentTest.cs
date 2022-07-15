using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.API;
using EasyPost.Parameters;
using EasyPost.Services;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class ShipmentTest : UnitTest
    {
        public ShipmentTest() : base("shipment")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            ShipmentCollection shipmentCollection = await Client.Shipments.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<Shipment> shipments = shipmentCollection.Shipments;

            Assert.IsTrue(shipments.Count <= Fixture.PageSize);
            Assert.IsNotNull(shipmentCollection.HasMore);
            foreach (Shipment shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
            }
        }

        [Fact]
        public async Task TestBuy()
        {
            UseVCR("buy", ApiVersion.Latest);

            Shipment shipment = await Client.CreateFullShipment();

            await shipment.Buy(new Shipments.Buy
            {
                RateId = shipment.LowestRate().Id
            });

            Assert.IsNotNull(shipment.PostageLabel);
        }

        [Fact]
        public async Task TestConvertLabel()
        {
            UseVCR("convert_label", ApiVersion.Latest);

            Shipment shipment = await Client.CreateOneCallBuyShipment();

            shipment = await shipment.GenerateLabel("ZPL");

            Assert.IsNotNull(shipment.PostageLabel.LabelZplUrl);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Shipment shipment = await Client.CreateFullShipment();

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.Id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.Rates);
            Assert.AreEqual("PNG", shipment.Options.LabelFormat);
            Assert.AreEqual("123", shipment.Options.InvoiceNumber);
            Assert.AreEqual("123", shipment.Reference);
        }

        [Fact]
        public async Task TestCreateEmptyObjects()
        {
            UseVCR("create_empty_objects", ApiVersion.Latest);

            Shipment shipmentData = Fixture.BasicShipment;

            shipmentData.CustomsInfo = null;

            Shipment shipment = await Client.Shipments.Create(new Shipments.Create
            {
                Shipment = shipmentData
            });

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.Id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.Options); // The EasyPost API populates some default values here
            Assert.IsTrue(shipment.CustomsInfo.CustomsItems.Count == 0);
            Assert.IsNull(shipment.Reference);
            Assert.IsNull(shipment.TaxIdentifiers);
        }

        [Fact]
        public async Task TestCreateTaxIdentifiers()
        {
            UseVCR("create_tax_identifiers", ApiVersion.Latest);

            Shipment shipmentData = Fixture.BasicShipment;
            shipmentData.TaxIdentifiers = new List<TaxIdentifier>
            {
                Fixture.TaxIdentifier
            };

            Shipment shipment = await Client.Shipments.Create(new Shipments.Create
            {
                Shipment = shipmentData
            });

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.Id.StartsWith("shp_"));
            Assert.AreEqual("IOSS", shipment.TaxIdentifiers[0].TaxIdType);
        }

        [Fact(Skip = "Test does not play well with VCR")]
        public async Task TestCreateWithIds()
        {
            UseVCR("create_with_ids", ApiVersion.Latest);

            Address fromAddress = await Client.CreateBasicAddress();
            Address toAddress = fromAddress;
            Parcel parcel = await Client.CreateBasicParcel();

            Shipment shipment = await Client.Shipments.Create(new Shipments.Create(new Dictionary<string, object>
            {
                {
                    "from_address", new Dictionary<string, object>
                    {
                        {
                            "id", fromAddress.Id
                        }
                    }
                },
                {
                    "to_address", new Dictionary<string, object>
                    {
                        {
                            "id", toAddress.Id
                        }
                    }
                },
                {
                    "parcel", new Dictionary<string, object>
                    {
                        {
                            "id", parcel.Id
                        }
                    }
                }
            }));

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.Id.StartsWith("shp_"));
            Assert.IsTrue(shipment.FromAddress.Id.StartsWith("adr_"));
            Assert.IsTrue(shipment.ToAddress.Id.StartsWith("adr_"));
            Assert.IsTrue(shipment.Parcel.Id.StartsWith("prcl_"));
            Assert.AreEqual("388 Townsend St", shipment.FromAddress.Street1);
        }

        [Fact]
        public async Task TestInstanceLowestSmartrate()
        {
            UseVCR("lowest_smartrate_instance", ApiVersion.Latest);

            Shipment shipment = await Client.CreateBasicShipment();

            // test lowest smartrate with valid filters
            Smartrate lowestSmartrate = await shipment.LowestSmartrate(1, SmartrateAccuracy.Percentile90);
            Assert.AreEqual("First", lowestSmartrate.Service);
            Assert.AreEqual(5.49, lowestSmartrate.Rate);
            Assert.AreEqual("USPS", lowestSmartrate.Carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            await Assert.ThrowsExceptionAsync<FilterFailureException>(async () => await shipment.LowestSmartrate(0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [Fact]
        public async Task TestInsure()
        {
            UseVCR("insure", ApiVersion.Latest);

            Dictionary<string, object> shipmentData = Fixture.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;

            Shipment shipment = await Client.Shipments.Create(new Shipments.Create(shipmentData));

            shipment = await shipment.Insure(100);

            Assert.AreEqual("100.00", shipment.Insurance);
        }

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate", ApiVersion.Latest);

            Shipment shipment = await Client.CreateFullShipment();

            // test lowest rate with no filters
            Rate lowestRate = shipment.LowestRate();
            Assert.AreEqual("First", lowestRate.Service);
            Assert.AreEqual("5.49", lowestRate.Price);
            Assert.AreEqual("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new List<string>
            {
                "Priority"
            };
            lowestRate = shipment.LowestRate(null, services);
            Assert.AreEqual("Priority", lowestRate.Service);
            Assert.AreEqual("7.37", lowestRate.Price);
            Assert.AreEqual("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.ThrowsException<FilterFailureException>(() => shipment.LowestRate(carriers));
        }

        // Refunding a test shipment must happen within seconds of the shipment being created as test shipments naturally
        // follow a flow of created -> delivered to cycle through tracking events in test mode - as such anything older
        // than a few seconds in test mode may not be refundable.
        [Fact]
        public async Task TestRefund()
        {
            UseVCR("refund", ApiVersion.Latest);

            Shipment shipment = await Client.CreateOneCallBuyShipment();

            shipment = await shipment.Refund();

            Assert.AreEqual("submitted", shipment.RefundStatus);
        }

        [Fact]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates", ApiVersion.Latest);

            Shipment shipment = await Client.CreateFullShipment();

            await shipment.RegenerateRates();

            List<Rate> rates = shipment.Rates;

            Assert.IsNotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Shipment shipment = await Client.CreateFullShipment();

            Shipment retrievedShipment = await Client.Shipments.Retrieve(shipment.Id);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.AreEqual(shipment, retrievedShipment);
        }

        [Fact]
        public async Task TestSmartrate()
        {
            UseVCR("smartrate", ApiVersion.Latest);

            Shipment shipment = await Client.CreateBasicShipment();

            Assert.IsNotNull(shipment.Rates);

            List<Smartrate> smartRates = await shipment.GetSmartrates();
            Smartrate smartrate = smartRates.First();
            // Must compare IDs because one is a Rate object and one is a Smartrate object
            Assert.AreEqual(shipment.Rates[0].Id, smartrate.Id);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile50);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile75);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile85);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile90);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile95);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile97);
            Assert.IsNotNull(smartrate.TimeInTransit.Percentile99);
        }

        [Fact]
        public async Task TestStaticLowestSmartrate()
        {
            UseVCR("lowest_smartrate_static", ApiVersion.Latest);

            Shipment shipment = await Client.CreateBasicShipment();

            // test lowest smartrate with valid filters
            List<Smartrate> smartrates = await shipment.GetSmartrates();
            Smartrate lowestSmartrate = ShipmentService.GetLowestSmartrate(smartrates, 1, SmartrateAccuracy.Percentile90);
            Assert.AreEqual("First", lowestSmartrate.Service);
            Assert.AreEqual(5.49, lowestSmartrate.Rate);
            Assert.AreEqual("USPS", lowestSmartrate.Carrier);

            // test lowest smartrate with invalid filters (should error due to strict delivery_days)
            Assert.ThrowsException<FilterFailureException>(() => ShipmentService.GetLowestSmartrate(smartrates, 0, SmartrateAccuracy.Percentile90));

            // test lowest smartrate with invalid filters (should error due to bad delivery_accuracy)
            // this test is not needed in the C# CL because it uses enums for the accuracy (can't pass in an incorrect value)
        }
    }
}
