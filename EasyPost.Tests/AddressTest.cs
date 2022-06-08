using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class AddressTest : UnitTest
    {
        public AddressTest() : base("address")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.V2);

            AddressCollection addressCollection = await Client.Addresses.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Address> addresses = addressCollection.addresses;

            Assert.IsTrue(addresses.Count <= Fixture.PageSize);
            Assert.IsNotNull(addressCollection.has_more);
            foreach (Address item in addresses)
            {
                Assert.IsInstanceOfType(item, typeof(Address));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.V2);

            Address address = await CreateBasicAddress();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 Townsend St", address.street1);
        }

        [Fact]
        public async Task TestCreateAndVerify()
        {
            UseVCR("create_and_verify", ApiVersion.V2);

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Client.Addresses.CreateAndVerify(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }

        [Fact]
        public async Task TestCreateVerify()
        {
            UseVCR("create_verify", ApiVersion.V2);

            Address address = await Client.Addresses.Create(Fixture.IncorrectAddressToVerify);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("417 MONTGOMERY ST FL 5", address.street1);
        }

        [Fact]
        public async Task TestCreateVerifyStrict()
        {
            UseVCR("create_verify_strict", ApiVersion.V2);

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Client.Addresses.Create(addressData);

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }


        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);


            Address address = await Client.Addresses.Create(Fixture.BasicAddress);

            Address retrievedAddress = await Client.Addresses.Retrieve(address.id);

            Assert.IsInstanceOfType(retrievedAddress, typeof(Address));
            Assert.AreEqual(address, retrievedAddress);
        }

        [Fact]
        public async Task TestVerify()
        {
            UseVCR("verify", ApiVersion.V2);


            Address address = await CreateBasicAddress();

            await address.Verify();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.street1);
        }

        private async Task<Address> CreateBasicAddress() => await Client.Addresses.Create(Fixture.BasicAddress);
    }
}
