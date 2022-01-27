// CustomsItemTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CustomsItemTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var item = CustomsItem.Create(new Dictionary<string, object>
            {
                { "description", "TShirt" },
                { "quantity", 1 },
                { "weight", 8 },
                { "value", 10.0 },
                { "currency", "USD" }
            });
            var retrieved = CustomsItem.Retrieve(item.id);
            Assert.AreEqual(item.id, retrieved.id);
            Assert.AreEqual(retrieved.value, 10.0);
            Assert.AreEqual(retrieved.currency, "USD");
        }
    }
}
