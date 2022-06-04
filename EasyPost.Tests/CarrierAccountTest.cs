using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.V2;
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
                    CarrierAccount retrievedCarrierAccount = await V2Client.CarrierAccounts.Retrieve(id);
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
            UseVCR("all");

            List<CarrierAccount> carrierAccounts = await V2Client.CarrierAccounts.All();

            foreach (CarrierAccount item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            bool success = await carrierAccount.Delete();

            Assert.IsTrue(success);

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = await V2Client.CarrierAccounts.Retrieve(carrierAccount.id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount, retrievedCarrierAccount);
        }

        [Fact]
        public async Task TestTypes()
        {
            UseVCR("types");

            List<CarrierType> types = await V2Client.CarrierTypes.All();

            foreach (CarrierType item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            string testDescription = "my custom description";

            Dictionary<string, object> carrierAccountData = new Dictionary<string, object>()
            {
                {
                    "description",
                    testDescription
                }
            };
            await carrierAccount.Update(carrierAccountData);

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
            Assert.AreEqual(testDescription, carrierAccount.description);
        }

        private async Task<CarrierAccount> CreateBasicCarrierAccount()
        {
            CarrierAccount carrierAccount = await V2Client.CarrierAccounts.Create(Fixture.BasicCarrierAccount);
            CleanUpAfterTest(carrierAccount.id);

            return carrierAccount;
        }
    }
}
