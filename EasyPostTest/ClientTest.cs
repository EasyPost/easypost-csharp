using EasyPost;

using RestSharp;

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
        [TestCleanup]
        public void Cleanup() {
            Client.apiBase = null;
        }

        [TestMethod]
        public void TestApiKey() {
            Client.apiKey = "apiKey";
            Assert.AreEqual(Client.apiKey, "apiKey");
        }

        [TestMethod]
        public void TestApiBase() {
            Client.apiBase = "https://foobar.com";
            Client client = new Client();
            Assert.AreEqual(new System.Uri("https://foobar.com"), client.client.BaseUrl);
        }

        [TestMethod]
        public void TestRestClient() {
            Client client = new Client();
            Assert.AreEqual(client.client.BaseUrl, "https://api.easypost.com/v2");
        }

        [TestMethod]
        public void TestRestClientWithBase() {
            Client client = new Client("http://apiBase.com");
            Assert.AreEqual(client.client.BaseUrl, "http://apiBase.com");
        }

        [TestMethod]
        public void TestPrepareRequest() {
            Client.apiKey = "apiKey";
            Client client = new Client();
            Request request = new Request("resource");

            List<String> parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + client.version);
            CollectionAssert.Contains(parameters, "authorization=Bearer " + Client.apiKey);
            CollectionAssert.Contains(parameters, "content_type=application/x-www-form-urlencoded");
        }
    }
}
