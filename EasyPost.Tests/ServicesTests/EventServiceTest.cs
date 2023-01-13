using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class EventServiceTests : UnitTest
    {
        public EventServiceTests() : base("event_service") =>
            CleanupFunction = async id =>
            {
                try
                {
                    Webhook retrievedWebhook = await Client.Webhook.Retrieve(id);
                    await retrievedWebhook.Delete();
                    return true;
                }
                catch
                {
                    // trying to delete something that doesn't exist, pass
                    return false;
                }
            };

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            EventCollection eventCollection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

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

            EventCollection eventCollection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            Event @event = eventCollection.Events[0];

            Event retrievedEvent = await Client.Event.Retrieve(@event.Id);

            Assert.IsType<Event>(retrievedEvent);
            // Must compare IDs because other elements of objects may be different
            Assert.Equal(@event.Id, retrievedEvent.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieveAllPayloadsForEvent()
        {
            UseVCR("retrieve_payloads_for_event");

            // Create a webhook to receive the event
            const string url = "https://example.com";
            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            // Create a batch to trigger an event
            Batch _ = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } });

            if (IsRecording())
            {
                await Task.Delay(5000); // Wait for the event to be created
            }

            // Retrieve all events and extract the newest one
            EventCollection eventCollection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            Event @event = eventCollection.Events[0];

            // Retrieve the payloads for the event
            List<Payload> payloads = await Client.Event.RetrieveAllPayloadsForEvent(@event);

            Assert.IsType<List<Payload>>(payloads);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrievePayloadForEvent()
        {
            UseVCR("retrieve_payload_for_event");

            // Create a webhook to receive the event
            const string url = "https://example.com";
            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            // Create a batch to trigger an event
            Batch _ = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } });

            if (IsRecording())
            {
                await Task.Delay(5000); // Wait for the event to be created
            }

            // Retrieve all events and extract the newest one
            EventCollection eventCollection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            Event @event = eventCollection.Events[0];

            // Retrieve a specific payload for the event
            // Payload does not exist due to queueing, so this will throw an exception
            // invalid payload should throw an exception
            await Assert.ThrowsAsync<NotFoundError>(async () => await Client.Event.RetrievePayloadForEvent(@event, "payload_11111111111111111111111111111111"));

            // Invalid payload ID length will throw a 500
            await Assert.ThrowsAsync<InternalServerError>(async () => await Client.Event.RetrievePayloadForEvent(@event, "payload_123"));
        }

        #endregion

        #endregion
    }
}
