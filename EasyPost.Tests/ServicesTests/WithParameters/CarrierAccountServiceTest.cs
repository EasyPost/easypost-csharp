using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Models.API;
using EasyPost.Parameters.CarrierAccount;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class CarrierAccountServiceTests : UnitTest
    {
        public CarrierAccountServiceTests() : base("carrier_account_service_with_parameters", TestUtils.ApiKey.Production) =>
            CleanupFunction = async id =>
            {
                try
                {
                    CarrierAccount retrievedCarrierAccount = await Client.CarrierAccount.Retrieve(id);
                    await Client.CarrierAccount.Delete(retrievedCarrierAccount.Id);
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

            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Create parameters = Fixtures.Parameters.CarrierAccounts.Create(data);

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
            CleanUpAfterTest(carrierAccount.Id);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateWithCustomWorkflow()
        {
            UseVCR("create_with_custom_workflow");

            // Carriers like FedEx and UPS should hit the `/carrier_accounts/register` endpoint
            try
            {
                Dictionary<string, object> data = Fixtures.BasicCarrierAccount;
                data["type"] = "FedexAccount";
                data["registration_data"] = new Dictionary<string, object>();

                Create parameters = Fixtures.Parameters.CarrierAccounts.Create(data);

                CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
                CleanUpAfterTest(carrierAccount.Id);
            }
            catch (InvalidRequestError e)
            {
                // the data we're sending is invalid, we want to check that the API error is because of malformed data and not due to the endpoint
                Assert.Equal(422, e.StatusCode);  // 422 is fine. We don't want a 404 not found
                Assert.NotNull(e.Errors);
                Assert.Contains(e.Errors, error => error is { Field: "account_number", Message: "must be present and a string" });

                // Check the cassette to make sure the endpoint is correct (it should be carrier_accounts/register)
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Create createParameters = Fixtures.Parameters.CarrierAccounts.Create(data);

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(createParameters);
            CleanUpAfterTest(carrierAccount.Id);

            const string testDescription = "my custom description";

            Update updateParameters = new Update
            {
                Description = testDescription,
            };

            carrierAccount = await Client.CarrierAccount.Update(carrierAccount.Id, updateParameters);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
            Assert.Equal(testDescription, carrierAccount.Description);
        }

        #endregion

        #endregion
    }
}
