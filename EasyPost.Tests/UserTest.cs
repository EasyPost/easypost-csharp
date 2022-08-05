using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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

        [Fact]
        public async Task TestAllApiKeys()
        {
            UseVCR("all_api_keys");

            List<ApiKey> apiKeys = await Client.ApiKey.All();

            // API keys will be censored, so we'll just check for the existence of the list
            Assert.IsNotNull(apiKeys);
        }

        [Fact]
        public async Task TestApiKeys()
        {
            UseVCR("api_keys");

            User user = await RetrieveMe();

            // API keys will be censored, so we'll just check for the existence of the `children` element
            List<User> children = user.children;
            Assert.IsNotNull(children);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            User user = await CreateUser();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual("Test User", user.name);
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete");

            User user = await CreateUser();

            await user.Delete();

            // TODO: Assert something

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            User authenticatedUser = await RetrieveMe();

            string id = authenticatedUser.id;

            User user = await Client.User.Retrieve(id);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(id, user.id);
        }


        [Fact]
        public async Task TestRetrieveMe()
        {
            UseVCR("retrieve_me");

            User user = await RetrieveMe();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update");

            User user = await CreateUser();

            string testName = "New Name";

            Dictionary<string, object?> userDict = new Dictionary<string, object?>
            {
                {
                    "name",
                    testName
                }
            };
            user = await user.Update(userDict);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(testName, user.name);
        }

        [Fact]
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand");

            User user = await CreateUser();

            string color = "#123456";
            Brand brand = await user.UpdateBrand(new Dictionary<string, object?>
            {
                {
                    "color", color
                }
            });

            Assert.IsInstanceOfType(brand, typeof(Brand));
            Assert.IsTrue(brand.id.StartsWith("brd_"));
            Assert.AreEqual(color, brand.color);
        }

        private async Task<User> CreateUser()
        {
            User user = await Client.User.CreateChild(new Dictionary<string, object?>
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
