using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class ShipmentTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        [TestMethod]
        public void TestCreate()
        {
            Shipment shipment = Shipment.Create(Fixture.FullShipment);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.rates);
            Assert.AreEqual("PNG", shipment.options.label_format);
            Assert.AreEqual("123", shipment.options.invoice_number);
            Assert.AreEqual("123", shipment.reference);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            Shipment shipment = Shipment.Create(Fixture.FullShipment);

            Shipment retrievedShipment = Shipment.Retrieve(shipment.id);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.AreEqual(shipment.id, retrievedShipment.id);
        }

        [TestMethod]
        public void TestAll()
        {
            ShipmentList shipmentList = Shipment.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Shipment> shipments = shipmentList.shipments;

            Assert.IsTrue(shipments.Count <= Fixture.PageSize);
            Assert.IsNotNull(shipmentList.has_more);
            foreach (var shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
            }
        }

        [TestMethod]
        public void TestBuy()
        {
            Shipment shipment = Shipment.Create(Fixture.FullShipment);

            shipment.Buy(shipment.LowestRate());

            Assert.IsNotNull(shipment.postage_label);
        }

        [TestMethod]
        public void TestRegenerateRates()
        {
            Shipment shipment = Shipment.Create(Fixture.FullShipment);

            shipment.RegenerateRates();

            List<Rate> rates = shipment.rates;

            Assert.IsNotNull(rates);
            foreach (var rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [TestMethod]
        public void TestConvertLabel()
        {
            Shipment shipment = Shipment.Create(Fixture.OneCallBuyShipment);

            shipment.GenerateLabel("ZPL");

            Assert.IsNotNull(shipment.postage_label.label_zpl_url);
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [TestMethod]
        public void TestInsure()
        {
            Dictionary<string, object> shipmentData = Fixture.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;

            Shipment shipment = Shipment.Create(shipmentData);

            shipment.Insure(100);

            Assert.AreEqual("100.00", shipment.insurance);
        }

        // Refunding a test shipment must happen within seconds of the shipment being created as test shipments naturally
        // follow a flow of created -> delivered to cycle through tracking events in test mode - as such anything older
        // than a few seconds in test mode may not be refundable.
        [TestMethod]
        public void TestRefund()
        {
            Shipment shipment = Shipment.Create(Fixture.OneCallBuyShipment);

            shipment.Refund();

            Assert.AreEqual("submitted", shipment.refund_status);
        }

        [TestMethod]
        public void TestSmartrate()
        {
            Shipment shipment = Shipment.Create(Fixture.BasicShipment);

            Assert.IsNotNull(shipment.rates);

            List<Smartrate> smartRates = shipment.GetSmartrates();
            Smartrate smartrate = smartRates.First();
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
        public void TestCreateEmptyObjects()
        {
            Dictionary<string, object> shipmentData = Fixture.BasicShipment;

            shipmentData.Add("customs_info", new Dictionary<string, object>());
            (shipmentData["customs_info"] as Dictionary<string, object>).Add("customs_items", new List<object>());
            shipmentData["options"] = null;
            shipmentData["tax_identifiers"] = null;
            shipmentData["reference"] = "";

            Shipment shipment = Shipment.Create(shipmentData);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.options); // The EasyPost API populates some default values here
            Assert.IsTrue(shipment.customs_info.customs_items.Count == 0);
            Assert.IsNull(shipment.reference);
        }

        [TestMethod]
        public void TestCreateTaxIdentifiers()
        {
            Dictionary<string, object> shipmentData = Fixture.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>>
            {
                Fixture.TaxIdentifier
            };

            Shipment shipment = Shipment.Create(shipmentData);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.AreEqual("IOSS", shipment.tax_identifiers[0].tax_id_type);
        }
    }
}
