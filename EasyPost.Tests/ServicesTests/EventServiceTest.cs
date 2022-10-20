using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class EventServiceTests : UnitTest
    {
        public EventServiceTests() : base("event_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            EventCollection eventCollection = await GetBasicEventCollection();

            List<Event> events = eventCollection.Events;

            Assert.True(eventCollection.HasMore);
            Assert.True(events.Count <= Fixtures.PageSize);
            foreach (Event item in events)
            {
                Assert.IsType<Event>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            EventCollection eventCollection = await GetBasicEventCollection();
            Event @event = eventCollection.Events[0];

            Event retrievedEvent = await Client.Event.Retrieve(@event.Id);

            Assert.IsType<Event>(retrievedEvent);
            // Must compare IDs because other elements of objects may be different
            Assert.Equal(@event.Id, retrievedEvent.Id);
        }

        #endregion

        #endregion

        private async Task<EventCollection> GetBasicEventCollection() =>
            await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
    }
}
