using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ScanFormTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestScanFormList() {
            ScanFormList scanFormList = ScanForm.List();
            Assert.AreNotEqual(0, scanFormList.scanForms.Count);

            ScanFormList nextScanFormList = scanFormList.Next();
            Assert.AreNotEqual(scanFormList.scanForms[0].id, nextScanFormList.scanForms[0].id);
        }
    }
}
