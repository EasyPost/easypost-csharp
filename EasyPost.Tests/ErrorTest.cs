using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ErrorTest
    {
        private string _error;

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            _error =
                "{\"code\":\"E.ADDRESS.NOT_FOUND\",\"field\":\"address\",\"suggestion\":\"foobar\",\"message\":\"Address not found\"}";
        }

        [TestMethod]
        public void TestErrorLoad()
        {
            Error e = Error.Load<Error>(_error);
            Assert.AreEqual("E.ADDRESS.NOT_FOUND", e.code);
            Assert.AreEqual("Address not found", e.message);
            Assert.AreEqual("address", e.field);
            Assert.AreEqual("foobar", e.suggestion);
        }
    }
}
