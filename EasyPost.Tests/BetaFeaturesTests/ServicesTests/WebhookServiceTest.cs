using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class WebhookServiceTests : UnitTest
    {
        // NOTE: Because the API does not allow two webhooks with the same URL,
        // and these tests run in parallel, each test needs to have a unique URL.

        public WebhookServiceTests() : base("webhook_service") =>
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
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            const string url = "https://example.com/create";

            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
            Assert.Equal(url, webhook.Url);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Webhooks.All parameters = Fixtures.Parameters.Webhooks.All(data);

            List<Webhook> webhooks = await Client.Webhook.All(parameters);

            foreach (Webhook item in webhooks)
            {
                Assert.IsType<Webhook>(item);
            }
        }

        #endregion

        #endregion
    }
}
