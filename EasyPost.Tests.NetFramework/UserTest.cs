using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class UserTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");

        [TestMethod]
        public void TestRetrieveMe()
        {
            User user = User.RetrieveMe();
            Assert.IsNotNull(user.id);
        }

        [TestMethod]
        public void TestRetrieveSelf()
        {
            User user = User.Retrieve();
            Assert.IsNotNull(user.id);

            User user2 = User.Retrieve(user.id);
            Assert.AreEqual(user.id, user2.id);
        }

        [TestMethod]
        public void TestUpdateBrand()
        {
            User user = User.Retrieve();
            Assert.IsNotNull(user.id);

            const string color = "#AA4A44";
            Dictionary<string, object> details = new Dictionary<string, object>()
            {
                {
                    "color", color
                }
            };
            Brand brand = user.UpdateBrand(parameters: details);
            Assert.IsNotNull(brand);
            Assert.AreEqual(user.id, brand.user_id);
            Assert.AreEqual(color, brand.color);
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

        //     User user = User.Create(new Dictionary<string, object>() { { "name", "Test Name" } });
        //     Assert.AreEqual(user.api_keys.Count, 2);
        //     Assert.IsNotNull(user.id);

        //     User other = User.Retrieve(user.id);
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
