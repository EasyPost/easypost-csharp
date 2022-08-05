using System.Threading.Tasks;
using EasyPost.Models.API;
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
            UseVCR("create");

            Parcel parcel = await CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.weight);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Parcel parcel = await CreateBasicParcel();

            Parcel retrievedParcel = await Client.Parcel.Retrieve(parcel.id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel, retrievedParcel);
        }

        private async Task<Parcel> CreateBasicParcel() => await Client.Parcel.Create(Fixture.BasicParcel);
    }
}
