using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class EventTest : UnitTest
    {
        public EventTest() : base("event")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            EventCollection eventCollection = await GetBasicEventCollection();

            List<Event> events = eventCollection.Events;

            Assert.IsTrue(events.Count <= Fixture.PageSize);
            Assert.IsNotNull(eventCollection.HasMore);
            foreach (Event item in events)
            {
                Assert.IsInstanceOfType(item, typeof(Event));
            }
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            EventCollection eventCollection = await GetBasicEventCollection();
            Event _event = eventCollection.Events[0];

            Event retrievedEvent = await Client.Events.Retrieve(_event.Id);

            Assert.IsInstanceOfType(retrievedEvent, typeof(Event));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(_event.Id, retrievedEvent.Id);
        }

        private async Task<EventCollection> GetBasicEventCollection() =>
            await Client.Events.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
    }
}
