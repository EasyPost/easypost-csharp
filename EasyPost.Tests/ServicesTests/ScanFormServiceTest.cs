using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
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
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            ScanFormCollection collection = await Client.ScanForm.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                ScanFormCollection nextPageCollection = await Client.ScanForm.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.ScanForms[0].Id, nextPageCollection.ScanForms[0].Id);
            }
            catch (EndOfPaginationError) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
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
