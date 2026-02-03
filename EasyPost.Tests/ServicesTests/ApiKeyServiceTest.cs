using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class ApiKeyServiceTests : UnitTest
    {
        public ApiKeyServiceTests() : base("api_key_service", TestUtils.ApiKey.Production)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieveApiKeys()
        {
            UseVCR("retrieve_api_keys");

            User user = await Client.User.RetrieveMe();

            List<ApiKey> apiKeys = await Client.ApiKey.RetrieveApiKeysForUser(user.Id);

            Assert.IsType<List<ApiKey>>(apiKeys);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieveApiKeysChild()
        {
            UseVCR("retrieve_api_keys_child");

            const string fakeChildId = "user_123456789";

            // Test suite user has no child users, so this should throw a FilteringError
            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.ApiKey.RetrieveApiKeysForUser(fakeChildId));

            Assert.IsType<FilteringError>(possibleException);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            ApiKeyCollection collection = await Client.ApiKey.All();

            // API keys will be censored, so we'll just check for the existence of the list
            Assert.NotNull(collection);
            Assert.NotNull(collection.Keys);
            Assert.NotNull(collection.Children);

            // And for each key in the collection, we'll check for the existence of the key
            foreach (ApiKey key in collection.Keys)
            {
                Assert.NotNull(key);
                Assert.NotNull(key.Key);
            }
        }

        [Fact]
        [CrudOperations.Create]
        [CrudOperations.Update]
        [CrudOperations.Delete]
        public async Task TestApiKeyLifecycle()
        {
            string? referralApiKey = Environment.GetEnvironmentVariable("REFERRAL_CUSTOMER_PROD_API_KEY");
            UseVCR("lifecycle", referralApiKey);

            // Create an API key
            ApiKey apiKey = await Client.ApiKey.Create("production");
            Assert.NotNull(apiKey);
            Assert.StartsWith("ak_", apiKey.Id);
            Assert.Equal("production", apiKey.Mode);

            // Disable the API key
            apiKey = await Client.ApiKey.Disable(apiKey.Id);
            Assert.False(apiKey.Active);

            // Enable the API key
            apiKey = await Client.ApiKey.Enable(apiKey.Id);
            Assert.True(apiKey.Active);

            // Delete the API key
            await Client.ApiKey.Delete(apiKey.Id);
        }

        #endregion

        #endregion
    }
}
