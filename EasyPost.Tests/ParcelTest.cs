using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class ParcelTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "parcel", true);
        }

        private static async Task<Parcel> CreateBasicParcel()
        {
            return await Parcel.Create(Fixture.BasicParcel);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Parcel parcel = await CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.weight);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            Parcel parcel = await CreateBasicParcel();

            Parcel retrievedParcel = await Parcel.Retrieve(parcel.id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel.id, retrievedParcel.id);
        }
    }
}
