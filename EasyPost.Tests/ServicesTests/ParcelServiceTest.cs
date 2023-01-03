using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class ParcelServiceTests : UnitTest
    {
        public ParcelServiceTests() : base("parcel_service")
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

            Parcel parcel = await Client.Parcel.Create(Fixtures.BasicParcel);

            Assert.IsType<Parcel>(parcel);
            Assert.StartsWith("prcl_", parcel.Id);
            Assert.Equal(15.4, parcel.Weight);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Parcel parcel = await Client.Parcel.Create(Fixtures.BasicParcel);

            Parcel retrievedParcel = await Client.Parcel.Retrieve(parcel.Id);

            Assert.IsType<Parcel>(retrievedParcel);
            Assert.Equal(parcel, retrievedParcel);
        }

        #endregion

        #endregion
    }
}
