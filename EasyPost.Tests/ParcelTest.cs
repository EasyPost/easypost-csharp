using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class ParcelTest : UnitTest
    {
        public ParcelTest() : base("parcel")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Parcel parcel = await CreateBasicParcel();

            Assert.IsType<Parcel>(parcel);
            Assert.StartsWith("prcl_", parcel.Id);
            Assert.Equal(15.4, parcel.Weight);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Parcel parcel = await CreateBasicParcel();

            Parcel retrievedParcel = await Client.Parcel.Retrieve(parcel.Id);

            Assert.IsType<Parcel>(retrievedParcel);
            Assert.Equal(parcel, retrievedParcel);
        }

        #endregion

        private async Task<Parcel> CreateBasicParcel() => await Client.Parcel.Create(Fixture.BasicParcel);
    }
}
