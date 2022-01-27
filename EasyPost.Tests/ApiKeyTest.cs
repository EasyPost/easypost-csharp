// ApiKeyTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ApiKeyTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");

        [TestMethod]
        public void TestList()
        {
            var keys = ApiKey.All();
            Assert.AreEqual(keys.Count, 2);
        }
    }
}
