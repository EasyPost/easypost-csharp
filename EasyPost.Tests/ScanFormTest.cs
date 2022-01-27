﻿// ScanFormTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ScanFormTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

        [TestMethod]
        public void TestScanFormCreateAndRetrieve()
        {
            var scanForm = ScanForm.Retrieve("sf_e35ae7fc59bb4482ae32efc663267104");
            Assert.AreEqual("sf_e35ae7fc59bb4482ae32efc663267104", scanForm.id);
        }

        [TestMethod]
        public void TestScanFormList()
        {
            var dict = new Dictionary<string, object>
            {
                {
                    "page_size", 1
                }
            };
            var scanFormList = ScanForm.List(dict);
            Assert.AreNotEqual(null, scanFormList.scan_forms[0].batch_id);
            Assert.AreNotEqual(0, scanFormList.scan_forms.Count);
            var nextScanFormList = scanFormList.Next();
            Assert.AreNotEqual(scanFormList.scan_forms[0].id, nextScanFormList.scan_forms[0].id);
        }
    }
}
