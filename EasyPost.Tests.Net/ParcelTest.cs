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

        private static Parcel CreateBasicParcel()
        {
            return Parcel.Create(Fixture.BasicParcel);
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            Parcel parcel = CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.weight);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");


            Parcel parcel = CreateBasicParcel();

            Parcel retrievedParcel = Parcel.Retrieve(parcel.id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel.id, retrievedParcel.id);
        }
    }
}
