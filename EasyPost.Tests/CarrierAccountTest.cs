using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using EasyPost.Parameters.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class CarrierAccountTest : UnitTest
    {
        public CarrierAccountTest() : base("carrier_account", TestUtils.ApiKey.Production) =>
            CleanupFunction = async id =>
            {
                try
                {
                    CarrierAccount retrievedCarrierAccount = await Client.CarrierAccounts.Retrieve(id);
                    return await retrievedCarrierAccount.Delete();
                }
                catch
                {
                    // trying to delete something that doesn't exist, pass
                    return false;
                }
            };

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            List<CarrierAccount> carrierAccounts = await Client.CarrierAccounts.All();

            foreach (CarrierAccount item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.Id.StartsWith("ca_"));
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete", ApiVersion.Latest);

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            bool success = await carrierAccount.Delete();

            Assert.IsTrue(success);

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = await Client.CarrierAccounts.Retrieve(carrierAccount.Id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount, retrievedCarrierAccount);
        }

        [Fact]
        public async Task TestTypes()
        {
            UseVCR("types", ApiVersion.Latest);

            List<CarrierType> types = await Client.CarrierTypes.All();

            foreach (CarrierType item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update", ApiVersion.Latest);


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            string testDescription = "my custom description";

            Dictionary<string, object> carrierAccountData = new Dictionary<string, object>()
            {
                {
                    "description", testDescription
                }
            };
            carrierAccount = await carrierAccount.Update(new CarrierAccounts.Update(carrierAccountData));

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.Id.StartsWith("ca_"));
            Assert.AreEqual(testDescription, carrierAccount.Description);
        }

        private async Task<CarrierAccount> CreateBasicCarrierAccount()
        {
            CarrierAccount carrierAccount = await Client.CarrierAccounts.Create(new CarrierAccounts.Create(Fixture.BasicCarrierAccount));
            CleanUpAfterTest(carrierAccount.Id);

            return carrierAccount;
        }
    }
}
