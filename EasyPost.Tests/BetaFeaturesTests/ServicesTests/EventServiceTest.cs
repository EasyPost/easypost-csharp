using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class EventServiceTests : UnitTest
    {
        public EventServiceTests() : base("event_service_with_parameters") =>
            CleanupFunction = async id =>
            {
                try
                {
                    Webhook retrievedWebhook = await Client.Webhook.Retrieve(id);
                    await Client.Webhook.Delete(retrievedWebhook.Id);
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

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Events.All parameters = Fixtures.Parameters.Events.All(data);

            EventCollection eventCollection = await Client.Event.All(parameters);

            List<Event> events = eventCollection.Events;

            Assert.True(events.Count <= Fixtures.PageSize);
            foreach (Event item in events)
            {
                Assert.IsType<Event>(item);
            }
        }

        #endregion

        #endregion
    }
}
