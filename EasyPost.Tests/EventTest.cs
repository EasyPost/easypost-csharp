using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class EventTest : UnitTest
    {
        public EventTest() : base("event", TestUtils.ApiKey.Test)
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            EventCollection eventCollection = await GetBasicEventCollection();

            List<Event> events = eventCollection.events;

            Assert.IsTrue(events.Count <= Fixture.PageSize);
            Assert.IsNotNull(eventCollection.has_more);
            foreach (Event item in events)
            {
                Assert.IsInstanceOfType(item, typeof(Event));
            }
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            EventCollection eventCollection = await GetBasicEventCollection();
            Event _event = eventCollection.events[0];

            Event retrievedEvent = await V2Client.Events.Retrieve(_event.id);

            Assert.IsInstanceOfType(retrievedEvent, typeof(Event));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(_event.id, retrievedEvent.id);
        }

        private async Task<EventCollection> GetBasicEventCollection() =>
            await V2Client.Events.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
    }
}
