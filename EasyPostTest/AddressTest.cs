using EasyPost;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EasyPostTest {
    [TestClass]
    public class AddressTest {
        Address address;

        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
            address = new Address() {
                company = "Simpler Postage Inc",
                street1 = "164 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestRetrieveInvalidId() {
            Address.Retrieve("not-an-id");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                {"city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" }
            };
            Address address = Address.Create(parameters);
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "Simpler Postage Inc");
            Assert.IsNull(address.name);

            Address retrieved = Address.Retrieve(address.id);
            Assert.AreEqual(address.id, retrieved.id);
        }

        [TestMethod]
        public void TestCreateWithVerifications() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                {"city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
                { "verifications", new List<string>() { "delivery", "zip4" } }
            };

            Address address = Address.Create(parameters);
            Assert.IsNotNull(address.verifications.delivery);
            Assert.IsNotNull(address.verifications.zip4);

            parameters = new Dictionary<string, object>() {
                { "company", "Simpler Postage Inc" },
                { "street1", "123 Fake Street" },
                { "zip", "94107" },
                { "verifications", new List<string>() { "delivery", "zip4" } }
            };

            address = Address.Create(parameters);
            Assert.AreEqual(address.verifications.delivery.success, false);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestCreateWithStrictVerifications() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                { "company", "Simpler Postage Inc" },
                { "street1", "123 Fake Street" },
                { "zip", "94107" },
                { "strict_verifications", new List<string>() { "delivery", "zip4" } }
            };

            Address address = Address.Create(parameters);
        }

        [TestMethod]
        public void TestCreateInstance() {
            address.Create();
            Assert.IsNotNull(address.id);
        }

        [TestMethod]
        public void TestInstanceCreateWithVerifications() {
            address.Create(new List<string>() { "delivery", "zip4" });
            Assert.IsNotNull(address.verifications.delivery);
            Assert.IsNotNull(address.verifications.zip4);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestInstanceCreateWithStrictVerifications() {
            address = new Address() {
                company = "Simpler Postage Inc"
            };
            address.Create(StrictVerifications: new List<string> { "delivery", "zip4" });
        }

        [TestMethod]
        public void TestVerify() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                {"city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
                { "residential", true }
            };
            Address address = Address.Create(parameters);
            address.Verify();
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "Simpler Postage Inc");
            Assert.IsNull(address.name);
            Assert.IsTrue(address.residential);
        }

        [TestMethod]
        public void TestVerifyCarrier() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                {"city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
                { "residential", true }
            };
            Address address = Address.Create(parameters);
            address.Verify("usps");
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "Simpler Postage Inc");
            Assert.AreEqual(address.street1, "164 TOWNSEND ST UNIT 1");
            Assert.IsNull(address.name);
        }

        [TestMethod]
        public void TestVerifyBeforeCreate() {
            address.Verify();
            Assert.IsNotNull(address.id);
        }

        [TestMethod]
        public void TestCreateAndVerify() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"}
            };
            Address address = Address.CreateAndVerify(parameters);
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "Simpler Postage Inc");
            Assert.IsNull(address.name);
        }
    }
}
