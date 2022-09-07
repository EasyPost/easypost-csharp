using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class EventTest : UnitTest
    {
        public EventTest() : base("event")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            EventCollection eventCollection = await GetBasicEventCollection();

            List<Event> events = eventCollection.Events;

            Assert.True(eventCollection.HasMore);
            Assert.True(events.Count <= Fixture.PageSize);
            foreach (Event item in events)
            {
                Assert.IsType<Event>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            EventCollection eventCollection = await GetBasicEventCollection();
            Event _event = eventCollection.Events[0];

            Event retrievedEvent = await Client.Event.Retrieve(_event.Id);

            Assert.IsType<Event>(retrievedEvent);
            // Must compare IDs because other elements of objects may be different
            Assert.Equal(_event.Id, retrievedEvent.Id);
        }

        #endregion

        private async Task<EventCollection> GetBasicEventCollection() =>
            await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixture.PageSize } });
    }
}
