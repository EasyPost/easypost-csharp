using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {

    [TestClass]
    public class CustomsInfoTest {
        [TestInitialize]
        public void Initialize() {
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";
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
    }
}