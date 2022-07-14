using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
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
            UseVCR("all", ApiVersion.Latest);

            AddressCollection addressCollection = await Client.Addresses.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<Address> addresses = addressCollection.Addresses;

            Assert.IsTrue(addresses.Count <= Fixture.PageSize);
            Assert.IsNotNull(addressCollection.HasMore);
            foreach (Address item in addresses)
            {
                Assert.IsInstanceOfType(item, typeof(Address));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Address address = await CreateBasicAddress();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.Id.StartsWith("adr_"));
            Assert.AreEqual("388 Townsend St", address.Street1);
        }

        [Fact]
        public async Task TestCreateAndVerify()
        {
            UseVCR("create_and_verify", ApiVersion.Latest);

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Client.Addresses.CreateAndVerify(new Addresses.Create(addressData));

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.Id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.Street1);
        }

        [Fact]
        public async Task TestCreateVerify()
        {
            UseVCR("create_verify", ApiVersion.Latest);

            Address address = await Client.Addresses.Create(new Addresses.Create(Fixture.IncorrectAddressToVerify));

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.Id.StartsWith("adr_"));
            Assert.AreEqual("417 MONTGOMERY ST FL 5", address.Street1);
        }

        [Fact]
        public async Task TestCreateVerifyStrict()
        {
            UseVCR("create_verify_strict", ApiVersion.Latest);

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Client.Addresses.Create(new Addresses.Create(addressData));

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.Id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.Street1);
        }


        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);


            Address address = await Client.Addresses.Create(new Addresses.Create(Fixture.BasicAddress));

            Address retrievedAddress = await Client.Addresses.Retrieve(address.Id);

            Assert.IsInstanceOfType(retrievedAddress, typeof(Address));
            Assert.AreEqual(address, retrievedAddress);
        }

        [Fact]
        public async Task TestVerify()
        {
            UseVCR("verify", ApiVersion.Latest);


            Address address = await CreateBasicAddress();

            address = await address.Verify();

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(address.Id.StartsWith("adr_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", address.Street1);
        }

        private async Task<Address> CreateBasicAddress() => await Client.Addresses.Create(new Addresses.Create(new Dictionary<string, object>
        {
            {
                "address", Fixture.BasicAddress
            }
        }));
    }
}
