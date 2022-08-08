using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class WebhookTest : UnitTest
    {
        // NOTE: Because the API does not allow two webhooks with the same URL,
        // and these tests run in parallel, each test needs to have a unique URL.

        public WebhookTest() : base("webhook") =>
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

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            List<Webhook> webhooks = await Client.Webhook.All();

            foreach (Webhook item in webhooks)
            {
                Assert.IsType<Webhook>(item);
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            const string url = "https://example.com/create";

            Webhook webhook = await CreateBasicWebhook(url);

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.id);
            Assert.Equal(url, webhook.url);
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete");

            const string url = "https://example.com/delete";

            Webhook webhook = await CreateBasicWebhook(url);
            Webhook retrievedWebhook = await Client.Webhook.Retrieve(webhook.id);

            await retrievedWebhook.Delete();

            // TODO: Assert something

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            const string url = "https://example.com/retrieve";

            Webhook webhook = await CreateBasicWebhook(url);

            Webhook retrievedWebhook = await Client.Webhook.Retrieve(webhook.id);

            Assert.IsType<Webhook>(retrievedWebhook);
            Assert.Equal(webhook, retrievedWebhook);
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update");

            const string url = "https://example.com/update";

            Webhook webhook = await CreateBasicWebhook(url);

            webhook = await webhook.Update();

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.id);
        }

        private async Task<Webhook> CreateBasicWebhook(string url)
        {
            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object?>
            {
                {
                    "url", url
                }
            });
            CleanUpAfterTest(webhook.id);

            return webhook;
        }
    }
}
