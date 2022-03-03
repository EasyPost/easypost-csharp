using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class ParcelTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        private static Parcel CreateBasicParcel()
        {
            return Parcel.Create(Fixture.BasicParcel);
        }

        [TestMethod]
        public void TestCreate()
        {
            Parcel parcel = CreateBasicParcel();

            Assert.IsInstanceOfType(parcel, typeof(Parcel));
            Assert.IsTrue(parcel.id.StartsWith("prcl_"));
            Assert.AreEqual(15.4, parcel.weight);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            Parcel parcel = CreateBasicParcel();

            Parcel retrievedParcel = Parcel.Retrieve(parcel.id);

            Assert.IsInstanceOfType(retrievedParcel, typeof(Parcel));
            Assert.AreEqual(parcel.id, retrievedParcel.id);
        }
    }
}
