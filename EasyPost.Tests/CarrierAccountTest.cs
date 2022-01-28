// CarrierAccountTest.cs
// See LICENSE for licensing info.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CarrierAccountTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");

        [Ignore]
        [TestMethod]
        public void TestCRUD()
        {
            CarrierAccount account = CarrierAccount.Create(new Dictionary<string, object>
            {
                {
                    "type", "DhlExpressAccount"
                },
                {
                    "description", "description"
                }
            });

            Assert.IsNotNull(account.id);
            Assert.AreEqual(account.type, "DhlExpressAccount");

            account.Update(new Dictionary<string, object>
            {
                {
                    "reference", "new-reference"
                }
            });
            Assert.AreEqual("new-reference", account.reference);

            account.Destroy();
            try
            {
                CarrierAccount.Retrieve(account.id);
                Assert.Fail();
            }
            catch (HttpException)
            {
            }
        }

        [TestMethod]
        public void TestList()
        {
            var accounts = CarrierAccount.List();
            Assert.AreEqual(accounts[0].id, "ca_7642d249fdcf47bcb5da9ea34c96dfcf");
        }

        [TestMethod]
        public void TestRetrieve()
        {
            var account = CarrierAccount.Retrieve("ca_7642d249fdcf47bcb5da9ea34c96dfcf");
            Assert.AreEqual("ca_7642d249fdcf47bcb5da9ea34c96dfcf", account.id);
        }
    }
}
