using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
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

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(Fixtures.BasicCarrierAccount);
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
                Dictionary<string, object> parameters = Fixtures.BasicCarrierAccount;
                parameters["type"] = "FedexAccount";
                parameters["registration_data"] = new Dictionary<string, object>();

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

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(Fixtures.BasicCarrierAccount);
            CleanUpAfterTest(carrierAccount.Id);

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

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(Fixtures.BasicCarrierAccount);
            CleanUpAfterTest(carrierAccount.Id);

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

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(Fixtures.BasicCarrierAccount);
            CleanUpAfterTest(carrierAccount.Id);

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.CarrierAccount.Delete(carrierAccount.Id));

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
        }

        #endregion

        /// <summary>
        ///     Test that the CarrierAccount fields are correctly deserialized from the API response.
        ///     None of the demo carrier accounts used in the above tests have credentials or test credentials fields, so we need to use some mock data.
        /// </summary>
        [Fact]
        [Testing.Function]
        public async Task TestCarrierFieldsJsonDeserialization()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/carrier_accounts$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "[{\"id\":\"ca_123\",\"object\":\"CarrierAccount\",\"fields\":{\"credentials\":{\"account_number\":{\"visibility\":\"visible\",\"label\":\"DHL Account Number\",\"value\":\"123456\"},\"country\":{\"visibility\":\"visible\",\"label\":\"Account Country Code (2 Letter)\",\"value\":\"US\"},\"site_id\":{\"visibility\":\"visible\",\"label\":\"Site ID (Optional)\",\"value\": null },\"password\":{\"visibility\":\"password\",\"label\":\"Password (Optional)\",\"value\":\"\"},\"is_reseller\":{\"visibility\":\"checkbox\",\"label\":\"Reseller Account? (check if yes)\",\"value\":null}}}}]")
                )
            });

            List<CarrierAccount> carrierAccounts = await Client.CarrierAccount.All();

            Assert.NotEmpty(carrierAccounts);
            Assert.Single(carrierAccounts);

            CarrierAccount carrierAccount = carrierAccounts[0];
            Assert.NotNull(carrierAccount.Fields);
            Assert.NotNull(carrierAccount.Fields.Credentials);
            Assert.NotNull(carrierAccount.Fields.Credentials["account_number"]);

            CarrierField accountNumberField = carrierAccount.Fields.Credentials["account_number"];
            Assert.Equal("visible", accountNumberField.Visibility);
            Assert.Equal("DHL Account Number", accountNumberField.Label);
            Assert.Equal("123456", accountNumberField.Value);
        }

        #endregion
    }
}
