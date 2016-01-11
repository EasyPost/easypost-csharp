using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class CustomsItemTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            CustomsItem item = CustomsItem.Create(new Dictionary<string, object>() {
                {"description", "TShirt"},
                {"quantity", 1},
                {"weight", 8},
                {"value", 10.0},
                {"currency", "USD"}
            });
            CustomsItem retrieved = CustomsItem.Retrieve(item.id);
            Assert.AreEqual(item.id, retrieved.id);
            Assert.AreEqual(retrieved.value, 10.0);
            Assert.AreEqual(retrieved.currency, "USD");
        }
    }
}