using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class EventTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
        }

        [TestMethod]
        public void TestLoad()
        {
            Assert.AreEqual(Resource.Load<Event>("{'id': 'barfoo'}").id, "barfoo");
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestRetrieve()
        {
            // Events are archived after some time. Lets at least make sure we get a 404.
            Event e = Event.Retrieve("evt_d0000c1a9c6c4614949af6931ea9fac8");
        }
    }
}