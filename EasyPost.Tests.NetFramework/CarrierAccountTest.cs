using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class CarrierAccountTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Production);
        }

        private static CarrierAccount CreateBasicCarrierAccount()
        {
            return CarrierAccount.Create(Fixture.BasicCarrierAccount);
        }

        [TestMethod]
        public void TestCreate()
        {
            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            Assert.IsInstanceOfType(carrierAccount, typeof(CarrierAccount));
            Assert.IsTrue(carrierAccount.id.StartsWith("ca_"));
        }

        [TestMethod]
        public void TestRetrieve()
        {
            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            CarrierAccount retrievedCarrierAccount = CarrierAccount.Retrieve(carrierAccount.id);

            Assert.IsInstanceOfType(retrievedCarrierAccount, typeof(CarrierAccount));
            Assert.AreEqual(carrierAccount.id, retrievedCarrierAccount.id);
        }

        [TestMethod]
        public void TestAll()
        {
            List<CarrierAccount> carrierAccounts = CarrierAccount.All();

            foreach (var item in carrierAccounts)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierAccount));
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
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
            CarrierAccount carrierAccount = CreateBasicCarrierAccount();

            bool success = carrierAccount.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestTypes()
        {
            List<CarrierType> types = CarrierType.All();

            foreach (var item in types)
            {
                Assert.IsInstanceOfType(item, typeof(CarrierType));
            }
        }
    }
}
