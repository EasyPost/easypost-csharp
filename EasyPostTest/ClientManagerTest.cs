using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ClientManagerTest {
        [TestMethod]
        public void TestApiKey() {
            ClientManager.SetCurrent("apiKey");
            Client client = ClientManager.Build();
            Assert.AreEqual("apiKey", client.configuration.ApiKey);
        }

        [TestMethod]
        public void TestApiKeyForUniqueObjects() {
            ClientManager.SetCurrent("apiKey");
            Client client1 = ClientManager.Build();
            Client client2 = ClientManager.Build();

            Assert.AreNotSame(client1, client2);
        }

        [TestMethod]
        public void TestFactoryDelegate() {
            ClientManager.SetCurrent(() => new Client(new ClientConfiguration("apiKey")));
            Client client = ClientManager.Build();
            Assert.AreEqual("apiKey", client.configuration.ApiKey);
        }

        [TestMethod]
        public void TestFactoryDelegateForUniqueObjects() {
            ClientManager.SetCurrent(() => new Client(new ClientConfiguration("apiKey")));
            Client client1 = ClientManager.Build();
            Client client2 = ClientManager.Build();

            Assert.AreNotSame(client1, client2);
        }
    }
}
