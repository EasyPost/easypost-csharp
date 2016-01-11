using System;
using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class ClientFactoryTest {

        private class FakeClientFactoryForKeyPlusUrl : IClientFactory {
            public Client Build(){
                return new Client(new ClientConfiguration("someapikey","http://foobar.com"));
            }
        }

        private class FakeClientFactoryForKey : IClientFactory {
            public Client Build() {
                return new Client(new ClientConfiguration("someapikey"));
            }
        }

        [TestMethod]
        public void TestDefaultFactoryClientConstruction() {
            ClientManager.SetCurrent(new DefaultClientFactory());

            Client.apiKey = "asdf";
            Client client = ClientManager.Current.Build();

            Assert.AreEqual(new Uri("https://api.easypost.com/v2"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestClientFactoryInstanceConstructionWithKeyAndUrl() {
            ClientManager.SetCurrent(new FakeClientFactoryForKeyPlusUrl());

            Client client = ClientManager.Current.Build();
            Assert.AreEqual(new Uri("http://foobar.com"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestClientFactoryInstanceConstructionWithKey() {
            ClientManager.SetCurrent(new FakeClientFactoryForKey());

            Client client = ClientManager.Current.Build();
            Assert.AreEqual(new Uri("https://api.easypost.com/v2"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestClientFactoryInstanceDelegatedConstruction() {
            ClientManager.SetCurrent(() => null);
            Assert.AreEqual(null, ClientManager.Current);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TestClientFactorySetCurrentInstanceFailWithNullReferenceException() {
            ClientManager.SetCurrent((IClientFactory)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestClientFactorySetCurrentDelegateFailWithNullReferenceException() {
            ClientManager.SetCurrent((Func<IClientFactory>)null);
        }

    }
}
