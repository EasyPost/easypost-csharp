using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ScanFormTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestScanFormList() {
            Dictionary<string, object> dict = new Dictionary<string, object>() { { "page_size", 1 } };
            ScanFormList scanFormList = ScanForm.List(dict);
            Assert.AreNotEqual(null, scanFormList.scanForms[0].batch_id);
            Assert.AreNotEqual(0, scanFormList.scanForms.Count);
            ScanFormList nextScanFormList = scanFormList.Next();
            Assert.AreNotEqual(scanFormList.scanForms[0].id, nextScanFormList.scanForms[0].id);
        }

        [TestMethod]
        public void TestScanFormCreateAndRetrieve() {
            // XXX: This is not repeatable. Need a new shipment id each time.
            List<Shipment> shipments = new List<Shipment>() {
                new Shipment() { id = "shp_1b84b4a90660417bba7a2cda5152f681" }
            };

            ScanForm scanForm = ScanForm.Create(shipments);
            Assert.IsNotNull(scanForm.id);

            ScanForm otherScanForm = ScanForm.Retrieve(scanForm.id);
            Assert.AreEqual(scanForm.id, otherScanForm.id);
        }
    }
}
