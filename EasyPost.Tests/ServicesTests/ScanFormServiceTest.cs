using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class ScanFormServiceTests : UnitTest
    {
        public ScanFormServiceTests() : base("scan_form_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            ScanForm scanForm = await Client.ScanForm.Create(new List<Shipment> { shipment });

            Assert.IsType<ScanForm>(scanForm);
            Assert.StartsWith("sf_", scanForm.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            ScanFormCollection scanFormCollection = await Client.ScanForm.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<ScanForm> scanForms = scanFormCollection.ScanForms;

            Assert.True(scanForms.Count <= Fixtures.PageSize);
            foreach (ScanForm scanForm in scanForms)
            {
                Assert.IsType<ScanForm>(scanForm);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            ScanForm scanForm = await Client.ScanForm.Create(new List<Shipment> { shipment });

            ScanForm retrievedScanForm = await Client.ScanForm.Retrieve(scanForm.Id);

            Assert.IsType<ScanForm>(retrievedScanForm);
            Assert.Equal(scanForm, retrievedScanForm);
        }

        #endregion

        #endregion
    }
}
