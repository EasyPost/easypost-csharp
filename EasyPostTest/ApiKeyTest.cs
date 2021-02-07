using EasyPost;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ApiKeyTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");
        }

        [TestMethod]
        public void TestList() {
            List<ApiKey> keys = ApiKey.All();
            Assert.AreEqual(2, keys.Count);
        }
    }
}
