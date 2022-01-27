// RequestTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;
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
            var request = new Request("resource");
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "foo", "bar"
                }
            });

            var restRequest = (RestRequest)request;
            CollectionAssert.Contains(restRequest.Parameters.Select(parameter => parameter.ToString()).ToList(),
                "application/json={\"foo\":\"bar\"}");
        }

        [TestMethod]
        public void TestCastToRestRequest()
        {
            var request = (RestRequest)new Request("resource");
        }

        // [TestMethod]
        // public void TestRestRequest() {
        //     var request = new Request("resource");
        //     Assert.IsInstanceOfType(request.restRequest, typeof(RestRequest));
        // }

        [TestMethod]
        public void TestRootElement()
        {
            var request = new Request("resource");
            request.RootElement = "root";
            Assert.AreEqual(request.RootElement, "root");
        }
    }
}
