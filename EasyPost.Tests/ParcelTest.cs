using System.Threading.Tasks;
using EasyPost.Clients;
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
            UseVCR("create", ApiVersion.Latest);

            Parcel parcel = await Client.CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.Id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.Weight);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Parcel parcel = await Client.CreateBasicParcel();

            Parcel retrievedParcel = await Client.Parcels.Retrieve(parcel.Id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel, retrievedParcel);
        }
    }
}
