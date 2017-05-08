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

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestErrorLoad()
        {
            //var str = JObject.Parse(s).SelectToken("error").ToString();
            //var sut = JsonConvert.DeserializeObject<Error>(s, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Default });
            var sut = Error.Load<Error>(error);
            Assert.IsNotNull(sut);
            Assert.AreEqual("ADDRESS.VERIFY.FAILURE", sut.code);
            Assert.AreEqual("Unable to verify address.", sut.message);
            Assert.AreEqual("E.ADDRESS.NOT_FOUND", sut.errors[0].code);
            Assert.AreEqual("address", sut.errors[0].field);
            Assert.AreEqual("Address not found", sut.errors[0].message);
        }
    }
}
