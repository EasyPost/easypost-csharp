using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class ScanFormTest : UnitTest
    {
        public ScanFormTest() : base("scan_form")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.V2);

            ScanFormCollection scanFormCollection = await Client.ScanForms.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<ScanForm> scanForms = scanFormCollection.scan_forms;

            Assert.IsTrue(scanForms.Count <= Fixture.PageSize);
            Assert.IsNotNull(scanFormCollection.has_more);
            foreach (ScanForm scanForm in scanForms)
            {
                Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.V2);

            ScanForm scanForm = await GetBasicScanForm();

            Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            Assert.IsTrue(scanForm.id.StartsWith("sf_"));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            ScanForm scanForm = await GetBasicScanForm();

            ScanForm retrievedScanForm = await Client.ScanForms.Retrieve(scanForm.id);

            Assert.IsInstanceOfType(retrievedScanForm, typeof(ScanForm));
            Assert.AreEqual(scanForm, retrievedScanForm);
        }

        private async Task<ScanForm> GetBasicScanForm()
        {
            Shipment shipment = await Client.Shipments.Create(Fixture.OneCallBuyShipment);
            return await Client.ScanForms.Create(new List<Shipment>
            {
                shipment
            });
        }
    }
}
