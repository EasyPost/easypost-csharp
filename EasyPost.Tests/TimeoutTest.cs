using EasyPost;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests {
    [TestClass]
    public class TimeoutTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");
        }

        [TestMethod]
        public void TestTimeout() {
            var client = new Client(new ClientConfiguration("apikey"));
            client.setConnectionTimeout(5000);
            client.setRequestTimeout(5000);

            Assert.AreEqual(5000, client.getConnectionTimeout());
            Assert.AreEqual(5000, client.getRequestTimeout());
        }
    }
}
