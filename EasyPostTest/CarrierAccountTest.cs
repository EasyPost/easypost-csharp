using EasyPost;

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class CarrierAccountTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("VJ63zukvLyxz92NKP1k0EQ");
        }

        [TestMethod]
        public void TestRetrieve() {
            CarrierAccount account = CarrierAccount.Retrieve("ca_7c7X1XzO");
            Assert.AreEqual("ca_7c7X1XzO", account.id);
        }

        [TestMethod]
        public void TestCRUD() {
            CarrierAccount account = CarrierAccount.Create(new Dictionary<string, object>() {
                {"type", "EndiciaAccount"},
                {"description", "description"}
            });

            Assert.IsNotNull(account.id);

            account.Update(new Dictionary<string, object>() { { "reference", "new-reference" } });
            Assert.AreEqual("new-reference", account.reference);

            account.Destroy();
        }

        [TestMethod]
        public void TestList() {
            List<CarrierAccount> accounts = CarrierAccount.List();
            Assert.AreEqual(accounts[0].id, "ca_7c7X1XzO");
        }
    }
}