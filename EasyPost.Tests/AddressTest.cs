using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class AddressTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "address", true);
        }

        private static async Task<Address> CreateBasicAddress()
        {
            return await Address.Create(Fixture.BasicAddress);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Address address = await CreateBasicAddress();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 Townsend St", address.street1);
        }

        [TestMethod]
        public async Task TestCreateVerifyStrict()
        {
            VCR.Replay("create_verify_strict");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Address.Create(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }


        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            Address address = await Address.Create(Fixture.BasicAddress);

            Address retrievedAddress = await Address.Retrieve(address.id);

            Assert.IsInstanceOfType(retrievedAddress, typeof(Address));
            Assert.AreEqual(address, retrievedAddress);
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            AddressCollection addressCollection = await Address.All(new Dictionary<string, object>
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
        public async Task TestCreateVerify()
        {
            VCR.Replay("create_verify");

            Address address = await Address.Create(Fixture.IncorrectAddressToVerify);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("417 MONTGOMERY ST FL 5", address.street1);
        }

        [TestMethod]
        public async Task TestCreateAndVerify()
        {
            VCR.Replay("create_and_verify");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Address.CreateAndVerify(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }

        [TestMethod]
        public async Task TestVerify()
        {
            VCR.Replay("verify");


            Address address = await CreateBasicAddress();

            await address.Verify();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }
    }
}
