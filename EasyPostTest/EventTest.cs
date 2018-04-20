using EasyPost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class EventTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
        }

        [TestMethod]
        public void TestLoad() {
            Assert.AreEqual(Resource.Load<Event>("{'id': 'barfoo'}").id, "barfoo");
        }
            
        [TestMethod]
         public void TestRetrieve() {
            Event e = Event.Retrieve("evt_d0000c1a9c6c4614949af6931ea9fac8");
            Assert.AreEqual(e.result["id"], "shprep_ee1e22402e0b4331be620301b517186f");
        }
    }
}