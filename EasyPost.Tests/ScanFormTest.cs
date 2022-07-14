using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
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
            UseVCR("all", ApiVersion.Latest);

            ScanFormCollection scanFormCollection = await Client.ScanForms.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<ScanForm> scanForms = scanFormCollection.ScanForms;

            Assert.IsTrue(scanForms.Count <= Fixture.PageSize);
            Assert.IsNotNull(scanFormCollection.HasMore);
            foreach (ScanForm scanForm in scanForms)
            {
                Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            ScanForm scanForm = await GetBasicScanForm();

            Assert.IsInstanceOfType(scanForm, typeof(ScanForm));
            Assert.IsTrue(scanForm.Id.StartsWith("sf_"));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            ScanForm scanForm = await GetBasicScanForm();

            ScanForm retrievedScanForm = await Client.ScanForms.Retrieve(scanForm.Id);

            Assert.IsInstanceOfType(retrievedScanForm, typeof(ScanForm));
            Assert.AreEqual(scanForm, retrievedScanForm);
        }

        private async Task<ScanForm> GetBasicScanForm()
        {
            Shipment shipment = await Client.Shipments.Create(new Shipments.Create(Fixture.OneCallBuyShipment));
            return await Client.ScanForms.Create(new List<Shipment>
            {
                shipment
            });
        }
    }
}
