using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
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
                    User retrievedUser = await Client.Users.Retrieve(id);
                    return await retrievedUser.Delete();
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
            UseVCR("all_api_keys", ApiVersion.V2);

            List<ApiKey> apiKeys = await Client.ApiKeys.All();

            // API keys will be censored, so we'll just check for the existence of the list
            Assert.IsNotNull(apiKeys);
        }

        [Fact]
        public async Task TestApiKeys()
        {
            UseVCR("api_keys", ApiVersion.V2);

            User user = await RetrieveMe();

            // API keys will be censored, so we'll just check for the existence of the `children` element
            List<User> children = user.children;
            Assert.IsNotNull(children);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.V2);

            User user = await CreateUser();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual("Test User", user.name);
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete", ApiVersion.V2);

            User user = await CreateUser();

            bool success = await user.Delete();
            Assert.IsTrue(success);

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            User authenticatedUser = await RetrieveMe();

            string id = authenticatedUser.id;

            User user = await Client.Users.Retrieve(id);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(id, user.id);
        }


        [Fact]
        public async Task TestRetrieveMe()
        {
            UseVCR("retrieve_me", ApiVersion.V2);

            User user = await RetrieveMe();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update", ApiVersion.V2);

            User user = await CreateUser();

            string testName = "New Name";

            Dictionary<string, object> userDict = new Dictionary<string, object>()
            {
                {
                    "name",
                    testName
                }
            };
            await user.Update(userDict);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(testName, user.name);
        }

        [Fact]
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand", ApiVersion.V2);

            User user = await CreateUser();

            string color = "#123456";
            Brand brand = await user.UpdateBrand(new Dictionary<string, object>
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
            User user = await Client.Users.Create(new Dictionary<string, object>
            {
                {
                    "name", "Test User"
                }
            });
            CleanUpAfterTest(user.id);

            return user;
        }

        private async Task<User> RetrieveMe() => await Client.Users.RetrieveMe();
    }
}
