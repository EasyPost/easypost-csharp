using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

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

            Assert.True(events.Count <= Fixtures.PageSize);
            foreach (Event item in events)
            {
                Assert.IsType<Event>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            EventCollection collection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                EventCollection nextPageCollection = await Client.Event.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Events[0].Id, nextPageCollection.Events[0].Id);
            }
            catch (EndOfPaginationError e) // There's no second page, that's not a failure
            {
                CustomAssertions.Pass();
            }
            catch // Any other exception is a failure
            {
                Assert.Fail("Failed to get next page");
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
        public async Task TestRetrieveAllPayloads()
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

            EventCollection eventCollection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            Event @event = eventCollection.Events[0];

            List<Payload> payloads = await Client.Event.RetrieveAllPayloads(@event.Id);

            Assert.IsType<List<Payload>>(payloads);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrievePayload()
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

            EventCollection eventCollection = await Client.Event.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            Event @event = eventCollection.Events[0];

            // Payload does not exist due to queueing, so this will throw an exception
            await Assert.ThrowsAsync<NotFoundError>(async () => await Client.Event.RetrievePayload(@event.Id, "payload_11111111111111111111111111111111"));
        }

        #endregion

        #endregion
    }
}
