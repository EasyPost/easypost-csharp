using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class InsuranceServiceTests : UnitTest
    {
        public InsuranceServiceTests() : base("insurance_service")
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

            Dictionary<string, object> parameters = Fixtures.BasicInsurance;
            parameters.Add("tracking_code", shipment.TrackingCode);

            Insurance insurance = await Client.Insurance.Create(parameters);

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

            BetaFeatures.Parameters.Insurance.All parameters = Fixtures.Parameters.Insurance.All(data);

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
