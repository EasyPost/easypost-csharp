using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class CarrierAccountTest : UnitTest
    {
        public CarrierAccountTest() : base("carrier_account", TestUtils.ApiKey.Production) =>
            CleanupFunction = async id =>
            {
                try
                {
                    CarrierAccount retrievedCarrierAccount = await Client.CarrierAccount.Retrieve(id);
                    await retrievedCarrierAccount.Delete();
                    return true;
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

            List<CarrierAccount> carrierAccounts = await Client.CarrierAccount.All();

            foreach (CarrierAccount item in carrierAccounts)
            {
                Assert.IsType<CarrierAccount>(item);
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.id);
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            await carrierAccount.Delete();

            // TODO: Assert something

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = await Client.CarrierAccount.Retrieve(carrierAccount.id);

            Assert.IsType<CarrierAccount>(retrievedCarrierAccount);
            Assert.Equal(carrierAccount, retrievedCarrierAccount);
        }

        [Fact]
        public async Task TestTypes()
        {
            UseVCR("types");

            List<CarrierType> types = await Client.CarrierType.All();

            foreach (CarrierType item in types)
            {
                Assert.IsType<CarrierType>(item);
            }
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update");


            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            string testDescription = "my custom description";

            Dictionary<string, object?> carrierAccountData = new Dictionary<string, object?>()
            {
                {
                    "description",
                    testDescription
                }
            };
            carrierAccount = await carrierAccount.Update(carrierAccountData);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.id);
            Assert.Equal(testDescription, carrierAccount.description);
        }

        private async Task<CarrierAccount> CreateBasicCarrierAccount()
        {
            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(Fixture.BasicCarrierAccount);
            CleanUpAfterTest(carrierAccount.id);

            return carrierAccount;
        }
    }
}
