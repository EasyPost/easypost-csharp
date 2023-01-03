using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class UserTests : UnitTest
    {
        public UserTests() : base("user", TestUtils.ApiKey.Production) =>
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
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand");

            User user = await Client.User.CreateChild(new Dictionary<string, object> { { "name", "Test User" } });
            CleanUpAfterTest(user.Id);

            string color = "#123456";
            Brand brand = await user.UpdateBrand(new Dictionary<string, object> { { "color", color } });

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

            User user = await Client.User.CreateChild(new Dictionary<string, object> { { "name", "Test User" } });
            CleanUpAfterTest(user.Id);

            const string testName = "New Name";

            Dictionary<string, object> userDict = new() { { "name", testName } };
            user = await user.Update(userDict);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal(testName, user.Name);
        }

        [Fact]
        [CrudOperations.Delete]
        [Testing.Function]
        public async Task TestDelete()
        {
            UseVCR("delete");

            User user = await Client.User.CreateChild(new Dictionary<string, object> { { "name", "Test User" } });
            CleanUpAfterTest(user.Id);

            Exception? possibleException = await Record.ExceptionAsync(async () => await user.Delete());

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
        }

        #endregion

        #endregion
    }
}
