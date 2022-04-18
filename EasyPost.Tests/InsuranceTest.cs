using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class InsuranceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "insurance", true);
        }

        private static async Task<Insurance> CreateBasicInsurance()
        {
            return await Insurance.Create(await Fixture.BasicInsurance());
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Insurance insurance = await CreateBasicInsurance();

            Assert.IsInstanceOfType(insurance, typeof(Insurance));
            Assert.IsTrue(insurance.id.StartsWith("ins_"));
            // TODO: amount really should be a number, not a string
            Assert.AreEqual("100.00000", insurance.amount);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            Insurance insurance = await CreateBasicInsurance();

            Insurance retrievedInsurance = await Insurance.Retrieve(insurance.id);
            Assert.IsInstanceOfType(retrievedInsurance, typeof(Insurance));
            // Must compare IDs since other elements of object may be different
            Assert.AreEqual(insurance.id, retrievedInsurance.id);
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            InsuranceCollection insuranceCollection = await Insurance.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Insurance> insurances = insuranceCollection.insurances;

            Assert.IsTrue(insurances.Count <= Fixture.PageSize);
            Assert.IsNotNull(insuranceCollection.has_more);
            foreach (var item in insurances)
            {
                Assert.IsInstanceOfType(item, typeof(Insurance));
            }
        }
    }
}
