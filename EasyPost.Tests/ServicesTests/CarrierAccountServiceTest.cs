using System;
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
                    await Client.CarrierAccount.Delete(id);
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

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            const string testDescription = "my custom description";

            Dictionary<string, object> carrierAccountData = new() { { "description", testDescription } };
            carrierAccount = await Client.CarrierAccount.Update(carrierAccount.Id, carrierAccountData);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
            Assert.Equal(testDescription, carrierAccount.Description);
        }

        [Fact]
        [CrudOperations.Delete]
        [Testing.Function]
        public async Task TestDelete()
        {
            UseVCR("delete");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.CarrierAccount.Delete(carrierAccount.Id));

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
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
