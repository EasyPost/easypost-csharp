using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class AddressTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        private static Address CreateBasicAddress()
        {
            return Address.Create(Fixture.BasicAddress);
        }

        [TestMethod]
        public void TestCreate()
        {
            Address address = CreateBasicAddress();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 Townsend St", address.street1);
        }

        [TestMethod]
        public void TestCreateVerifyStrict()
        {
            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = Address.Create(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }


        [TestMethod]
        public void TestRetrieve()
        {
            Address address = Address.Create(Fixture.BasicAddress);

            Address retrievedAddress = Address.Retrieve(address.id);

            Assert.IsInstanceOfType(retrievedAddress, typeof(Address));
            Assert.AreEqual(address.id, retrievedAddress.id);
        }

        [TestMethod]
        public void TestAll()
        {
            AddressCollection addressCollection = Address.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Address> addresses = addressCollection.addresses;

            Assert.IsTrue(addresses.Count <= Fixture.PageSize);
            Assert.IsNotNull(addressCollection.has_more);
            foreach (var item in addresses)
            {
                Assert.IsInstanceOfType(item, typeof(Address));
            }
        }

        [TestMethod]
        public void TestCreateVerify()
        {
            Address address = Address.Create(Fixture.IncorrectAddressToVerify);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("417 MONTGOMERY ST STE 500", address.street1);
        }

        [TestMethod]
        public void TestCreateAndVerify()
        {
            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = Address.CreateAndVerify(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }

        [TestMethod]
        public void TestVerify()
        {
            Address address = CreateBasicAddress();

            address.Verify();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }
    }
}
