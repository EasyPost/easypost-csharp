using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class EventTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("event");
        }

        private static async Task<EventCollection> GetBasicEventCollection(V2Client v2Client)
        {
            return await v2Client.Events.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client v2Client = _vcr.SetUpTest("all");

            EventCollection eventCollection = await GetBasicEventCollection(v2Client);

            List<Event> events = eventCollection.events;

            Assert.IsTrue(events.Count <= Fixture.PageSize);
            Assert.IsNotNull(eventCollection.has_more);
            foreach (var item in events)
            {
                Assert.IsInstanceOfType(item, typeof(Event));
            }
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            EventCollection eventCollection = await GetBasicEventCollection(v2Client);
            Event _event = eventCollection.events[0];

            Event retrievedEvent = await v2Client.Events.Retrieve(_event.id);

            Assert.IsInstanceOfType(retrievedEvent, typeof(Event));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(_event.id, retrievedEvent.id);
        }
    }
}
