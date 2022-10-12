using EasyPost.Tests._Utilities;

namespace EasyPost.Tests.ModelsTests
{
    public class ApiKeyTests : UnitTest
    {
        public ApiKeyTests() : base("api_key", TestUtils.ApiKey.Production)
        {
        }
    }
}
