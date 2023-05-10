using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters;
using EasyPost.Parameters.Shipment;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using All = EasyPost.Parameters.ScanForm.All;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class ScanFormServiceTests : UnitTest
    {
        public ScanFormServiceTests() : base("scan_form_service_with_parameters")
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

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Parameters.ScanForm.Create parameters = new()
            {
                Shipments = new List<IShipmentParameter> { shipment },
            };

            ScanForm scanForm = await Client.ScanForm.Create(parameters);

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

            All parameters = Fixtures.Parameters.ScanForms.All(data);

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
