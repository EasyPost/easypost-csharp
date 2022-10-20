using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class CarrierAccountTests : UnitTest
    {
        public CarrierAccountTests() : base("carrier_account", TestUtils.ApiKey.Production) =>
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
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            CarrierAccount carrierAccount = await CreateBasicCarrierAccount();

            const string testDescription = "my custom description";

            Dictionary<string, object> carrierAccountData = new() { { "description", testDescription } };
            carrierAccount = await carrierAccount.Update(carrierAccountData);

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

            Exception? possibleException = await Record.ExceptionAsync(async () => await carrierAccount.Delete());

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
