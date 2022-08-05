using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
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
            UseVCR("all");

            InsuranceCollection insuranceCollection = await Client.Insurance.All(new Dictionary<string, object?>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Insurance> insurances = insuranceCollection.insurances;

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
            UseVCR("create");

            Insurance insurance = await CreateBasicInsurance();

            Assert.IsInstanceOfType(insurance, typeof(Insurance));
            Assert.IsTrue(insurance.id.StartsWith("ins_"));
            // TODO: amount really should be a number, not a string
            Assert.AreEqual("100.00000", insurance.amount);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Insurance insurance = await CreateBasicInsurance();

            Insurance retrievedInsurance = await Client.Insurance.Retrieve(insurance.id);
            Assert.IsInstanceOfType(retrievedInsurance, typeof(Insurance));
            // Must compare IDs since other elements of object may be different
            Assert.AreEqual(insurance.id, retrievedInsurance.id);
        }

        private async Task<Insurance> CreateBasicInsurance()
        {
            Dictionary<string, object?> basicInsurance = await Fixture.BasicInsurance(Client);
            return await Client.Insurance.Create(basicInsurance);
        }
    }
}
