using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ParcelTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("parcel");

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Parcel parcel = await CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.weight);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            Parcel parcel = await CreateBasicParcel();

            Parcel retrievedParcel = await Parcel.Retrieve(parcel.id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel, retrievedParcel);
        }

        private static async Task<Parcel> CreateBasicParcel() => await Parcel.Create(Fixture.BasicParcel);
    }
}
