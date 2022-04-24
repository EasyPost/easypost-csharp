using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CarrierAccountTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("carrier_account", TestUtils.ApiKey.Production);
        }

        private static async Task<CarrierAccount> CreateBasicCarrierAccount(Client client)
        {
            return await client.CarrierAccounts.Create(Fixture.BasicCarrierAccount);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            Client client = _vcr.SetUpTest("create");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(client);

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            Client client = _vcr.SetUpTest("retrieve");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(client);

            CarrierAccount retrievedCarrierAccount = await client.CarrierAccounts.Retrieve(carrierAccount.id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount, retrievedCarrierAccount);
        }

        [TestMethod]
        public async Task TestAll()
        {
            Client client = _vcr.SetUpTest("all");

            List<CarrierAccount> carrierAccounts = await client.CarrierAccounts.All();

            foreach (var item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            Client client = _vcr.SetUpTest("update");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(client);

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

        [TestMethod]
        public async Task TestDelete()
        {
            Client client = _vcr.SetUpTest("delete");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(client);

            bool success = await carrierAccount.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task TestTypes()
        {
            Client client = _vcr.SetUpTest("types");

            List<CarrierType> types = await client.CarrierTypes.All();

            foreach (var item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }
    }
}
