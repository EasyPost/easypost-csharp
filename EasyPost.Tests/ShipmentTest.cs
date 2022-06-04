﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Models.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class ShipmentTest : UnitTest
    {
        public ShipmentTest() : base("shipment", TestUtils.ApiKey.Test)
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            ShipmentCollection shipmentCollection = await V2Client.Shipments.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Shipment> shipments = shipmentCollection.shipments;

            Assert.IsTrue(shipments.Count <= Fixture.PageSize);
            Assert.IsNotNull(shipmentCollection.has_more);
            foreach (Shipment shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
            }
        }

        [Fact]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.FullShipment);

            await shipment.Buy(shipment.LowestRate());

            Assert.IsNotNull(shipment.postage_label);
        }

        [Fact]
        public async Task TestConvertLabel()
        {
            UseVCR("convert_label");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.OneCallBuyShipment);

            await shipment.GenerateLabel("ZPL");

            Assert.IsNotNull(shipment.postage_label.label_zpl_url);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.FullShipment);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.rates);
            Assert.AreEqual("PNG", shipment.options.label_format);
            Assert.AreEqual("123", shipment.options.invoice_number);
            Assert.AreEqual("123", shipment.reference);
        }

        [Fact]
        public async Task TestCreateEmptyObjects()
        {
            UseVCR("create_empty_objects");

            Dictionary<string, object> shipmentData = Fixture.BasicShipment;

            shipmentData.Add("customs_info", new Dictionary<string, object>());
            Assert.IsNotNull(shipmentData["customs_info"]);
            (shipmentData["customs_info"] as Dictionary<string, object>).Add("customs_items", new List<object>());
            shipmentData["options"] = null;
            shipmentData["tax_identifiers"] = null;
            shipmentData["reference"] = "";

            Shipment shipment = await V2Client.Shipments.Create(shipmentData);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsNotNull(shipment.options); // The EasyPost API populates some default values here
            Assert.IsTrue(shipment.customs_info.customs_items.Count == 0);
            Assert.IsNull(shipment.reference);
            Assert.IsNull(shipment.tax_identifiers);
        }

        [Fact]
        public async Task TestCreateTaxIdentifiers()
        {
            UseVCR("create_tax_identifiers");

            Dictionary<string, object> shipmentData = Fixture.BasicShipment;
            shipmentData["tax_identifiers"] = new List<Dictionary<string, object>>
            {
                Fixture.TaxIdentifier
            };

            Shipment shipment = await V2Client.Shipments.Create(shipmentData);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.AreEqual("IOSS", shipment.tax_identifiers[0].tax_id_type);
        }

        [Fact(Skip = "Test does not play well with VCR")]
        public async Task TestCreateWithIds()
        {
            UseVCR("create_with_ids");

            Address fromAddress = await V2Client.Addresses.Create(Fixture.BasicAddress);
            Address toAddress = await V2Client.Addresses.Create(Fixture.BasicAddress);
            Parcel parcel = await V2Client.Parcels.Create(Fixture.BasicParcel);

            Shipment shipment = await V2Client.Shipments.Create(new Dictionary<string, object>
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

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.IsTrue(shipment.id.StartsWith("shp_"));
            Assert.IsTrue(shipment.from_address.id.StartsWith("adr_"));
            Assert.IsTrue(shipment.to_address.id.StartsWith("adr_"));
            Assert.IsTrue(shipment.parcel.id.StartsWith("prcl_"));
            Assert.AreEqual("388 Townsend St", shipment.from_address.street1);
        }

        // If the shipment was purchased with a USPS rate, it must have had its insurance set to `0` when bought
        // so that USPS doesn't automatically insure it so we could manually insure it here.
        [Fact]
        public async Task TestInsure()
        {
            UseVCR("insure");

            Dictionary<string, object> shipmentData = Fixture.OneCallBuyShipment;
            // Set to 0 so USPS doesn't insure this automatically and we can insure the shipment manually
            shipmentData["insurance"] = 0;

            Shipment shipment = await V2Client.Shipments.Create(shipmentData);

            await shipment.Insure(100);

            Assert.AreEqual("100.00", shipment.insurance);
        }

        // Refunding a test shipment must happen within seconds of the shipment being created as test shipments naturally
        // follow a flow of created -> delivered to cycle through tracking events in test mode - as such anything older
        // than a few seconds in test mode may not be refundable.
        [Fact]
        public async Task TestRefund()
        {
            UseVCR("refund");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.OneCallBuyShipment);

            await shipment.Refund();

            Assert.AreEqual("submitted", shipment.refund_status);
        }

        [Fact]
        public async Task TestRegenerateRates()
        {
            UseVCR("regenerate_rates");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.FullShipment);

            await shipment.RegenerateRates();

            List<Rate> rates = shipment.rates;

            Assert.IsNotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.FullShipment);

            Shipment retrievedShipment = await V2Client.Shipments.Retrieve(shipment.id);

            Assert.IsInstanceOfType(shipment, typeof(Shipment));
            Assert.AreEqual(shipment, retrievedShipment);
        }

        [Fact]
        public async Task TestSmartrate()
        {
            UseVCR("smartrate");

            Shipment shipment = await V2Client.Shipments.Create(Fixture.BasicShipment);

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
    }
}
