using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class JsonTest {
    public string key { get; set; }
}

namespace EasyPostTest {
    [TestClass]
    public class ClientTest {

        [TestMethod]
        public void TestApiKey() {
            ClientManager.SetCurrent("apiKey");
            Assert.AreEqual("apiKey", ClientManager.Build().configuration.ApiKey);
        }

        [TestMethod]
        public void TestApiBase() {
            Client client = new Client(new ClientConfiguration("apiKey", "https://foobar.com"));
            Assert.AreEqual(new System.Uri("https://foobar.com"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestRestClient() {
            Client client = new Client(new ClientConfiguration("apiKey"));
            Assert.AreEqual(new System.Uri("https://api.easypost.com/"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestRestClientWithOptions() {
            Client client = new Client(new ClientConfiguration("someapikey", "http://apiBase.com"));
            Assert.AreEqual(new Uri("http://apiBase.com"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestPrepareRequest() {
            const string apiKey = "apiKey";

            ClientManager.SetCurrent(apiKey);
            Request request = new Request("resource");

            List<String> parameters = ClientManager.Build().PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();

#if NET35
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET 3.5)/", ClientManager.Build().version);
#elif NET40
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET 4.0)/", ClientManager.Build().version);
#elif NETCORE31
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET Core 3.1)/", ClientManager.Build().version);
#else
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET 4.5.2)/", ClientManager.Build().version);
#endif

#if DEBUG
            user_agent += " [DEBUG]";
#endif

            CollectionAssert.Contains(parameters, "user_agent=" + user_agent);
            CollectionAssert.Contains(parameters, "authorization=Bearer " + apiKey);
            CollectionAssert.Contains(parameters, "content_type=application/json");
        }

        [TestMethod]
        public void TestPrepareRequestWithOptions() {
            Client client = new Client(new ClientConfiguration("someapikey", "http://foobar.com"));
            Request request = new Request("resource");

            List<String> parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();

#if NET35
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET 3.5)/", client.version);
#elif NET40
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET 4.0)/", client.version);
#elif NETCORE31
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET Core 3.1)/", client.version);
#else
            string user_agent = string.Concat("EasyPost/v2 CSharp (.NET 4.5.2)/", client.version);
#endif

#if DEBUG
            user_agent += " [DEBUG]";
#endif

            CollectionAssert.Contains(parameters, "user_agent=" + user_agent);
            CollectionAssert.Contains(parameters, "authorization=Bearer someapikey");
            CollectionAssert.Contains(parameters, "content_type=application/json");
        }
    }
}
