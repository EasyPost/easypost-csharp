using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EasyPostTest {
    [TestClass]
    public class SmartRateTest:Resource
    {
        Dictionary<string, object> parameters, options, toAddress, fromAddress;

        [TestInitialize]
        public void Initialize()
        {

            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            toAddress = new Dictionary<string, object>() {
                { "name", "Dr. Steve Brule" },
                { "street1", "179 N Harbor Dr" },
                { "city", "Redondo Beach" },
                { "state", "CA" },
                { "zip", "90277" },
                { "country", "US" },
                { "phone", "4153334444" },
                { "email", "dr_steve_brule@gmail.com" }
            };
            fromAddress = new Dictionary<string, object>() {
                { "name", "EasyPost" },
                { "street1", "417 Montgomery Street" },
                { "street2", "5th Floor" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94104" },
                {"phone",  "4153334444"},
                {"email", "support@easypost.com" }
            };
            options = new Dictionary<string, object>();

            parameters = new Dictionary<string, object>() {
                {"parcel", new Dictionary<string, object>()
                    {
                        { "length", 20.2 },
                        { "width", 10.9 },
                        { "height", 5 },
                        { "weight", 65.9 }
                    }
                },
                { "to_address", toAddress },
                { "from_address", fromAddress },
                { "reference", "ShipmentRef" },
                { "options", options }
            };
        }

        [TestMethod]
        public void LoadSmartRateResultsCorrectly()
        {
            string jsonData = File.ReadAllText("SmartRateData.txt");
            SmartRateResult smartRateResult = SmartRateResult.Load<SmartRateResult>(jsonData);
            Assert.IsNotNull(smartRateResult);
        }

        [TestMethod]
        public void ApiCallIsNotNull()
        {
            Shipment shipment = Shipment.Create(parameters);
            SmartRateResult smartRateResult = shipment.GetSmartRates();
            Assert.IsNotNull(smartRateResult);                                                        
        }

        [TestMethod]
        public void TestGetSmartRates()
        {
            Shipment shipment = Shipment.Create(parameters);
            SmartRateResult smartRateResult = shipment.GetSmartRates();
            //Make sure shipment id from smartrate is the same as the created one
            Assert.AreEqual(shipment.rates[0].id, smartRateResult.result[0].id);
            Assert.IsNotNull(shipment.rates);
        }

        //Welp this changes each time. Replicated from the python version. Will fail since percentiles are changing each time.
        [TestMethod]
        public void TestTimeInTransitData()
        {
            Shipment shipment = Shipment.Create(parameters);
            SmartRateResult smartRateResult = shipment.GetSmartRates();
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_50, 1);
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_75, 2);
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_85, 3);
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_90, 3);
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_95, 3);
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_97, 4);
            Assert.AreEqual(smartRateResult.result[0].time_in_transit.percentile_99, 5);
        }
    }
}
