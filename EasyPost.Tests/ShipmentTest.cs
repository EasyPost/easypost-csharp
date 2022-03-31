using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class ShipmentTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "shipment", true);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Shipment shipment = await Shipment.Create(Fixture.FullShipment);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.rates);
            Assert.AreEqual("PNG", shipment.options.label_format);
            Assert.AreEqual("123", shipment.options.invoice_number);
            Assert.AreEqual("123", shipment.reference);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            Shipment shipment = await Shipment.Create(Fixture.FullShipment);

            Shipment retrievedShipment = await Shipment.Retrieve(shipment.id);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.AreEqual(shipment, retrievedShipment);
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            ShipmentCollection shipmentCollection = await Shipment.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Shipment> shipments = shipmentCollection.shipments;

            Assert.IsTrue(shipments.Count <= Fixture.PageSize);
            Assert.IsNotNull(shipmentCollection.has_more);
            foreach (var shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
            }
        }

        [TestMethod]
        public async Task TestBuy()
        {
            VCR.Replay("buy");


            Shipment shipment = await Shipment.Create(Fixture.FullShipment);

            await shipment.Buy(shipment.LowestRate());

            Assert.IsNotNull(shipment.postage_label);
        }

        [TestMethod]
        public async Task TestRegenerateRates()
        {
            VCR.Replay("regenerate_rates");


            Shipment shipment = await Shipment.Create(Fixture.FullShipment);

            await shipment.RegenerateRates();

            List<Rate> rates = shipment.rates;

            Assert.IsNotNull(rates);
            foreach (var rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [TestMethod]
        public async Task TestConvertLabel()
        {
            VCR.Replay("convert_label");

            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyShipment);

            await shipment.GenerateLabel("ZPL");

            Assert.IsNotNull(shipment.postage_label.label_zpl_url);
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [TestMethod]
        public async Task TestInsure()
        {
            VCR.Replay("insure");

            Dictionary<string, object> shipmentData = Fixture.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;

            Shipment shipment = await Shipment.Create(shipmentData);

            await shipment.Insure(100);

            Assert.AreEqual("100.00", shipment.insurance);
        }

        // Refunding a test shipment must happen within seconds of the shipment being created as test shipments naturally
        // follow a flow of created -> delivered to cycle through tracking events in test mode - as such anything older
        // than a few seconds in test mode may not be refundable.
        [TestMethod]
        public async Task TestRefund()
        {
            VCR.Replay("refund");

            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyShipment);

            await shipment.Refund();

            Assert.AreEqual("submitted", shipment.refund_status);
        }

        [TestMethod]
        public async Task TestSmartrate()
        {
            VCR.Replay("smartrate");

            Shipment shipment = await Shipment.Create(Fixture.BasicShipment);

            Assert.IsNotNull(shipment.rates);

            List<Smartrate> smartRates = await shipment.GetSmartrates();
            Smartrate smartrate = smartRates.First();
            // Must compare IDs because one is a Rate object and one is a Smartrate object
            Assert.AreEqual(shipment.rates[0].id, smartrate.id);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_50);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_75);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_85);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_90);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_95);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_97);
            Assert.IsNotNull(smartrate.time_in_transit.percentile_99);
        }

        [TestMethod]
        public async Task TestCreateEmptyObjects()
        {
            VCR.Replay("create_empty_objects");

            Dictionary<string, object> shipmentData = Fixture.BasicShipment;

            shipmentData.Add("customs_info", new Dictionary<string, object>());
            Assert.IsNotNull(shipmentData["customs_info"]);
            (shipmentData["customs_info"] as Dictionary<string, object>).Add("customs_items", new List<object>());
            shipmentData["options"] = null;
            shipmentData["tax_identifiers"] = null;
            shipmentData["reference"] = "";

            Shipment shipment = await Shipment.Create(shipmentData);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.options); // The EasyPost API populates some default values here
            Assert.IsTrue(shipment.customs_info.customs_items.Count == 0);
            Assert.IsNull(shipment.reference);
            Assert.IsNull(shipment.tax_identifiers);
        }

        [TestMethod]
        public async Task TestCreateTaxIdentifiers()
        {
            VCR.Replay("create_tax_identifiers");

            Dictionary<string, object> shipmentData = Fixture.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>>
            {
                Fixture.TaxIdentifier
            };

            Shipment shipment = await Shipment.Create(shipmentData);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.AreEqual("IOSS", shipment.tax_identifiers[0].tax_id_type);
        }

        [TestMethod]
        public async Task TestCreateWithIds()
        {
            VCR.Replay("create_with_ids");

            Address fromAddress = await Address.Create(Fixture.BasicAddress);
            Address toAddress = await Address.Create(Fixture.BasicAddress);
            Parcel parcel = await Parcel.Create(Fixture.BasicParcel);

            Shipment shipment = await Shipment.Create(new Dictionary<string, object>() {
                { "from_address", new Dictionary<string, object> { { "id", fromAddress.id } } },
                { "to_address", new Dictionary<string, object> { { "id", toAddress.id } } },
                { "parcel", new Dictionary<string, object> { { "id", parcel.id } } },
            });

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsTrue(shipment.from_address.id.StartsWith("adr_"));
            Assert.IsTrue(shipment.to_address.id.StartsWith("adr_"));
            Assert.IsTrue(shipment.parcel.id.StartsWith("prcl_"));
            Assert.AreEqual("388 Townsend St", shipment.from_address.street1);
        }
    }
}
