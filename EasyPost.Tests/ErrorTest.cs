// <copyright file="ErrorTest.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
