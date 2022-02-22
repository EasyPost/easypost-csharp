using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class ApiKeyTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");

        [TestMethod]
        public void TestAll()
        {
            List<ApiKey> keys = ApiKey.All();
            Assert.AreEqual(keys.Count, 2);
        }
    }
}
