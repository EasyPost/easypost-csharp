// ApiKeyTest.cs
// See LICENSE for licensing info.

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
