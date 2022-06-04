using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class WebhookTest : UnitTest
    {
        public WebhookTest() : base("webhook", TestUtils.ApiKey.Test)
        {
            CleanupFunction = async id =>
            {
                try
                {
                    Webhook retrievedWebhook = await V2Client.Webhooks.Retrieve(id);
                    return await retrievedWebhook.Delete();
                }
                catch
                {
                    // trying to delete something that doesn't exist, pass
                    return false;
                }
            };
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            Webhook webhook = await CreateBasicWebhook();

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(Fixture.WebhookUrl, webhook.url);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Webhook webhook = await CreateBasicWebhook();

            Webhook retrievedWebhook = await V2Client.Webhooks.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            List<Webhook> webhooks = await V2Client.Webhooks.All();

            foreach (var item in webhooks)
            {
                Assert.IsInstanceOfType(item, typeof(Webhook));
            }
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update");

            Webhook webhook = await CreateBasicWebhook();

            await webhook.Update();
            // TODO: We should call this something more intuitive in the future, since it doesn't work like the other Update function
        }

        [Fact]
        public async Task TestDelete()
        {
            UseVCR("delete");

            Webhook webhook = await CreateBasicWebhook();
            Webhook retrievedWebhook = await V2Client.Webhooks.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();
            Assert.IsTrue(success);

            SkipCleanUpAfterTest();
        }

        private async Task<Webhook> CreateBasicWebhook()
        {
            Webhook webhook = await V2Client.Webhooks.Create(new Dictionary<string, object>
            {
                {
                    "url", Fixture.WebhookUrl
                }
            });
            CleanUpAfterTest(webhook.id);

            return webhook;
        }

    }
}
