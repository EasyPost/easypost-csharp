using EasyPost;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class EventTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestLoad() {
            Assert.AreEqual(Resource.Load<Event>("{'id': 'barfoo'}").id, "barfoo");
        }
            
        [TestMethod]
        public void TestRetrieve() {
            Event e = Event.Retrieve("evt_8ff440c1bcef40c6a825171d190c3bdb");
            Assert.AreEqual(e.result["id"], "sf_8b4c8eb46fa0459e9e7be9f2e784da03");
        }
    }
}
