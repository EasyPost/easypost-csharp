using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json.Linq;
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
            Assert.Equal("DhlEcsAccount", carrierAccount.Type);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateWithCustomWorkflow()
        {
            UseVCR("create_with_custom_workflow");

            //  FedEx or UPS should trigger a function error since not supported by legacy parameter method
            try
            {
                Dictionary<string, object> parameters = Fixtures.BasicCarrierAccount;
                parameters["type"] = CarrierAccountType.FedEx.Name;
                parameters["registration_data"] = new Dictionary<string, object>();

                CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
                CleanUpAfterTest(carrierAccount.Id);
            }
            catch (InvalidFunctionError e)
            {
                // Function should have been halted due to incompatible carrier account type
                Assert.NotNull(e);
            }
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateOauth()
        {
            UseVCR("create_with_oauth");

            EasyPost.Parameters.CarrierAccount.CreateOauth parameters = new()
            {
                Type = CarrierAccountType.AmazonShippingAccount.Name,
            };

            CarrierAccount carrierAccount = await Client.CarrierAccount.Create(parameters);
            CleanUpAfterTest(carrierAccount.Id);

            Assert.IsType<CarrierAccount>(carrierAccount);
            Assert.StartsWith("ca_", carrierAccount.Id);
            Assert.Equal("AmazonShippingAccount", carrierAccount.Type);
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
        [Testing.EdgeCase]
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

        /// <summary>
        ///     Test that the CarrierAccount fields are correctly serialized to the API request.
        /// </summary>
        [Fact]
        [Testing.EdgeCase]
        public async Task TestCarrierFieldsJsonSerialization()
        {
            UseMockClient(new List<TestUtils.MockRequest>
                {
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/pickups"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{}")
                    )
                }
            );

            CarrierAccount carrierAccount = new()
            {
                Id = "ca_123",
                Fields = new CarrierFields
                {
                    Credentials = new Dictionary<string, CarrierField>
                    {
                        {
                            "account_number", new CarrierField
                            {
                                Visibility = "visible",
                                Label = "DHL Account Number",
                                Value = "123456"
                            }
                        }
                    }
                }
            };

            EasyPost.Parameters.Pickup.Create parameters = new()
            {
                Shipment = new Shipment(),
                CarrierAccounts = new List<CarrierAccount> { carrierAccount }
            };

            // Confirm that the CarrierAccount fields are serialized correctly
            Dictionary<string, object> json = parameters.ToDictionary();
            Dictionary<string, object> pickupJson = json["pickup"] as Dictionary<string, object>;
            List<object> carrierAccountsJson = pickupJson["carrier_accounts"] as List<object>;
            Dictionary<string, object> carrierAccountJson = carrierAccountsJson[0] as Dictionary<string, object>;
            JObject fieldsJson = carrierAccountJson["fields"] as JObject;
            JObject credentialsJson = fieldsJson["credentials"] as JObject;
            JObject accountNumberJson = credentialsJson["account_number"] as JObject;
            JToken visibility = accountNumberJson["visibility"];
            Assert.Equal("visible", visibility);

            // Test serialization again via an actual API call attempt
            // This will throw an exception if the CarrierAccount fields are not serialized correctly
            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Pickup.Create(parameters));
            Assert.Null(possibleException);
        }

        #endregion
    }
}
