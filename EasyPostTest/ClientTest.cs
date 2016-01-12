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
            Assert.AreEqual(client.client.BaseUrl, "https://api.easypost.com/v2");
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
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + ClientManager.Build().version);
            CollectionAssert.Contains(parameters, "authorization=Bearer " + apiKey);
            CollectionAssert.Contains(parameters, "content_type=application/x-www-form-urlencoded");
        }

        [TestMethod]
        public void TestPrepareRequestWithOptions() {
            Client client = new Client(new ClientConfiguration("someapikey", "http://foobar.com"));
            Request request = new Request("resource");

            List<String> parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + client.version);
            CollectionAssert.Contains(parameters, "authorization=Bearer someapikey");
            CollectionAssert.Contains(parameters, "content_type=application/x-www-form-urlencoded");
        }
    }
}
