using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
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
            UseVCR("all_api_keys", ApiVersion.Latest);

            List<ApiKey> apiKeys = await Client.ApiKeys.All();

            // API keys will be censored, so we'll just check for the existence of the list
            Assert.IsNotNull(apiKeys);
        }

        [Fact]
        public async Task TestApiKeys()
        {
            UseVCR("api_keys", ApiVersion.Latest);

            User user = await Client.RetrieveMe();

            // API keys will be censored, so we'll just check for the existence of the `children` element
            List<User> children = user.Children;
            Assert.IsNotNull(children);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            User user = await CreateUser();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.Id.StartsWith("user_"));
            Assert.AreEqual("Test User", user.Name);
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete", ApiVersion.Latest);

            User user = await CreateUser();

            bool success = await user.Delete();
            Assert.IsTrue(success);

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            User authenticatedUser = await Client.RetrieveMe();

            string id = authenticatedUser.Id;

            User user = await Client.Users.Retrieve(id);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.Id.StartsWith("user_"));
            Assert.AreEqual(id, user.Id);
        }


        [Fact]
        public async Task TestRetrieveMe()
        {
            UseVCR("retrieve_me", ApiVersion.Latest);

            User user = await Client.RetrieveMe();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.Id.StartsWith("user_"));
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update", ApiVersion.Latest);

            User user = await CreateUser();

            const string testName = "New Name";

            user = await user.Update(new Users.Update
            {
                Name = testName
            });

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.Id.StartsWith("user_"));
            Assert.AreEqual(testName, user.Name);
        }

        [Fact]
        public async Task TestUpdateBrand()
        {
            UseVCR("update_brand", ApiVersion.Latest);

            User user = await CreateUser();

            string color = "#123456";
            Brand brand = await user.UpdateBrand(new Users.UpdateBrand(new Dictionary<string, object>
            {
                {
                    "color", color
                }
            }));

            Assert.IsInstanceOfType(brand, typeof(Brand));
            Assert.IsTrue(brand.Id.StartsWith("brd_"));
            Assert.AreEqual(color, brand.Color);
        }

        private async Task<User> CreateUser()
        {
            User user = await Client.Users.Create(new Users.Create
            {
                Name = "Test User"
            });

            CleanUpAfterTest(user.Id);

            return user;
        }
    }
}
