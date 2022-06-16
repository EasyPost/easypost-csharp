using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
                    Webhook retrievedWebhook = await Client.Webhooks.Retrieve(id);
                    return await retrievedWebhook.Delete();
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
            UseVCR("all", ApiVersion.V2);

            List<Webhook> webhooks = await Client.Webhooks.All();

            foreach (Webhook item in webhooks)
            {
                Assert.IsInstanceOfType(item, typeof(Webhook));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.V2);

            const string url = "https://example.com/create";

            Webhook webhook = await CreateBasicWebhook(url);

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.Id.StartsWith("hook_"));
            Assert.AreEqual(url, webhook.Url);
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete", ApiVersion.V2);

            const string url = "https://example.com/delete";

            Webhook webhook = await CreateBasicWebhook(url);
            Webhook retrievedWebhook = await Client.Webhooks.Retrieve(webhook.Id);

            bool success = await retrievedWebhook.Delete();
            Assert.IsTrue(success);

            SkipCleanUpAfterTest();
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            const string url = "https://example.com/retrieve";

            Webhook webhook = await CreateBasicWebhook(url);

            Webhook retrievedWebhook = await Client.Webhooks.Retrieve(webhook.Id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update", ApiVersion.V2);

            const string url = "https://example.com/update";

            Webhook webhook = await CreateBasicWebhook(url);

            await webhook.Update();
            // TODO: We should call this something more intuitive in the future, since it doesn't work like the other Update function
        }

        private async Task<Webhook> CreateBasicWebhook(string url)
        {
            Webhook webhook = await Client.Webhooks.Create(new Dictionary<string, object>
            {
                {
                    "url", url
                }
            });
            CleanUpAfterTest(webhook.Id);

            return webhook;
        }
    }
}
