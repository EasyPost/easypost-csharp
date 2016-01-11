using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ParcelTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
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
            Parcel parcel = new Parcel() { weight = 1.8, predefined_package = "SMALLFLATRATEBOX" };
            Shipment shipment = new Shipment() { parcel = parcel };
            shipment.Create();

            Assert.AreEqual(null, shipment.parcel.height);
            Assert.AreEqual("SMALLFLATRATEBOX", shipment.parcel.predefined_package);
        }
    }
}
