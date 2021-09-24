using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ParcelTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Parcel parcel = Parcel.Create(new Dictionary<string, object>() {
                {"length", 10}, {"width", 20}, {"height", 5}, {"weight", 1.8}
            });
            Parcel retrieved = Parcel.Retrieve(parcel.id);
            Assert.AreEqual(parcel.id, retrieved.id);
        }

        [TestMethod]
        public void TestPredefinedPackage() {
            Parcel parcel = Parcel.Create(new Dictionary<string, object>() {
                { "weight", 1.8 }, { "predefined_package", "SMALLFLATRATEBOX" }
            });

            Parcel retrieved = Parcel.Retrieve(parcel.id);

            Assert.AreEqual(null, retrieved.height);
            Assert.AreEqual("SMALLFLATRATEBOX", retrieved.predefined_package);
        }
    }
}
