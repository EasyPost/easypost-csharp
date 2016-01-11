using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {

    [TestClass]
    public class CustomsInfoTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Dictionary<string, object> item = new Dictionary<string, object>() {
                {"description", "TShirt"}, {"quantity", 1}, {"weight", 8}, {"origin_country", "US"}
            };

            CustomsInfo info = CustomsInfo.Create(new Dictionary<string, object>() {
                {"customs_certify", true}, {"eel_pfc", "NOEEI 30.37(a)"},
                {"customs_items", new List<Dictionary<string, object>>() {item}}
            });

            CustomsInfo retrieved = CustomsInfo.Retrieve(info.id);
            Assert.AreEqual(info.id, retrieved.id);
            Assert.IsNotNull(retrieved.customs_items);
        }

        [TestMethod]
        public void TestCreateWithIResource() {
            CustomsItem item = new CustomsItem() { description = "description" };
            CustomsInfo info = CustomsInfo.Create(
                new Dictionary<string, object>() {
                    { "customs_certify", true },
                    { "eel_pfc", "NOEEI 30.37(a)" },
                    { "customs_items", new List<IResource>() { item } }
                }
            );

            Assert.IsNotNull(info.id);
            Assert.AreEqual(info.customs_items.Count, 1);
            Assert.AreEqual(info.customs_items[0].description, item.description);
        }
    }
}