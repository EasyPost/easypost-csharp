using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class ApiKeyTest : UnitTest
    {
        public ApiKeyTest() : base("api_key", TestUtils.ApiKey.Production)
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Read]
        public async Task TestAllApiKeys()
        {
            UseVCR("all_api_keys");

            ApiKeyCollection collection = await Client.ApiKey.All();

            // API keys will be censored, so we'll just check for the existence of the list
            Assert.NotNull(collection);
            Assert.NotNull(collection.keys);
            Assert.NotNull(collection.children);
        }

        #endregion
    }
}
