using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class AddressTest : UnitTest
    {
        public AddressTest() : base("address")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Address address = await CreateBasicAddress();

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 Townsend St", address.Street1);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateVerify()
        {
            UseVCR("create_verify");

            Dictionary<string, object> addressData = Fixture.IncorrectAddressToVerify;

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("417 MONTGOMERY ST FL 5", address.Street1);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateVerifyStrict()
        {
            UseVCR("create_verify_strict");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool> { true });

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            AddressCollection addressCollection = await Client.Address.All(new Dictionary<string, object> { { "page_size", Fixture.PageSize } });
            List<Address> addresses = addressCollection.Addresses;

            Assert.True(addressCollection.HasMore);
            Assert.True(addresses.Count <= Fixture.PageSize);
            foreach (Address item in addresses)
            {
                Assert.IsType<Address>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Address address = await Client.Address.Create(Fixture.BasicAddress);

            Address retrievedAddress = await Client.Address.Retrieve(address.Id);

            Assert.IsType<Address>(retrievedAddress);
            Assert.Equal(address, retrievedAddress);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestCreateAndVerify()
        {
            UseVCR("create_and_verify");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool> { true });

            Address address = await Client.Address.CreateAndVerify(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestVerify()
        {
            UseVCR("verify");

            Address address = await CreateBasicAddress();

            address = await address.Verify();

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        #endregion

        private async Task<Address> CreateBasicAddress() => await Client.Address.Create(Fixture.BasicAddress);
    }
}
