using EasyPost;

using RestSharp;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class RequestTest {
        [TestMethod]
        public void TestRestRequest() {
            Request request = new Request("resource");
            Assert.IsInstanceOfType(request.restRequest, typeof(RestRequest));
        }

        [TestMethod]
        public void TestRootElement() {
            Request request = new Request("resource");
            request.RootElement = "root";
            Assert.AreEqual(request.RootElement, "root");
        }

        [TestMethod]
        public void TestCastToRestRequest() {
            RestRequest request = (RestRequest)new Request("resource");
        }

        [TestMethod]
        public void TestAddBody() {
            Request request = new Request("resource");
            request.addBody(new Dictionary<string, object>() { { "foo", "bar" } }, "parent");

            RestRequest restRequest = (RestRequest)request;
            CollectionAssert.Contains(restRequest.Parameters.Select(parameter => parameter.ToString()).ToList(), "application/x-www-form-urlencoded=parent%5Bfoo%5D=bar");
        }

        [TestMethod]
        public void TestEncodePayload() {
            Request request = new Request("resource");
            string result = request.encodeParameters(new List<Tuple<string, string>>() {
                new Tuple<string, string>("parent[foo]", "bar"),
                new Tuple<string, string>("parent[baz]", "qux")
            });
            Assert.AreEqual(result, "parent%5Bfoo%5D=bar&parent%5Bbaz%5D=qux");
        }


        [TestMethod]
        public void TestFlattenParameters() {
            Request request = new Request("resource");
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "foo", "bar" }, { "baz", "qux" } };
            List<Tuple<string, string>> result = request.flattenParameters(parameters, "parent");
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[foo]", "bar"));
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[baz]", "qux"));
        }

        [TestMethod]
        public void TestFlattenParametersWithNestedDictionary() {
            Request request = new Request("resource");
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"foo", new Dictionary<string, object>() {{"bar", "baz"}}},
                {"baz", "qux"}
            };
            List<Tuple<string, string>> result = request.flattenParameters(parameters, "parent");
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[foo][bar]", "baz"));
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[baz]", "qux"));
        }

        [TestMethod]
        public void TestFlattenParametersWithNestedList() {
            Request request = new Request("resource");
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"foo", new List<Dictionary<string, object>>() {new Dictionary<string, object>() {{"bar", "baz"}}}},
                {"baz", "qux"}
            };
            List<Tuple<string, string>> result = request.flattenParameters(parameters, "parent");
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[foo][0][bar]", "baz"));
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[baz]", "qux"));
        }
    }
}
