using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
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

            Parameters.CarrierAccount.Create parameters = Fixtures.Parameters.CarrierAccounts.Create(data);

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
            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Parameters.CarrierAccount.CreateFedEx parameters = Fixtures.Parameters.CarrierAccounts.CreateFedEx(data);

            try
            {
                // confirms we can pass in CreateFedEx and CreateUps parameters to the same Create method because they are children of the generic Create class
                CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
                CleanUpAfterTest(carrierAccount.Id);
            }

            catch (BadRequestError e)
            {
                // the data we're sending is invalid, we want to check that the API error is because of malformed data and not due to the endpoint
                Assert.Equal(400, e.StatusCode); // 400 is fine. We don't want a 404 not found
                Assert.NotNull(e.Errors);
                Assert.Contains(e.Errors, error => error is { Message: "Invalid Customer Account Nbr" });

                // Check the cassette to make sure the endpoint is correct (it should be carrier_accounts/register)
                // Check the cassette to make sure the "registration_data" key is populated in the request body
            }
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Exception]
        public async Task TestPreventUsersUsingGenericParameterSetWithCustomWorkflow()
        {
            UseVCR("prevent_users_using_generic_parameter_set_with_custom_workflow");

            // Generic Create parameter set configured for DHL
            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            // Override the type to be a custom type
            data["type"] = CarrierAccountType.FedEx.Name;
            data["registration_data"] = new Dictionary<string, object>();

            Parameters.CarrierAccount.Create parameters = Fixtures.Parameters.CarrierAccounts.Create(data);

            // should raise an exception because we're using a generic Create set with a custom workflow type (FedExAccount)
            await Assert.ThrowsAsync<InvalidParameterError>(async () => await Client.CarrierAccount.Create(parameters));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Parameters.CarrierAccount.Create createParameters = Fixtures.Parameters.CarrierAccounts.Create(data);

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(createParameters);
            CleanUpAfterTest(carrierAccount.Id);

            const string testDescription = "my custom description";

            Parameters.CarrierAccount.Update updateParameters = new()
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
