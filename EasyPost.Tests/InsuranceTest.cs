using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using EasyPost.Parameters;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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

            List<Insurance> insurances = insuranceCollection.Insurances;

            Assert.IsTrue(insurances.Count <= Fixture.PageSize);
            Assert.IsNotNull(insuranceCollection.HasMore);
            foreach (Insurance item in insurances)
            {
                Assert.IsInstanceOfType(item, typeof(Insurance));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Insurance insurance = await CreateBasicInsurance();

            Assert.IsInstanceOfType(insurance, typeof(Insurance));
            Assert.IsTrue(insurance.Id.StartsWith("ins_"));
            // TODO: amount really should be a number, not a string
            Assert.AreEqual("100.00000", insurance.Amount);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Insurance insurance = await CreateBasicInsurance();

            Insurance retrievedInsurance = await Client.Insurance.Retrieve(insurance.Id);
            Assert.IsInstanceOfType(retrievedInsurance, typeof(Insurance));
            // Must compare IDs since other elements of object may be different
            Assert.AreEqual(insurance.Id, retrievedInsurance.Id);
        }

        private async Task<Insurance> CreateBasicInsurance()
        {
            Dictionary<string, object> basicInsurance = await Fixture.BasicInsurance(Client);
            return await Client.Insurance.Create(new Parameters.V2.Insurance.Create(basicInsurance));
        }
    }
}
