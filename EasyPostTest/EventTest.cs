using EasyPost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class EventTest {
        [TestMethod]
        public void TestLoad() {
            Assert.AreEqual(Resource.Load<Event>("{'id': 'barfoo'}").id, "barfoo");
        }
    }
}
