using System.Threading.Tasks;
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

        #endregion

        #endregion
    }
}
