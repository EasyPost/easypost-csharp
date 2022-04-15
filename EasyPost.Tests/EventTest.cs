using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class EventTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "event", true);
        }

        private static async Task<EventCollection> GetBasicEventCollection()
        {
            return await Event.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            EventCollection eventCollection = await GetBasicEventCollection();

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
            VCR.Replay("retrieve");


            EventCollection eventCollection = await GetBasicEventCollection();
            Event _event = eventCollection.events[0];

            Event retrievedEvent = await Event.Retrieve(_event.id);

            Assert.IsInstanceOfType(retrievedEvent, typeof(Event));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(_event.id, retrievedEvent.id);
        }
    }
}
