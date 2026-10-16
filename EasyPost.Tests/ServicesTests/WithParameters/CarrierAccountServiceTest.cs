using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json.Linq;
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
        public async Task TestCreateUrlRouting(Parameters.CarrierAccount.ACreate parameters, string cassetteName, string expectedEndpoint, string expectedBodyKey)
        {
            UseVCR(cassetteName);

            try
            {
                CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
                CleanUpAfterTest(carrierAccount.Id);
            }
            catch (ApiError error)
            {
                Assert.NotEqual(404, error.StatusCode);
                Assert.NotEqual(500, error.StatusCode);
            }

            AssertCassetteRequest(cassetteName, expectedEndpoint, expectedBodyKey);
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
            yield return new object[] { Fixtures.Parameters.CarrierAccounts.CreateFedEx(new Dictionary<string, object>()), "create_url_routing_fedex", "/carrier_accounts/register", "carrier_account" };
            yield return new object[] { CreateFedExSmartPostParameters(), "create_url_routing_fedex_smartpost", "/carrier_accounts/register", "carrier_account" };
            yield return new object[] { new Parameters.CarrierAccount.CreateFedExDefault(), "create_url_routing_fedex_default", "/carrier_accounts/open", "carrier_account" };
            yield return new object[] { new Parameters.CarrierAccount.CreateGofoWallet(), "create_url_routing_gofo_wallet", "/carrier_accounts/open", "carrier_account" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUpsDap(), "create_url_routing_ups_dap", "/carrier_accounts/open", "carrier_account" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUniUniWallet(), "create_url_routing_uniuni_wallet", "/carrier_accounts/open", "carrier_account" };
            yield return new object[] { new Parameters.CarrierAccount.CreateVehoWallet(), "create_url_routing_veho_wallet", "/carrier_accounts/open", "carrier_account" };
            yield return new object[] { new Parameters.CarrierAccount.CreateOauth { Type = CarrierAccountType.AmazonShippingAccount.Name }, "create_url_routing_amazon_shipping", "/carrier_accounts/register_oauth", "carrier_account_oauth_registrations" };
            yield return new object[] { Fixtures.Parameters.CarrierAccounts.CreateUps(new Dictionary<string, object>()), "create_url_routing_ups", "/carrier_accounts/register_oauth", "carrier_account_oauth_registrations" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUpsMailInnovations { AccountNumber = "123456789" }, "create_url_routing_ups_mail_innovations", "/carrier_accounts/register_oauth", "carrier_account_oauth_registrations" };
            yield return new object[] { new Parameters.CarrierAccount.CreateUpsSurePost { AccountNumber = "123456789" }, "create_url_routing_ups_surepost", "/carrier_accounts/register_oauth", "carrier_account_oauth_registrations" };
            yield return new object[] { new Parameters.CarrierAccount.CreateOauth { Type = CarrierAccountType.UspsShipAccount.Name }, "create_url_routing_usps_ship", "/carrier_accounts/register_oauth", "carrier_account_oauth_registrations" };
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

        private static void AssertCassetteRequest(string cassetteName, string expectedEndpoint, string expectedBodyKey)
        {
            string cassettePath = Path.GetFullPath(Path.Combine(TestUtils.GetSourceFileDirectory(), "..", "..", "cassettes", "carrier_account_service_with_parameters", $"{cassetteName}.json"));
            string cassetteJson = File.ReadAllText(cassettePath);
            JArray interactions = JArray.Parse(cassetteJson);
            JToken? interaction = interactions.FirstOrDefault();

            Assert.NotNull(interaction);

            string? requestUri = interaction!["Request"]?["Uri"]?.Value<string>();
            string? requestBody = interaction["Request"]?["Body"]?.Value<string>();

            Assert.EndsWith(expectedEndpoint, requestUri);
            Assert.Contains(expectedBodyKey, requestBody);
        }

        #endregion
    }
}
