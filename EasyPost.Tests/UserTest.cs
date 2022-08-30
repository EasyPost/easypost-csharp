using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class UserTest : UnitTest
    {
        public UserTest() : base("user", TestUtils.ApiKey.Production) =>
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

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            User user = await CreateUser();

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.id);
            Assert.Equal("Test User", user.name);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand");

            User user = await CreateUser();

            string color = "#123456";
            Brand brand = await user.UpdateBrand(new Dictionary<string, object>
            {
                {
                    "color", color
                }
            });

            Assert.IsType<Brand>(brand);
            Assert.StartsWith("brd_", brand.id);
            Assert.Equal(color, brand.color);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            User authenticatedUser = await RetrieveMe();

            string id = authenticatedUser.id;

            User user = await Client.User.Retrieve(id);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.id);
            Assert.Equal(id, user.id);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieveMe()
        {
            UseVCR("retrieve_me");

            User user = await RetrieveMe();

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.id);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestUpdate()
        {
            UseVCR("update");

            User user = await CreateUser();

            string testName = "New Name";

            Dictionary<string, object> userDict = new Dictionary<string, object>
            {
                {
                    "name", testName
                }
            };
            user = await user.Update(userDict);

            Assert.IsType<User>(user);
            Assert.StartsWith("user_", user.id);
            Assert.Equal(testName, user.name);
        }

        [Fact]
        [CrudOperations.Delete]
        public async Task TestDelete()
        {
            UseVCR("delete");

            User user = await CreateUser();

            var possibleException = await Record.ExceptionAsync(async () => await user.Delete());

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
        }

        #endregion

        private async Task<User> CreateUser()
        {
            User user = await Client.User.CreateChild(new Dictionary<string, object>
            {
                {
                    "name", "Test User"
                }
            });
            CleanUpAfterTest(user.id);

            return user;
        }

        private async Task<User> RetrieveMe() => await Client.User.RetrieveMe();
    }
}
