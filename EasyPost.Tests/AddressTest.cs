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
            Assert.StartsWith("adr_", address.id);
            Assert.Equal("388 Townsend St", address.street1);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateVerify()
        {
            UseVCR("create_verify");

            Address address = await Client.Address.Create(Fixture.IncorrectAddressToVerify);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.id);
            Assert.Equal("417 MONTGOMERY ST FL 5", address.street1);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateVerifyStrict()
        {
            UseVCR("create_verify_strict");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.street1);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            AddressCollection addressCollection = await Client.Address.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
            Assert.True(addressCollection.has_more);
            List<Address> addresses = addressCollection.addresses;

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

            Address retrievedAddress = await Client.Address.Retrieve(address.id);

            Assert.IsType<Address>(retrievedAddress);
            Assert.Equal(address, retrievedAddress);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestCreateAndVerify()
        {
            UseVCR("create_and_verify");

            Dictionary<string, object> addressData = Fixture.BasicAddress;
            addressData.Add("verify_strict", new List<bool>
            {
                true
            });

            Address address = await Client.Address.CreateAndVerify(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.street1);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestVerify()
        {
            UseVCR("verify");


            Address address = await CreateBasicAddress();

            address = await address.Verify();

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.street1);
        }

        #endregion

        private async Task<Address> CreateBasicAddress() => await Client.Address.Create(Fixture.BasicAddress);
    }
}
