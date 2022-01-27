// <copyright file="ApiKeyTest.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ApiKeyTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");
        }

        [TestMethod]
        public void TestList()
        {
            var keys = ApiKey.All();
            Assert.AreEqual(keys.Count, 2);
        }
    }
}
