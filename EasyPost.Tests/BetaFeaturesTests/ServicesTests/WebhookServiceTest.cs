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

        public WebhookServiceTests() : base("webhook_service_with_parameters") =>
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
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            string url = $"https://example.com/beta/create/{TestUtils.NetVersion}";

            Dictionary<string, object> data = new Dictionary<string, object> { { "url", url } };

            BetaFeatures.Parameters.Webhooks.Create parameters = Fixtures.Parameters.Webhooks.Create(data);

            Webhook webhook = await Client.Webhook.Create(parameters);
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

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            string url = $"https://example.com/beta/update/{TestUtils.NetVersion}";
            BetaFeatures.Parameters.Webhooks.Create webhookParameters = new()
            {
                Url = url,
            };
            Webhook webhook = await Client.Webhook.Create(webhookParameters);
            CleanUpAfterTest(webhook.Id);

            BetaFeatures.Parameters.Webhooks.Update updateParameters = new()
            {
            };
            // Sending an empty payload will toggle the active status of the webhook silently.
            webhook = await Client.Webhook.Update(webhook.Id, updateParameters);

            // We can only update the secret, but that's not returned as part of the Webhook object, so we have no property to check to see if it was updated.
            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
        }

        #endregion

        #endregion
    }
}
