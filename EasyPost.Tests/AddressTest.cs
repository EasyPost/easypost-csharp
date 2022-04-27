using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class AddressTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("addresses");
        }

        private static async Task<Address> CreateBasicAddress(V2Client v2Client)
        {
            return await v2Client.Addresses.Create(Fixture.BasicAddress);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            Address address = await CreateBasicAddress(v2Client);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 Townsend St", address.street1);
        }

        [TestMethod]
        public async Task TestCreateVerifyStrict()
        {
            V2Client v2Client = _vcr.SetUpTest("create_verify_strict");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await v2Client.Addresses.Create(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }


        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");


            Address address = await v2Client.Addresses.Create(Fixture.BasicAddress);

            Address retrievedAddress = await v2Client.Addresses.Retrieve(address.id);

            Assert.IsInstanceOfType(retrievedAddress, typeof(Address));
            Assert.AreEqual(address, retrievedAddress);
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client v2Client = _vcr.SetUpTest("all");

            AddressCollection addressCollection = await v2Client.Addresses.All(new Dictionary<string, object>
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
            V2Client v2Client = _vcr.SetUpTest("create_verify");

            Address address = await v2Client.Addresses.Create(Fixture.IncorrectAddressToVerify);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("417 MONTGOMERY ST FL 5", address.street1);
        }

        [TestMethod]
        public async Task TestCreateAndVerify()
        {
            V2Client v2Client = _vcr.SetUpTest("create_and_verify");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await v2Client.Addresses.CreateAndVerify(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }

        [TestMethod]
        public async Task TestVerify()
        {
            V2Client v2Client = _vcr.SetUpTest("verify");


            Address address = await CreateBasicAddress(v2Client);

            await address.Verify();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }
    }
}
