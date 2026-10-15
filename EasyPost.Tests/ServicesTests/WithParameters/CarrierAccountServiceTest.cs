using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
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
        public async Task TestCreateFedEx()
        {
            UseVCR("create_fedex");

            // FedEx should hit the `/carrier_accounts/register` endpoint
            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Parameters.CarrierAccount.CreateFedEx parameters = Fixtures.Parameters.CarrierAccounts.CreateFedEx(data);

            try
            {
                // confirms we can pass in CreateFedEx parameters to the same Create method because they are children of the generic Create class
                CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
                CleanUpAfterTest(carrierAccount.Id);
            }

            catch (BadRequestError e)
            {
                // The data we're sending is invalid; we want to check that the API error is because of malformed data and not due to the endpoint
                Assert.Equal(400, e.StatusCode); // 400 is fine. We don't want a 404 not found
                Assert.NotNull(e.Errors);

                // Coerce the error into a FieldError and check its properties
                Assert.Contains(e.Errors, error =>
                {
                    if (error is FieldError fieldError)
                    {
                        return fieldError.Message == "Invalid Customer Account Nbr";
                    }
                    return false;
                });

                // Check the cassette to make sure the endpoint is correct (it should be carrier_accounts/register)
                // Check the cassette to make sure the "registration_data" key is populated in the request body
            }
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateUps()
        {
            UseVCR("create_ups");

            // UPS should hit the `/carrier_account_oauth_registrations` endpoint
            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Parameters.CarrierAccount.CreateUps parameters = Fixtures.Parameters.CarrierAccounts.CreateUps(data);

            // confirms we can pass in CreateUps parameters to the same Create method because they are children of the generic Create class
            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
            CleanUpAfterTest(carrierAccount.Id);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
        }

        [Theory]
        [CrudOperations.Create]
        [Testing.Parameters]
        [MemberData(nameof(CreateUrlRoutingData))]
        public async Task TestCreateUrlRouting(Parameters.CarrierAccount.ACreate parameters, string expectedEndpoint)
        {
            string endpointRegex = $@"v2\/{expectedEndpoint.Replace("/", @"\/")}$";
            string responseType = parameters.Type ?? string.Empty;

            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, endpointRegex),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.Created, content: $"{{\"id\":\"ca_123\",\"object\":\"CarrierAccount\",\"type\":\"{responseType}\"}}")
                )
            });

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
            Assert.Equal(responseType, carrierAccount.Type);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Exception]
        public async Task TestPreventUsersUsingGenericParameterSetWithCustomCreateWorkflow()
        {
            UseVCR("prevent_users_using_generic_parameter_set_with_custom_create_workflow");

            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            // Generic Create parameter set configured for DHL
            Parameters.CarrierAccount.Create standardParameters = Fixtures.Parameters.CarrierAccounts.Create(data);

            // Override the type to be a custom type
            standardParameters.Type = CarrierAccountType.FedEx.Name;

            // should raise an exception because we're using a generic Create set with a custom workflow type (FedExAccount)
            await Assert.ThrowsAsync<InvalidParameterError>(async () => await Client.CarrierAccount.Create(standardParameters));

            // Specialized CreateFedEx parameter set configured for FedEx
            Parameters.CarrierAccount.CreateFedEx fedExParameters = Fixtures.Parameters.CarrierAccounts.CreateFedEx(data);

            // Override the type to be a standard type
            fedExParameters.Type = "DhlExpressAccount";

            // should raise an exception because we're using a FedEx-specific Create set with a standard workflow type (DhlExpressAccount)
            await Assert.ThrowsAsync<InvalidParameterError>(async () => await Client.CarrierAccount.Create(fedExParameters));
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Parameters.CarrierAccount.Create createParameters = Fixtures.Parameters.CarrierAccounts.Create(data);

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(createParameters); // DHL Express
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

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestUpdateUps()
        {
            UseVCR("update_ups");

            Dictionary<string, object> data = Fixtures.BasicCarrierAccount;

            Parameters.CarrierAccount.CreateUps createParameters = Fixtures.Parameters.CarrierAccounts.CreateUps(data);

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(createParameters);
            CleanUpAfterTest(carrierAccount.Id);

            const string testDescription = "my custom description";

            Parameters.CarrierAccount.UpdateUps updateParameters = new()
            {
                Description = testDescription,
            };

            carrierAccount = await Client.CarrierAccount.Update(carrierAccount.Id, updateParameters);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
            // Assert.Equal(testDescription, carrierAccount.Description); // TODO: Uncomment when the UPS update endpoint is fixed
        }

        #endregion

        public static IEnumerable<object[]> CreateUrlRoutingData()
        {
            yield return new object[] { Fixtures.Parameters.CarrierAccounts.CreateFedEx(new Dictionary<string, object>()), "carrier_accounts/register" };
            yield return new object[] { CreateFedExSmartPostParameters(), "carrier_accounts/register" };
            yield return new object[] { new Parameters.CarrierAccount.CreateFedExDefault(), "carrier_accounts/open" };
            yield return new object[] { new Parameters.CarrierAccount.CreateGofoWallet(), "carrier_accounts/open" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUpsDap(), "carrier_accounts/open" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUniUniWallet(), "carrier_accounts/open" };
            yield return new object[] { new Parameters.CarrierAccount.CreateVehoWallet(), "carrier_accounts/open" };
            yield return new object[] { new Parameters.CarrierAccount.CreateOauth { Type = CarrierAccountType.AmazonShippingAccount.Name }, "carrier_accounts/register_oauth" };
            yield return new object[] { Fixtures.Parameters.CarrierAccounts.CreateUps(new Dictionary<string, object>()), "carrier_accounts/register_oauth" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUpsMailInnovations { AccountNumber = "123456789" }, "carrier_accounts/register_oauth" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUpsSurePost { AccountNumber = "123456789" }, "carrier_accounts/register_oauth" };
            yield return new object[] { new Parameters.CarrierAccount.CreateOauth { Type = CarrierAccountType.UspsShipAccount.Name }, "carrier_accounts/register_oauth" };
        }

        private static Parameters.CarrierAccount.CreateFedExSmartPost CreateFedExSmartPostParameters()
        {
            return new Parameters.CarrierAccount.CreateFedExSmartPost
            {
                AccountNumber = "123456789",
                HubId = "1234",
                CorporateAddressCity = "San Francisco",
                CorporateAddressCountryCode = "US",
                CorporateAddressPostalCode = "94105",
                CorporateAddressState = "CA",
                CorporateAddressStreet = "345 California St",
                CorporateCompanyName = "EasyPost",
                CorporateEmailAddress = "me@example.com",
                CorporateFirstName = "Demo",
                CorporateLastName = "User",
                CorporateJobTitle = "Developer",
                CorporatePhoneNumber = "5555555555",
                ShippingAddressCity = "San Francisco",
                ShippingAddressCountryCode = "US",
                ShippingAddressPostalCode = "94105",
                ShippingAddressState = "CA",
                ShippingAddressStreet = "345 California St",
            };
        }

        #endregion
    }
}
