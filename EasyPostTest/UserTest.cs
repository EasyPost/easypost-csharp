using EasyPost;

using System;
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
        public void TestRetrieve() {
            User user = User.Retrieve();
            Assert.IsNotNull(user.id);
        }

        [TestMethod]
        public void TestCreateAndUpdate() {
            User user = User.Create(new Dictionary<string, object>() { { "name", "Test Name" } });

            Assert.IsNotNull(user.id);

            user.Update(new Dictionary<string, object>() { { "name", "NewTest Name" } });
            Assert.AreEqual("NewTest Name", user.name);
        }
    }
}
