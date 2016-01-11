using EasyPost;

using RestSharp;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class RequestTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

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
            request.AddBody(new Dictionary<string, object>() { { "foo", "bar" } }, "parent");

            RestRequest restRequest = (RestRequest)request;
            CollectionAssert.Contains(restRequest.Parameters.Select(parameter => parameter.ToString()).ToList(), "application/x-www-form-urlencoded=parent%5Bfoo%5D=bar");
        }


        [TestMethod]
        public void TestAddBodyWithListOfIResource() {
            Request request = new Request("resource");
            Address address = Address.Retrieve("adr_f1369ed31d114c308f627d8879655bd5");
            request.AddBody(new Dictionary<string, object>() { { "foo", new List<Address>() { address } } }, "parent");

            RestRequest restRequest = (RestRequest)request;
            CollectionAssert.Contains(restRequest.Parameters.Select(parameter => parameter.ToString()).ToList(), "application/x-www-form-urlencoded=parent%5Bfoo%5D%5B0%5D%5Bid%5D=adr_f1369ed31d114c308f627d8879655bd5&parent%5Bfoo%5D%5B0%5D%5Bcreated_at%5D=9%2F15%2F2015%204%3A03%3A23%20PM&parent%5Bfoo%5D%5B0%5D%5Bupdated_at%5D=9%2F15%2F2015%204%3A03%3A23%20PM&parent%5Bfoo%5D%5B0%5D%5Bname%5D=EasyPost&parent%5Bfoo%5D%5B0%5D%5Bstreet1%5D=164%20Townsend%20St&parent%5Bfoo%5D%5B0%5D%5Bstreet2%5D=Unit%201&parent%5Bfoo%5D%5B0%5D%5Bcity%5D=San%20Francisco&parent%5Bfoo%5D%5B0%5D%5Bstate%5D=CA&parent%5Bfoo%5D%5B0%5D%5Bzip%5D=94107&parent%5Bfoo%5D%5B0%5D%5Bcountry%5D=US&parent%5Bfoo%5D%5B0%5D%5Bphone%5D=4154567890&parent%5Bfoo%5D%5B0%5D%5Bresidential%5D=False&parent%5Bfoo%5D%5B0%5D%5Bmode%5D=test");
        }

        [TestMethod]
        public void TestEncodePayload() {
            Request request = new Request("resource");
            string result = request.EncodeParameters(new List<Tuple<string, string>>() {
                new Tuple<string, string>("parent[foo]", "bar"),
                new Tuple<string, string>("parent[baz]", "qux")
            });
            Assert.AreEqual(result, "parent%5Bfoo%5D=bar&parent%5Bbaz%5D=qux");
        }


        [TestMethod]
        public void TestFlattenParameters() {
            Request request = new Request("resource");
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "foo", "bar" }, { "baz", "qux" } };
            List<Tuple<string, string>> result = request.FlattenParameters(parameters, "parent");
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
            List<Tuple<string, string>> result = request.FlattenParameters(parameters, "parent");
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
            List<Tuple<string, string>> result = request.FlattenParameters(parameters, "parent");
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[foo][0][bar]", "baz"));
            CollectionAssert.Contains(result, new Tuple<string, string>("parent[baz]", "qux"));
        }
    }
}
