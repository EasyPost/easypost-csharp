using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class CustomsItemTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
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
            Assert.AreEqual(10.0, retrieved.value);
            Assert.AreEqual("USD", retrieved.currency);
        }
    }
}