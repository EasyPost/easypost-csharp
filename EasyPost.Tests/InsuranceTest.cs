using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Insurance = EasyPost.Parameters.Insurance;

namespace EasyPost.Tests
{
    public class InsuranceTest : UnitTest
    {
        public InsuranceTest() : base("insurance")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            InsuranceCollection insuranceCollection = await Client.Insurance.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<Models.API.Insurance> insurances = insuranceCollection.Insurances;

            Assert.IsTrue(insurances.Count <= Fixture.PageSize);
            Assert.IsNotNull(insuranceCollection.HasMore);
            foreach (Models.API.Insurance item in insurances)
            {
                Assert.IsInstanceOfType(item, typeof(Insurance));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Models.API.Insurance insurance = await CreateBasicInsurance();

            Assert.IsInstanceOfType(insurance, typeof(Insurance));
            Assert.IsTrue(insurance.Id.StartsWith("ins_"));
            // TODO: amount really should be a number, not a string
            Assert.AreEqual("100.00000", insurance.Amount);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Models.API.Insurance insurance = await CreateBasicInsurance();

            Models.API.Insurance retrievedInsurance = await Client.Insurance.Retrieve(insurance.Id);
            Assert.IsInstanceOfType(retrievedInsurance, typeof(Insurance));
            // Must compare IDs since other elements of object may be different
            Assert.AreEqual(insurance.Id, retrievedInsurance.Id);
        }

        private async Task<Models.API.Insurance> CreateBasicInsurance()
        {
            Dictionary<string, object> basicInsurance = await Fixture.BasicInsurance(Client);
            return await Client.Insurance.Create(new Insurance.Create(basicInsurance));
        }
    }
}
