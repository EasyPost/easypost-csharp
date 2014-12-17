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
        [TestMethod]
        public void TestApiKey() {
            Client.apiKey = "apiKey";
            Assert.AreEqual(Client.apiKey, "apiKey");
        }

        [TestMethod]
        public void TestRestClient() {
            Client client = new Client();
            Assert.AreEqual(client.restClient.BaseUrl, "https://api.easypost.com/v2");
        }

        [TestMethod]
        public void TestRestClientWithBase() {
            Client client = new Client("http://apiBase.com");
            Assert.AreEqual(client.restClient.BaseUrl, "http://apiBase.com");
        }

        [TestMethod]
        public void TestExecute() {
            Client client = new Client("http://echo.jsontest.com");
            Request request = new Request("key/value", Method.GET);

            JsonTest response = client.Execute<JsonTest>(request);
            Assert.AreEqual(response.key, "value");
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestExecuteFail() {
            Client client = new Client("http://as.asdf");
            client.Execute<JsonTest>(new Request(""));
        }

        [TestMethod]
        public void TestPrepareRequest() {
            Client.apiKey = "apiKey";
            Client client = new Client();
            Request request = new Request("resource");

            List<String> parameters = client.prepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + client.version);
            CollectionAssert.Contains(parameters, "authorization=Bearer " + Client.apiKey);
            CollectionAssert.Contains(parameters, "content_type=application/x-www-form-urlencoded");
        }
    }
}
