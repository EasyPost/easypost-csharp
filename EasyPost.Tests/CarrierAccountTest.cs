using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class CarrierAccountTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Production, "carrier_account", true);
        }

        private static CarrierAccount CreateBasicCarrierAccount()
        {
            return CarrierAccount.Create(Fixture.BasicCarrierAccount);
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");


            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = CarrierAccount.Retrieve(carrierAccount.id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount.id, retrievedCarrierAccount.id);
        }

        [TestMethod]
        public void TestAll()
        {
            VCR.Replay("all");

            List<CarrierAccount> carrierAccounts = CarrierAccount.All();

            foreach (var item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            VCR.Replay("update");


            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            string testDescription = "my custom description";

            Dictionary<string, object> carrierAccountData = new Dictionary<string, object>
            {
                {
                    "description", testDescription
                }
            };
            carrierAccount.Update(carrierAccountData);

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
            Assert.AreEqual(testDescription, carrierAccount.description);
        }

        [TestMethod]
        public void TestDelete()
        {
            VCR.Replay("delete");


            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            bool success = carrierAccount.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestTypes()
        {
            VCR.Replay("types");

            List<CarrierType> types = CarrierType.All();

            foreach (var item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }
    }
}
