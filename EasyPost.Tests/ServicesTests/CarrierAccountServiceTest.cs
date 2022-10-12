using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class CarrierAccountServiceTests : UnitTest
    {
        public CarrierAccountServiceTests() : base("carrier_account_service", TestUtils.ApiKey.Production) =>
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

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
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
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = await Client.CarrierAccount.Retrieve(carrierAccount.Id);

            Assert.IsType<CarrierAccount>(retrievedCarrierAccount);
            Assert.Equal(carrierAccount, retrievedCarrierAccount);
        }

        #endregion

        #endregion

        private async Task<CarrierAccount> CreateBasicCarrierAccount()
        {
            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(Fixtures.BasicCarrierAccount);
            CleanUpAfterTest(carrierAccount.Id);

            return carrierAccount;
        }
    }
}
