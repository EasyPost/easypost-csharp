using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EasyPost;

namespace EasyPostTest {
    [TestClass]
    public class SecurityTest {
        [TestMethod]
        public void TestGetProtocol() {
            Assert.AreEqual(SecurityProtocolType.Tls12, Security.GetProtocol());
        }
    }
}
