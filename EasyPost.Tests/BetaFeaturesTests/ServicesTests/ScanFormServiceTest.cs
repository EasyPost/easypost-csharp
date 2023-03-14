using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
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

            Dictionary<string, object> data = Fixtures.OneCallBuyShipment;

            Shipment shipment = await Client.Shipment.Create(data);

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

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.ScanForms.All parameters = Fixtures.Parameters.ScanForms.All(data);

            ScanFormCollection scanFormCollection = await Client.ScanForm.All(parameters);

            List<ScanForm> scanForms = scanFormCollection.ScanForms;

            Assert.True(scanForms.Count <= Fixtures.PageSize);
            foreach (ScanForm scanForm in scanForms)
            {
                Assert.IsType<ScanForm>(scanForm);
            }
        }

        #endregion

        #endregion
    }
}
