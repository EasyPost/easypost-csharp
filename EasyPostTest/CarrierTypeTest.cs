﻿using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class CarrierTypeTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");
        }

        [TestMethod]
        public void TestAll() {
            List<CarrierType> types = CarrierType.All();
            Assert.AreNotEqual(0, types.Count);
        }
    }
}