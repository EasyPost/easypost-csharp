using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ScanFormTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "scan_form", true);
        }

        private static async Task<ScanForm> GetBasicScanForm()
        {
            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyShipment);
            return await ScanForm.Create(new List<Shipment>
            {
                shipment
            });
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            ScanForm scanForm = await GetBasicScanForm();

            Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            Assert.IsTrue(scanForm.id.StartsWith("sf_"));
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            ScanForm scanForm = await GetBasicScanForm();

            ScanForm retrievedScanForm = await ScanForm.Retrieve(scanForm.id);

            Assert.IsInstanceOfType(retrievedScanForm, typeof(ScanForm));
            Assert.AreEqual(scanForm, retrievedScanForm);
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            ScanFormCollection scanFormCollection = await ScanForm.All(new Dictionary<string, object>
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
