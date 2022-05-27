using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class UserTest
    {
        private static string _userId = null;

        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("user", TestUtils.ApiKey.Production);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            if (_userId != null)
            {
                try
                {
                    User retrievedUser = await User.Retrieve(_userId);
                    await retrievedUser.Delete();
                    _userId = null;
                }
                catch
                {
                    // in case we try to delete something that's already been deleted
                }
            }
        }

        private static async Task<User> RetrieveMe()
        {
            return await User.RetrieveMe();
        }

        private static async Task<User> CreateUser()
        {
            User user = await User.Create(new Dictionary<string, object>
            {
                {
                    "name", "Test User"
                }
            });
            _userId = user.id; // trigger deletion after test
            return user;
        }

        // This endpoint returns the child user keys in plain text, do not run this test.
        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            User user = await CreateUser();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual("Test User", user.name);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            User authenticatedUser = await RetrieveMe();

            string id = authenticatedUser.id;

            User user = await User.Retrieve(id);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(id, user.id);
        }


        [TestMethod]
        public async Task TestRetrieveMe()
        {
            _vcr.SetUpTest("retrieve_me");

            User user = await RetrieveMe();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");

            User user = await CreateUser();

            string testName = "New Name";

            Dictionary<string, object> userDict = new Dictionary<string, object>
            {
                {
                    "name", testName
                }
            };
            await user.Update(userDict);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(testName, user.name);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            User user = await CreateUser();

            bool success = await user.Delete();
            Assert.IsTrue(success);

            _userId = null; // skip deletion cleanup
        }

        [TestMethod]
        public async Task TestAllApiKeys()
        {
            _vcr.SetUpTest("all_api_keys");

            List<ApiKey> apiKeys = await ApiKey.All();

            // API keys will be censored, so we'll just check for the existence of the list
            Assert.IsNotNull(apiKeys);
        }

        [TestMethod]
        public async Task TestApiKeys()
        {
            _vcr.SetUpTest("api_keys");

            User user = await RetrieveMe();

            // API keys will be censored, so we'll just check for the existence of the `children` element
            List<User> children = user.children;
            Assert.IsNotNull(children);
        }

        [TestMethod]
        public async Task TestUpdateBrand()
        {
            _vcr.SetUpTest("update_brand");

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
    }
}
