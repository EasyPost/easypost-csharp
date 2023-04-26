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
                    await Client.User.Delete(retrievedUser.Id);
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
        
        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand");

            BetaFeatures.Parameters.Users.CreateChild userParameters = new()
            {
                Name = "Test User",
            };
            User user = await Client.User.CreateChild(userParameters);
            CleanUpAfterTest(user.Id);

            const string color = "#123456";
            BetaFeatures.Parameters.Users.UpdateBrand brandParameters = new()
            {
                ColorHexCode = color,
            };

            Brand brand = await Client.User.UpdateBrand(user.Id, brandParameters);

            Assert.IsType<Brand>(brand);
            Assert.StartsWith("brd_", brand.Id);
            Assert.Equal(color, brand.Color);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            BetaFeatures.Parameters.Users.CreateChild userParameters = new()
            {
                Name = "Test User",
            };
            User user = await Client.User.CreateChild(userParameters);
            CleanUpAfterTest(user.Id);

            const string testName = "New Name";
            BetaFeatures.Parameters.Users.Update userUpdateParameters = new()
            {
                Name = testName,
            };

            user = await Client.User.Update(user.Id, userUpdateParameters);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal(testName, user.Name);
        }

        #endregion

        #endregion
    }
}
