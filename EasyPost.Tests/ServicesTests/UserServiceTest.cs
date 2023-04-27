using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class UserServiceTests : UnitTest
    {
        public UserServiceTests() : base("user_service", TestUtils.ApiKey.Production) =>
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

            User user = await Client.User.CreateChild(new Dictionary<string, object> { { "name", "Test User" } });
            CleanUpAfterTest(user.Id);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal("Test User", user.Name);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            User authenticatedUser = await Client.User.RetrieveMe();

            string id = authenticatedUser.Id;

            User user = await Client.User.Retrieve(id);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
            Assert.Equal(id, user.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieveMe()
        {
            UseVCR("retrieve_me");

            User user = await Client.User.RetrieveMe();

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.Id);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand");

            User user = await Client.User.CreateChild(new Dictionary<string, object> { { "name", "Test User" } });
            CleanUpAfterTest(user.Id);

            const string color = "#123456";
            Brand brand = await Client.User.UpdateBrand(user.Id, new Dictionary<string, object> { { "color", color } });

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
            user = await Client.User.Update(user.Id, userDict);

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

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.User.Delete(user.Id));

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
        }

        #endregion

        #endregion
    }
}
