// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using EasyPost;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class WebhookTest
    {
        private string _webhookId = null;

        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "webhook", true);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            if (_webhookId != null)
            {
                try
                {
                    Webhook retrievedWebhook = await Webhook.Retrieve(_webhookId);
                    await retrievedWebhook.Delete();
                    _webhookId = null;
                }
                catch
                {
                }
            }
        }

        private static async Task<Webhook> CreateBasicWebhook(string url)
        {
            return await Webhook.Create(new Dictionary<string, object>
            {
                {
                    "url", url
                }
            });
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl);

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(Fixture.WebhookUrl, webhook.url);

            _webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl);

            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);

            _webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            List<Webhook> webhooks = await Webhook.All();

            foreach (var item in webhooks)
            {
                Assert.IsInstanceOfType(item, typeof(Webhook));
            }
        }

        // Cannot be easily tested - requires a disabled webhook
        [Ignore]
        [TestMethod]
        public async Task TestUpdate()
        {
            VCR.Replay("update");

            Webhook webhook = await Webhook.Retrieve("123...");

            await webhook.Update();
        }

        [TestMethod]
        public async Task TestDelete()
        {
            VCR.Replay("delete");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl);
            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();

            // This endpoint/method does not return anything, just make sure the request doesn't fail
            Assert.IsTrue(success);
        }
    }
}
