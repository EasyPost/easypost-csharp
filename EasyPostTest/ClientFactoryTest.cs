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
            ClientFactory.SetCurrent(new DefaultClientFactory());

            Client.apiKey = "asdf";
            Client client = ClientFactory.Current.Build();

            Assert.AreEqual(new Uri("https://api.easypost.com/v2"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestClientFactoryInstanceConstructionWithKeyAndUrl() {
            ClientFactory.SetCurrent(new FakeClientFactoryForKeyPlusUrl());

            Client client = ClientFactory.Current.Build();
            Assert.AreEqual(new Uri("http://foobar.com"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestClientFactoryInstanceConstructionWithKey() {
            ClientFactory.SetCurrent(new FakeClientFactoryForKey());

            Client client = ClientFactory.Current.Build();
            Assert.AreEqual(new Uri("https://api.easypost.com/v2"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestClientFactoryInstanceDelegatedConstruction() {
            ClientFactory.SetCurrent(() => null);
            Assert.AreEqual(null, ClientFactory.Current);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TestClientFactorySetCurrentInstanceFailWithNullReferenceException() {
            ClientFactory.SetCurrent((IClientFactory)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestClientFactorySetCurrentDelegateFailWithNullReferenceException() {
            ClientFactory.SetCurrent((Func<IClientFactory>)null);
        }

    }
}
