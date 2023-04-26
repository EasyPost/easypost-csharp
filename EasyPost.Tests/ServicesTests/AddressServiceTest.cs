using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class AddressServiceTests : UnitTest
    {
        public AddressServiceTests() : base("address_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Address address = await Client.Address.Create(Fixtures.CaAddress1);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 Townsend St", address.Street1);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateVerify()
        {
            UseVCR("create_verify");

            Dictionary<string, object> addressData = Fixtures.IncorrectAddress;
            // internally, we're just checking for the presence of "verify" in the dictionary, so the value doesn't matter
            addressData.Add("verify", true);

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("417 MONTGOMERY ST FL 5", address.Street1);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateVerifyArray()
        {
            UseVCR("create_verify_array");

            Dictionary<string, object> addressData = Fixtures.IncorrectAddress;
            // internally, we're just checking for the presence of "verify" in the dictionary, so the value doesn't matter
            addressData.Add("verify", new List<bool> { true });

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("417 MONTGOMERY ST FL 5", address.Street1);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateVerifyStrict()
        {
            UseVCR("create_verify_strict");

            Dictionary<string, object> addressData = Fixtures.CaAddress1;
            // internally, we're just checking for the presence of "verify_strict" in the dictionary, so the value doesn't matter
            addressData.Add("verify_strict", true);

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateVerifyStrictArray()
        {
            UseVCR("create_verify_strict_array");

            Dictionary<string, object> addressData = Fixtures.CaAddress1;
            // internally, we're just checking for the presence of "verify_strict" in the dictionary, so the value doesn't matter
            addressData.Add("verify_strict", true);

            Address address = await Client.Address.Create(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            AddressCollection addressCollection = await Client.Address.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            List<Address> addresses = addressCollection.Addresses;

            Assert.True(addresses.Count <= Fixtures.PageSize);
            foreach (Address item in addresses)
            {
                Assert.IsType<Address>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            AddressCollection collection = await Client.Address.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                AddressCollection nextPageCollection = await Client.Address.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Addresses[0].Id, nextPageCollection.Addresses[0].Id);
            }
            catch (EndOfPaginationError e) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Address address = await Client.Address.Create(Fixtures.CaAddress1);

            Address retrievedAddress = await Client.Address.Retrieve(address.Id);

            Assert.IsType<Address>(retrievedAddress);
            Assert.Equal(address, retrievedAddress);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestCreateAndVerify()
        {
            UseVCR("create_and_verify");

            Dictionary<string, object> addressData = Fixtures.CaAddress1;

            Address address = await Client.Address.CreateAndVerify(addressData);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }
        
        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestVerify()
        {
            UseVCR("verify");

            Address address = await Client.Address.Create(Fixtures.CaAddress1);

            address = await Client.Address.Verify(address.Id);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        #endregion

        #endregion
    }
}
