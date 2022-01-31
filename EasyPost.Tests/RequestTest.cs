﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace EasyPost.Tests
{
    [TestClass]
    public class RequestTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

        [TestMethod]
        public void TestAddBody()
        {
            Request request = new Request("resource");
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "foo", "bar"
                }
            });

            RestRequest restRequest = (RestRequest)request;
            CollectionAssert.Contains(restRequest.Parameters.Select(parameter => parameter.ToString()).ToList(),
                "application/json={\"foo\":\"bar\"}");
        }

        [TestMethod]
        public void TestCastToRestRequest()
        {
            RestRequest request = (RestRequest)new Request("resource");
        }

        // [TestMethod]
        // public void TestRestRequest() {
        //     Request request = new Request("resource");
        //     Assert.IsInstanceOfType(request.restRequest, typeof(RestRequest));
        // }

        [TestMethod]
        public void TestRootElement()
        {
            Request request = new Request("resource");
            request.RootElement = "root";
            Assert.AreEqual(request.RootElement, "root");
        }
    }
}
