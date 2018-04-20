using EasyPost;

using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPostTest
{
    [TestClass]
    public class ErrorTest
    {
        string  error = "{\"error\":{\"code\":\"ADDRESS.VERIFY.FAILURE\",\"message\":\"Unable to verify address.\",\"errors\":[{\"code\":\"E.ADDRESS.NOT_FOUND\",\"field\":\"address\",\"suggestion\":null,\"message\":\"Address not found\"}]}}";
        private Address address;

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
            address = new Address()
            {
                company = "Simpler Postage Inc",
                street1 = "1645456 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107"
            };
        }

        [TestMethod]
        public void TestErrorLoad()
        {
            var sut = Error.Load<Error>(error);
            Assert.IsNotNull(sut);
            Assert.AreEqual("ADDRESS.VERIFY.FAILURE", sut.code);
            Assert.AreEqual("Unable to verify address.", sut.message);
            Assert.AreEqual("E.ADDRESS.NOT_FOUND", sut.errors[0].code);
            Assert.AreEqual("address", sut.errors[0].field);
            Assert.AreEqual("Address not found", sut.errors[0].message);
        }

        [TestMethod]
        public void TestAddressError()
        {
            address.Create();
            HttpException ex = null;
            try {
                address.Verify();
            }
            catch (HttpException hex)
            {
                ex = hex;
            }

            var sut = Error.Load<Error>(ex.Message);
            Assert.IsNotNull(sut);
            Assert.AreEqual("ADDRESS.VERIFY.FAILURE", sut.code);
            Assert.AreEqual("Unable to verify address.", sut.message);
            Assert.AreEqual("E.ADDRESS.NOT_FOUND", sut.errors[0].code);
            Assert.AreEqual("address", sut.errors[0].field);
            Assert.AreEqual("Address not found", sut.errors[0].message);
        }
    }
}
