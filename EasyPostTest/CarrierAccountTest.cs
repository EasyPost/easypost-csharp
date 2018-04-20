﻿using EasyPost;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class CarrierAccountTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");
        }

        [TestMethod]
        public void TestRetrieve() {
            CarrierAccount account = CarrierAccount.Retrieve("ca_7642d249fdcf47bcb5da9ea34c96dfcf");
            Assert.AreEqual("ca_7642d249fdcf47bcb5da9ea34c96dfcf", account.id);
        }

        [TestMethod]
        public void TestCRUD() {
            CarrierAccount account = CarrierAccount.Create(new Dictionary<string, object>() {
                {"type", "UspsAccount"},
                {"description", "description"}
            });

            Assert.IsNotNull(account.id);
            Assert.AreEqual(account.type, "UspsAccount");

            account.Update(new Dictionary<string, object>() { { "reference", "new-reference" } });
            Assert.AreEqual("new-reference", account.reference);

            account.Destroy();
            try {
                CarrierAccount.Retrieve(account.id);
                Assert.Fail();
            }
            catch (HttpException) { }
        }

        [TestMethod]
        public void TestList() {
            List<CarrierAccount> accounts = CarrierAccount.List();
            Assert.AreEqual(accounts[0].id, "ca_7642d249fdcf47bcb5da9ea34c96dfcf");
        }
    }
}