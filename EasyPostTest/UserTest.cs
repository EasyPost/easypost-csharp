using EasyPost;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class UserTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("VJ63zukvLyxz92NKP1k0EQ");
        }

        [TestMethod]
        public void TestRetrieveSelf() {
            User user = User.Retrieve();
            Assert.IsNotNull(user.id);

            User user2 = User.Retrieve(user.id);
            Assert.AreEqual(user.id, user2.id);
        }

        [TestMethod]
        public void TestCRUD() {
            User user = User.Create(new Dictionary<string, object>() { { "name", "Test Name" } });
            Assert.AreEqual(user.api_keys.Count, 2);
            Assert.IsNotNull(user.id);

            User other = User.Retrieve(user.id);
            Assert.AreEqual(user.id, other.id);

            user.Update(new Dictionary<string, object>() { { "name", "NewTest Name" } });
            Assert.AreEqual("NewTest Name", user.name);

            user.Destroy();
            try {
                User.Retrieve(user.id);
                Assert.Fail();
            } catch (HttpException) { }
        }
    }
}
