using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace EasyPostTest {
    [TestClass]
    public class ClientManagerTest {
        [TestMethod]
        [ExpectedException(typeof(ClientNotConfigured))]
        public void TestNotConfigured() {
            ClientManager.Unconfigure();
            ClientManager.Build();
        }

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

        [TestMethod]
        public void TestThreadSafeConfiguration()
        {
            ClientManager.SetThreadStatic(true);

            Thread t1 = new Thread(() =>
            {
                bool hasCurrent = true;

                try
                {
                    Client c = ClientManager.Build();
                    hasCurrent = true;
                }
                catch (ClientNotConfigured)
                {
                    hasCurrent = false;
                }

                Assert.AreEqual(hasCurrent, false);

                ClientManager.SetCurrent("threadApiKey");
                Client client1 = ClientManager.Build();
            });

            Thread t2 = new Thread(() =>
            {
                bool hasCurrent = true;

                try
                {
                    Client c = ClientManager.Build();
                    hasCurrent = true;
                }
                catch (ClientNotConfigured)
                {
                    hasCurrent = false;
                }

                Assert.AreEqual(hasCurrent, false);

                ClientManager.SetCurrent("anotherThreadApiKey");
                Client client2 = ClientManager.Build();
            });

            t1.Start();
            t2.Start();
            t1.Abort();
            t2.Abort();
        }
    }
}
