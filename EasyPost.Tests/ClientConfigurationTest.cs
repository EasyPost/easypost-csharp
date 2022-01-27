// <copyright file="ClientConfigurationTest.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ClientConfigurationTest
    {
        [TestMethod]
        public void TestApiKeyConstructor()
        {
            var config = new ClientConfiguration("someApiKey");

            Assert.AreEqual("someApiKey", config.ApiKey);
            Assert.AreEqual("https://api.easypost.com/v2", config.ApiBase);
        }

        [TestMethod]
        public void TestApiKeyPlusBaseUrlConstructor()
        {
            var config = new ClientConfiguration("someApiKey", "http://foobar.com");

            Assert.AreEqual("someApiKey", config.ApiKey);
            Assert.AreEqual("http://foobar.com", config.ApiBase);
        }
    }
}
