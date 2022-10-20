using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
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

            Insurance insurance = await CreateBasicInsurance();

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

            InsuranceCollection insuranceCollection = await Client.Insurance.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Insurance> insurances = insuranceCollection.Insurances;

            Assert.True(insuranceCollection.HasMore);
            Assert.True(insurances.Count <= Fixtures.PageSize);
            foreach (Insurance item in insurances)
            {
                Assert.IsType<Insurance>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Insurance insurance = await CreateBasicInsurance();

            Insurance retrievedInsurance = await Client.Insurance.Retrieve(insurance.Id);
            Assert.IsType<Insurance>(retrievedInsurance);
            // Must compare IDs since other elements of object may be different
            Assert.Equal(insurance.Id, retrievedInsurance.Id);
        }

        #endregion

        #endregion

        private async Task<Insurance> CreateBasicInsurance()
        {
            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            Dictionary<string, object> parameters = Fixtures.BasicInsurance;
            parameters.Add("tracking_code", shipment.TrackingCode);

            return await Client.Insurance.Create(parameters);
        }
    }
}
