using EasyPost;

using RestSharp;

using System;
using System.Linq;
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
            RestRequest request = (RestRequest) new Request("resource");
        }

        [TestMethod]
        public void TestAddPayload() {
            Request request = new Request("resource");
            request.addPayload("foo", "bar");
            Assert.AreEqual(request.payload.Select(load => load.ToString()).First(), "foo=bar");
        }

        [TestMethod]
        public void TestEncodePayload() {
            Request request = new Request("resource");
            request.addPayload("foo", "bar");
            request.addPayload("baz", "qux");

            Assert.AreEqual(request.encodePayload(), "foo=bar&baz=qux");
        }
    }
}
