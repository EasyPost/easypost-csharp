using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
#pragma warning disable xUnit1004
    public class UserTests : UnitTest
    {
        public UserTests() : base("user_with_parameters", TestUtils.ApiKey.Production) =>
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

        [Fact(Skip = "TO BE REMOVED.")]
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

            Brand brand = await user.UpdateBrand(brandParameters);

            Assert.IsType<Brand>(brand);
            Assert.StartsWith("brd_", brand.Id);
            Assert.Equal(color, brand.Color);
        }

        [Fact(Skip = "TO BE REMOVED.")]
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

            user = await user.Update(userUpdateParameters);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal(testName, user.Name);
        }

        #endregion

        #endregion
    }
}
