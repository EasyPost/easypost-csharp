// UserTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");

        [TestMethod]
        public void TestRetrieveSelf()
        {
            User user = User.Retrieve();
            Assert.IsNotNull(user.id);

            User user2 = User.Retrieve(user.id);
            Assert.AreEqual(user.id, user2.id);
        }

        // [TestMethod]
        // public void TestCRUD() {
        //     try {
        //         User.Create(new Dictionary<string, object>() { { "name", "foo" } });
        //         Assert.Fail();
        //     } catch (HttpException e) {
        //         Assert.AreEqual("USER.INVALID", e.Code);
        //         Assert.AreEqual("The user record was invalid and could not be saved.", e.Message);
        //         Assert.AreEqual(1, e.Errors.Count);
        //         Assert.AreEqual("name", e.Errors[0].field);
        //         Assert.AreEqual("First and last name required.", e.Errors[0].message);
        //     }

        //     var user = User.Create(new Dictionary<string, object>() { { "name", "Test Name" } });
        //     Assert.AreEqual(user.api_keys.Count, 2);
        //     Assert.IsNotNull(user.id);

        //     var other = User.Retrieve(user.id);
        //     Assert.AreEqual(user.id, other.id);

        //     user.Update(new Dictionary<string, object>() { { "name", "NewTest Name" } });
        //     Assert.AreEqual("NewTest Name", user.name);

        //     user.Destroy();
        //     try {
        //         User.Retrieve(user.id);
        //         Assert.Fail();
        //     } catch (HttpException) { }
        // }
    }
}
