using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class AddressServiceTests : UnitTest
    {
        public AddressServiceTests() : base("address_service_with_parameters")
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

            Dictionary<string, object> fixture = Fixtures.CaAddress1;

            Parameters.Address.Create parameters = Fixtures.Parameters.Addresses.Create(fixture);

            Address address = await Client.Address.Create(parameters);

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

            Dictionary<string, object> fixture = Fixtures.IncorrectAddress;
            fixture["verify"] = true;

            Parameters.Address.Create parameters = Fixtures.Parameters.Addresses.Create(fixture);

            Address address = await Client.Address.Create(parameters);

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

            Dictionary<string, object> fixture = Fixtures.CaAddress1;
            fixture["verify_strict"] = true;

            Parameters.Address.Create parameters = Fixtures.Parameters.Addresses.Create(fixture);

            Address address = await Client.Address.Create(parameters);

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

            Dictionary<string, object> fixture = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            Parameters.Address.All parameters = Fixtures.Parameters.Addresses.All(fixture);

            AddressCollection addressCollection = await Client.Address.All(parameters);
            List<Address> addresses = addressCollection.Addresses;

            Assert.True(addresses.Count <= Fixtures.PageSize);
            foreach (Address item in addresses)
            {
                Assert.IsType<Address>(item);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestCreateAndVerify()
        {
            UseVCR("create_and_verify");

            Dictionary<string, object> fixture = Fixtures.CaAddress1;

            Parameters.Address.Create parameters = Fixtures.Parameters.Addresses.Create(fixture);

            Address address = await Client.Address.CreateAndVerify(parameters);

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        #endregion

        #endregion
    }
}
