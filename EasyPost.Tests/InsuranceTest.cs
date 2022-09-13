using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class InsuranceTest : UnitTest
    {
        public InsuranceTest() : base("insurance")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Insurance insurance = await CreateBasicInsurance();

            Assert.IsType<Insurance>(insurance);
            Assert.StartsWith("ins_", insurance.Id);
            // TODO: amount really should be a number, not a string
            Assert.Equal("100.00000", insurance.Amount);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            InsuranceCollection insuranceCollection = await Client.Insurance.All(new Dictionary<string, object> { { "page_size", Fixture.PageSize } });

            List<Insurance> insurances = insuranceCollection.Insurances;

            Assert.True(insuranceCollection.HasMore);
            Assert.True(insurances.Count <= Fixture.PageSize);
            foreach (Insurance item in insurances)
            {
                Assert.IsType<Insurance>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
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

        private async Task<Insurance> CreateBasicInsurance()
        {
            Dictionary<string, object> basicInsurance = await Fixture.BasicInsurance(Client);
            return await Client.Insurance.Create(basicInsurance);
        }
    }
}
