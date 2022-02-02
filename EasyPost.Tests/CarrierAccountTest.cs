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
        public void TestRetrieve()
        {
            CarrierAccount account = CarrierAccount.Retrieve("ca_7642d249fdcf47bcb5da9ea34c96dfcf");
            Assert.AreEqual("ca_7642d249fdcf47bcb5da9ea34c96dfcf", account.id);
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            List<CarrierAccount> accounts = CarrierAccount.All();
            Assert.IsNotNull(accounts);
            if (accounts.Count > 0)
            {
                Assert.IsNotNull(accounts[0].id);
                Assert.AreEqual(accounts[0].id.Substring(0, 3), "ca_");
            }
        }
    }
}
