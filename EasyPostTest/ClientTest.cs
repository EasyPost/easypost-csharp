﻿using EasyPost;

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

        [TestInitialize]
        public void BeforeEachTest() {
            // clean out the API key + base before each test; let the constructor defaults do their thing
            Client.apiKey = null;
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
        public void TestRestClientWithOptions() {
            Client client = new Client(new ClientConfiguration("someapikey", "http://apiBase.com"));
            Assert.AreEqual(new Uri("http://apiBase.com"), client.client.BaseUrl);
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

            List<String> parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + client.version);
            CollectionAssert.Contains(parameters, "authorization=Bearer " + Client.apiKey);
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
