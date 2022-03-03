using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class UserTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Production);
        }

        private static User RetrieveMe()
        {
            return User.RetrieveMe();
        }

        private static User CreateUser()
        {
            return User.Create(new Dictionary<string, object>
            {
                {
                    "name", "Test User"
                }
            });
        }

        // This endpoint returns the child user keys in plain text, do not run this test.
        [Ignore]
        [TestMethod]
        public void TestCreate()
        {
            User user = CreateUser();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual("Test User", user.name);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            User user = User.Retrieve(Fixture.ChildUserId);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(Fixture.ChildUserId, user.id);
        }


        [TestMethod]
        public void TestRetrieveMe()
        {
            User user = RetrieveMe();

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
        }

        [TestMethod]
        public void TestUpdate()
        {
            User user = RetrieveMe();

            string testPhone = "5555555555";

            Dictionary<string, object> userDict = new Dictionary<string, object>
            {
                {
                    "phone_number", testPhone
                }
            };
            user.Update(userDict);

            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsTrue(user.id.StartsWith("user_"));
            Assert.AreEqual(testPhone, user.phone_number);
        }

        // Due to our inability to create child users securely, we must also skip deleting them as we cannot replace the deleted ones easily.
        [Ignore]
        [TestMethod]
        public void TestDelete()
        {
            User user = CreateUser();

            user.Destroy();
        }

        // API keys are returned as plaintext, do not run this test.
        [Ignore]
        [TestMethod]
        public void TestAllApiKeys()
        {
            User user = RetrieveMe();

            // TODO: User doesn't have a .all_api_keys() method
            List<ApiKey> apiKeys = user.api_keys;
        }

        // API keys are returned as plaintext, do not run this test.
        [Ignore]
        [TestMethod]
        public void TestApiKeys()
        {
            User user = RetrieveMe();

            List<ApiKey> apiKeys = user.api_keys;
        }

        [TestMethod]
        public void TestUpdateBrand()
        {
            User user = RetrieveMe();

            string color = "#123456";
            Brand brand = user.UpdateBrand(new Dictionary<string, object>
            {
                {
                    "color", color
                }
            });

            Assert.IsInstanceOfType(brand, typeof(Brand));
            // TODO: Brand doesn't have an ID
            Assert.AreEqual(color, brand.color);
        }
    }
}
