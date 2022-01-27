// <copyright file="AddressTest.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EasyPost.Tests
{
    [TestClass]
    public class AddressTest
    {
        private Address _address;

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
            _address = new Address()
            {
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
        public void TestRetrieveInvalidId()
        {
            Address.Retrieve("not-an-id");
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" }
            };
            var address = Address.Create(parameters);
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "Simpler Postage Inc");
            Assert.IsNull(address.name);

            var retrieved = Address.Retrieve(address.id);
            Assert.AreEqual(address.id, retrieved.id);
        }

        [TestMethod]
        public void TestCreateWithVerifications()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
                { "verifications", new List<string>() { "delivery", "zip4" } }
            };

            var address = Address.Create(parameters);
            Assert.IsNotNull(address.verifications.delivery);
            Assert.IsNotNull(address.verifications.zip4);

            parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "123 Fake Street" },
                { "zip", "94107" },
                { "verifications", new List<string>() { "delivery", "zip4" } }
            };

            address = Address.Create(parameters);
            Assert.AreEqual(address.verifications.delivery.success, false);
        }

        [TestMethod]
        public void TestInstanceCreateWithVerifications()
        {
            var address = new Address()
            {
                company = "Simpler Postage Inc",
                street1 = "164 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107",
                verify = new List<string>() { "delivery", "zip4" }
            };

            address.Create();
            Assert.IsNotNull(address.verifications.delivery);
            Assert.IsNotNull(address.verifications.zip4);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestCreateWithStrictVerifications()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "123 Fake Street" },
                { "zip", "94107" },
                { "strict_verifications", new List<string>() { "delivery", "zip4" } }
            };

            var address = Address.Create(parameters);
        }

        [TestMethod]
        public void TestCreateInstance()
        {
            _address.Create();
            Assert.IsNotNull(_address.id);
        }

        [TestMethod]
        public void TestInstanceCreateWithVerificationParams()
        {
            _address.Create(new List<string>() { "delivery", "zip4" });
            Assert.IsNotNull(_address.verifications.delivery);
            Assert.IsNotNull(_address.verifications.zip4);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestInstanceCreateWithStrictVerifications()
        {
            _address = new Address()
            {
                company = "Simpler Postage Inc"
            };
            _address.Create(strictVerifications: new List<string> { "delivery", "zip4" });
        }

        [TestMethod]
        public void TestVerify()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
                { "residential", true }
            };
            var address = Address.Create(parameters);
            address.Verify();
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "SIMPLER POSTAGE INC");
            Assert.IsNull(address.name);
            Assert.IsNotNull(address.residential);
            Assert.IsFalse(address.residential);
        }

        [TestMethod]
        public void TestVerifyCarrier()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
                { "residential", true }
            };
            var address = Address.Create(parameters);
            address.Verify("usps");
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "SIMPLER POSTAGE INC");
            Assert.AreEqual(address.street1, "164 TOWNSEND ST UNIT 1");
            Assert.IsNull(address.name);
        }

        [TestMethod]
        public void TestVerifyBeforeCreate()
        {
            _address.Verify();
            Assert.IsNotNull(_address.id);
        }

        [TestMethod]
        public void TestCreateAndVerify()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" }
            };
            var address = Address.CreateAndVerify(parameters);
            Assert.IsNotNull(address.id);
            Assert.AreEqual(address.company, "SIMPLER POSTAGE INC");
            Assert.IsNull(address.name);
            Assert.AreEqual(address.verifications.delivery.success, true);
        }

        [TestMethod]
        public void TestVerificationFailure()
        {
            var address = new Address()
            {
                company = "Simpler Postage Inc",
                street1 = "1645456 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107"
            };

            address.Create();

            try
            {
                address.Verify();
            }
            catch (HttpException e)
            {
                Assert.AreEqual("ADDRESS.VERIFY.FAILURE", e.Code);
                Assert.AreEqual("Unable to verify address.", e.Message);
                Assert.AreEqual("E.ADDRESS.NOT_FOUND", e.Errors[0].code);
                Assert.AreEqual("address", e.Errors[0].field);
                Assert.AreEqual("Address not found", e.Errors[0].message);
            }
        }
    }
}
