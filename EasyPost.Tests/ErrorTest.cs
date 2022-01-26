using EasyPost;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPost.Tests
{
    [TestClass]
    public class ErrorTest
    {
        string error;

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            error =
                "{\"code\":\"E.ADDRESS.NOT_FOUND\",\"field\":\"address\",\"suggestion\":\"foobar\",\"message\":\"Address not found\"}";
        }

        [TestMethod]
        public void TestErrorLoad()
        {
            Error e = Error.Load<Error>(error);
            Assert.AreEqual("E.ADDRESS.NOT_FOUND", e.code);
            Assert.AreEqual("Address not found", e.message);
            Assert.AreEqual("address", e.field);
            Assert.AreEqual("foobar", e.suggestion);
        }
    }
}