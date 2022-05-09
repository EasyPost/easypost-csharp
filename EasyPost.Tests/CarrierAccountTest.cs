using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CarrierAccountTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("carrier_account", TestUtils.ApiKey.Production);

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            List<CarrierAccount> carrierAccounts = await CarrierAccount.All();

            foreach (CarrierAccount item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
        }

        [TestMethod]
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            bool success = await carrierAccount.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = await CarrierAccount.Retrieve(carrierAccount.id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount, retrievedCarrierAccount);
        }

        [TestMethod]
        public async Task TestTypes()
        {
            _vcr.SetUpTest("types");

            List<CarrierType> types = await CarrierType.All();

            foreach (CarrierType item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            string testDescription = "my custom description";

            Dictionary<string, object> carrierAccountData = new Dictionary<string, object>
            {
                {
                    "description", testDescription
                }
            };
            await carrierAccount.Update(carrierAccountData);

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
            Assert.AreEqual(testDescription, carrierAccount.description);
        }

        private static async Task<CarrierAccount> CreateBasicCarrierAccount() => await CarrierAccount.Create(Fixture.BasicCarrierAccount);
    }
}
