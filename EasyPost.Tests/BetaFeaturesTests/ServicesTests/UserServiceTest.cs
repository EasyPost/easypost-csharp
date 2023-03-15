using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class UserServiceTests : UnitTest
    {
        public UserServiceTests() : base("user_service_with_parameters", TestUtils.ApiKey.Production) =>
            CleanupFunction = async id =>
            {
                try
                {
                    User retrievedUser = await Client.User.Retrieve(id);
                    await retrievedUser.Delete();
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
        public async Task TestCreateChild()
        {
            UseVCR("create_child");

            Dictionary<string, object> data = new Dictionary<string, object> { { "name", "Test User" } };

            BetaFeatures.Parameters.Users.CreateChild parameters = Fixtures.Parameters.Users.CreateChild(data);

            User user = await Client.User.CreateChild(parameters);
            CleanUpAfterTest(user.Id);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal("Test User", user.Name);
        }

        #endregion

        #endregion
    }
}
