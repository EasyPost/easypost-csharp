using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CarrierAccountTest
    {
        private static string _carrierAccountId = null;

        private TestUtils.VCR _vcr;

        [TestCleanup]
        public async Task Cleanup()
        {
            if (_carrierAccountId != null)
            {
                try
                {
                    CarrierAccount retrievedCarrierAccount = await CarrierAccount.Retrieve(_carrierAccountId);
                    await retrievedCarrierAccount.Delete();
                    _carrierAccountId = null;
                }
                catch
                {
                    // in case we try to delete something that's already been deleted
                }
            }
        }

        private static async Task<CarrierAccount> CreateBasicCarrierAccount()
        {
            CarrierAccount carrierAccount = await CarrierAccount.Create(Fixture.BasicCarrierAccount);
            _carrierAccountId = carrierAccount.id; // trigger deletion after test
            return carrierAccount;
        }

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("carrier_account", TestUtils.ApiKey.Production);
        }

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            List<CarrierAccount> carrierAccounts = await CarrierAccount.All();

            foreach (var item in carrierAccounts)
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

            _carrierAccountId = null; // skip deletion cleanup
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

            foreach (var item in types)
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
    }
}
