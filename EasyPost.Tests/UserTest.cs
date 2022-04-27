using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class UserTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("user", TestUtils.ApiKey.Production);
        }

        private static async Task<User> RetrieveMe(V2Client v2Client)
        {
            return await v2Client.Users.RetrieveMe();
        }

        private static async Task<User> CreateUser(V2Client v2Client)
        {
            return await v2Client.Users.Create(new Dictionary<string, object>
            {
                {
                    "name", "Test User"
                }
            });
        }

        // This endpoint returns the child user keys in plain text, do not run this test.
        [Ignore]
        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            User user = await CreateUser(v2Client);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual("Test User", user.name);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            User authenticatedUser = await RetrieveMe(v2Client);

            string childId = authenticatedUser.children[0].id;

            User user = await v2Client.Users.Retrieve(childId);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(childId, user.id);
        }


        [TestMethod]
        public async Task TestRetrieveMe()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve_me");

            User user = await RetrieveMe(v2Client);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            V2Client v2Client = _vcr.SetUpTest("update");

            User user = await RetrieveMe(v2Client);

            string testPhone = "5555555555";

            Dictionary<string, object> userDict = new Dictionary<string, object>
            {
                {
                    "phone_number", testPhone
                }
            };
            await user.Update(userDict);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(testPhone, user.phone_number);
        }

        // Due to our inability to create child users securely, we must also skip deleting them as we cannot replace the deleted ones easily.
        [Ignore]
        [TestMethod]
        public async Task TestDelete()
        {
            V2Client v2Client = _vcr.SetUpTest("delete");


            User user = await CreateUser(v2Client);

            await user.Delete();
        }

        // API keys are returned as plaintext, do not run this test.
        [Ignore]
        [TestMethod]
        public async Task TestAllApiKeys()
        {
            V2Client v2Client = _vcr.SetUpTest("all_api_keys");


            User user = await RetrieveMe(v2Client);

            // TODO: User doesn't have a .all_api_keys() method
            List<ApiKey> apiKeys = user.api_keys;
        }

        // API keys are returned as plaintext, do not run this test.
        [Ignore]
        [TestMethod]
        public async Task TestApiKeys()
        {
            V2Client v2Client = _vcr.SetUpTest("api_keys");

            User user = await RetrieveMe(v2Client);

            List<ApiKey> apiKeys = user.api_keys;
        }

        [TestMethod]
        public async Task TestUpdateBrand()
        {
            V2Client v2Client = _vcr.SetUpTest("update_brand");

            User user = await RetrieveMe(v2Client);

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
