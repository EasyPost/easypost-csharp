using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
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

        private static async Task<CarrierAccount> CreateBasicCarrierAccount(V2Client v2Client)
        {
            return await v2Client.CarrierAccounts.Create(Fixture.BasicCarrierAccount);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(v2Client);

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(v2Client);

            CarrierAccount retrievedCarrierAccount = await v2Client.CarrierAccounts.Retrieve(carrierAccount.id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount, retrievedCarrierAccount);
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client v2Client = _vcr.SetUpTest("all");

            List<CarrierAccount> carrierAccounts = await v2Client.CarrierAccounts.All();

            foreach (var item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            V2Client v2Client = _vcr.SetUpTest("update");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(v2Client);

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
            V2Client v2Client = _vcr.SetUpTest("delete");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount(v2Client);

            bool success = await carrierAccount.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task TestTypes()
        {
            V2Client v2Client = _vcr.SetUpTest("types");

            List<CarrierType> types = await v2Client.CarrierTypes.All();

            foreach (var item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }
    }
}
