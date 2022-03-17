using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class ScanFormTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "scan_form", true);
        }

        private static ScanForm GetBasicScanForm()
        {
            Shipment shipment = Shipment.Create(Fixture.OneCallBuyShipment);
            return ScanForm.Create(new List<Shipment>
            {
                shipment
            });
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            ScanForm scanForm = GetBasicScanForm();

            Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            Assert.IsTrue(scanForm.id.StartsWith("sf_"));
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");


            ScanForm scanForm = GetBasicScanForm();

            ScanForm retrievedScanForm = ScanForm.Retrieve(scanForm.id);

            Assert.IsInstanceOfType(retrievedScanForm, typeof(ScanForm));
            Assert.AreEqual(scanForm.id, retrievedScanForm.id);
        }

        [TestMethod]
        public void TestAll()
        {
            VCR.Replay("all");

            ScanFormCollection scanFormCollection = ScanForm.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<ScanForm> scanForms = scanFormCollection.scan_forms;

            Assert.IsTrue(scanForms.Count <= Fixture.PageSize);
            Assert.IsNotNull(scanFormCollection.has_more);
            foreach (var scanForm in scanForms)
            {
                Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            }
        }
    }
}
