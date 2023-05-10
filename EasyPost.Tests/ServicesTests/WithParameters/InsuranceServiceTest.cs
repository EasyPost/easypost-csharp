using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.Shipment;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using All = EasyPost.Parameters.Insurance.All;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class InsuranceServiceTests : UnitTest
    {
        public InsuranceServiceTests() : base("insurance_service_with_parameters")
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

            Dictionary<string, object> insuranceData = Fixtures.BasicInsurance;
            insuranceData.Add("tracking_code", shipment.TrackingCode);

            Parameters.Insurance.Create insuranceParameters = Fixtures.Parameters.Insurance.Create(insuranceData);

            Insurance insurance = await Client.Insurance.Create(insuranceParameters);

            Assert.IsType<Insurance>(insurance);
            Assert.StartsWith("ins_", insurance.Id);
            Assert.Equal("100.00000", insurance.Amount);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            All parameters = Fixtures.Parameters.Insurance.All(data);

            InsuranceCollection insuranceCollection = await Client.Insurance.All(parameters);

            List<Insurance> insurances = insuranceCollection.Insurances;

            Assert.True(insurances.Count <= Fixtures.PageSize);
            foreach (Insurance item in insurances)
            {
                Assert.IsType<Insurance>(item);
            }
        }

        #endregion

        #endregion
    }
}
