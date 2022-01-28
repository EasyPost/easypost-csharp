// CarrierTypeTest.cs
// See LICENSE for licensing info.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CarrierTypeTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");

        [TestMethod]
        public void TestAll()
        {
            var types = CarrierType.All();
            Assert.AreNotEqual(0, types.Count);
        }
    }
}
