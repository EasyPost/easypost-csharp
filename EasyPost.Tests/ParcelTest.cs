using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using EasyPost.Parameters.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class ParcelTest : UnitTest
    {
        public ParcelTest() : base("parcel")
        {
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Parcel parcel = await CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.Id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.Weight);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Parcel parcel = await CreateBasicParcel();

            Parcel retrievedParcel = await Client.Parcels.Retrieve(parcel.Id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel, retrievedParcel);
        }

        private async Task<Parcel> CreateBasicParcel() => await Client.Parcels.Create(new Parcels.Create(Fixture.BasicParcel));
    }
}
