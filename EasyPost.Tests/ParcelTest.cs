using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
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
            UseVCR("create", ApiVersion.V2);

            Parcel parcel = await CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.weight);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            Parcel parcel = await CreateBasicParcel();

            Parcel retrievedParcel = await Client.Parcels.Retrieve(parcel.id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel, retrievedParcel);
        }

        private async Task<Parcel> CreateBasicParcel() => await Client.Parcels.Create(Fixture.BasicParcel);
    }
}
