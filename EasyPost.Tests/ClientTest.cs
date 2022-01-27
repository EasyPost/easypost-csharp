// ClientTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//public class JsonTest {
//    public string key { get; set; }
//}

//namespace EasyPost.Tests {
//    [TestClass]
//    public class ClientTest {

//        [TestMethod]
//        public void TestApiKey() {
//            ClientManager.SetCurrent("apiKey");
//            Assert.AreEqual("apiKey", ClientManager.Build().configuration.ApiKey);
//        }

//        [TestMethod]
//        public void TestApiBase() {
//            var client = new Client(new ClientConfiguration("apiKey", "https://foobar.com"));
//            Assert.AreEqual(new System.Uri("https://foobar.com"), client.client.BaseUrl);
//        }

//        [TestMethod]
//        public void TestRestClient() {
//            var client = new Client(new ClientConfiguration("apiKey"));
//            Assert.AreEqual(client.client.BaseUrl, "https://api.easypost.com/");
//        }

//        [TestMethod]
//        public void TestRestClientWithOptions() {
//            var client = new Client(new ClientConfiguration("someapikey", "http://apiBase.com"));
//            Assert.AreEqual(new Uri("http://apiBase.com"), client.client.BaseUrl);
//        }

//        [TestMethod]
//        public void TestPrepareRequest() {
//            const string apiKey = "apiKey";

//            ClientManager.SetCurrent(apiKey);
//            var request = new Request("resource");

//            var parameters = ClientManager.Build().PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
//            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + ClientManager.Build().version);
//            CollectionAssert.Contains(parameters, "authorization=Bearer " + apiKey);
//            CollectionAssert.Contains(parameters, "content_type=application/json");
//        }

//        [TestMethod]
//        public void TestPrepareRequestWithOptions() {
//            var client = new Client(new ClientConfiguration("someapikey", "http://foobar.com"));
//            var request = new Request("resource");

//            var parameters = client.PrepareRequest(request).Parameters.Select(parameter => parameter.ToString()).ToList();
//            CollectionAssert.Contains(parameters, "user_agent=EasyPost/v2 CSharp/" + client.version);
//            CollectionAssert.Contains(parameters, "authorization=Bearer someapikey");
//            CollectionAssert.Contains(parameters, "content_type=application/json");
//        }
//    }
//}



