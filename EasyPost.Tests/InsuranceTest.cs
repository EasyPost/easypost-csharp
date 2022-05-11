using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class InsuranceTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("insurance");

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            InsuranceCollection insuranceCollection = await Insurance.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Insurance> insurances = insuranceCollection.insurances;

            Assert.IsTrue(insurances.Count <= Fixture.PageSize);
            Assert.IsNotNull(insuranceCollection.has_more);
            foreach (Insurance item in insurances)
            {
                Assert.IsInstanceOfType(item, typeof(Insurance));
            }
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Insurance insurance = await CreateBasicInsurance();

            Assert.IsInstanceOfType(insurance, typeof(Insurance));
            Assert.IsTrue(insurance.id.StartsWith("ins_"));
            // TODO: amount really should be a number, not a string
            Assert.AreEqual("100.00000", insurance.amount);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            Insurance insurance = await CreateBasicInsurance();

            Insurance retrievedInsurance = await Insurance.Retrieve(insurance.id);
            Assert.IsInstanceOfType(retrievedInsurance, typeof(Insurance));
            // Must compare IDs since other elements of object may be different
            Assert.AreEqual(insurance.id, retrievedInsurance.id);
        }

        private static async Task<Insurance> CreateBasicInsurance() => await Insurance.Create(await Fixture.BasicInsurance());
    }
}
