using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

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
            UseVCR("all");

            ScanFormCollection scanFormCollection = await Client.ScanForm.All(new Dictionary<string, object?>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<ScanForm> scanForms = scanFormCollection.scan_forms;

            Assert.True(scanForms.Count <= Fixture.PageSize);
            foreach (ScanForm scanForm in scanForms)
            {
                Assert.IsType<ScanForm>(scanForm);
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            ScanForm scanForm = await GetBasicScanForm();

            Assert.IsType<ScanForm>(scanForm);
            Assert.StartsWith("sf_", scanForm.id);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            ScanForm scanForm = await GetBasicScanForm();

            ScanForm retrievedScanForm = await Client.ScanForm.Retrieve(scanForm.id);

            Assert.IsType<ScanForm>(retrievedScanForm);
            Assert.Equal(scanForm, retrievedScanForm);
        }

        private async Task<ScanForm> GetBasicScanForm()
        {
            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyShipment);
            return await Client.ScanForm.Create(new List<Shipment>
            {
                shipment
            });
        }
    }
}
