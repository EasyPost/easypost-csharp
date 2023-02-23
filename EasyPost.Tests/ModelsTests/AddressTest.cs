using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class AddressTests : UnitTest
    {
        public AddressTests() : base("address")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestVerify()
        {
            UseVCR("verify");

            Address address = await Client.Address.Create(Fixtures.CaAddress1);

            address = await address.Verify();

            Assert.IsType<Address>(address);
            Assert.StartsWith("adr_", address.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", address.Street1);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestVerifyWithNoId()
        {
            UseVCR("verify_with_no_id");

            Address address = await Client.Address.Create(Fixtures.CaAddress1);
            address.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await address.Verify());
        }

        #endregion

        #endregion
    }
}
