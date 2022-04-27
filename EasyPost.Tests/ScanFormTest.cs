using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ScanFormTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("scan_form");
        }

        private static async Task<ScanForm> GetBasicScanForm(V2Client v2Client)
        {
            Shipment shipment = await v2Client.Shipments.Create(Fixture.OneCallBuyShipment);
            return await v2Client.ScanForms.Create(new List<Shipment>
            {
                shipment
            });
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            ScanForm scanForm = await GetBasicScanForm(v2Client);

            Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            Assert.IsTrue(scanForm.id.StartsWith("sf_"));
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            ScanForm scanForm = await GetBasicScanForm(v2Client);

            ScanForm retrievedScanForm = await v2Client.ScanForms.Retrieve(scanForm.id);

            Assert.IsInstanceOfType(retrievedScanForm, typeof(ScanForm));
            Assert.AreEqual(scanForm, retrievedScanForm);
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client v2Client = _vcr.SetUpTest("all");

            ScanFormCollection scanFormCollection = await v2Client.ScanForms.All(new Dictionary<string, object>
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
