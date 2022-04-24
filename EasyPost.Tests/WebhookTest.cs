﻿// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using EasyPost;

using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class WebhookTest
    {
        private string _webhookId = null;

        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("webhook");
        }

        [TestCleanup]
        public async Task Cleanup(Client client)
        {
            if (_webhookId != null)
            {
                try
                {
                    Webhook retrievedWebhook = await client.Webhooks.Retrieve(_webhookId);
                    await retrievedWebhook.Delete();
                    _webhookId = null;
                }
                catch
                {
                }
            }
        }

        private static async Task<Webhook> CreateBasicWebhook(string url, Client client)
        {
            return await client.Webhooks.Create(new Dictionary<string, object>
            {
                {
                    "url", url
                }
            });
        }

        [TestMethod]
        public async Task TestCreate()
        {
            Client client = _vcr.SetUpTest("create");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl, client);

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(Fixture.WebhookUrl, webhook.url);

            _webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            Client client = _vcr.SetUpTest("retrieve");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl, client);

            Webhook retrievedWebhook = await client.Webhooks.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);

            _webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public async Task TestAll()
        {
            Client client = _vcr.SetUpTest("all");

            List<Webhook> webhooks = await client.Webhooks.All();

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
            Client client = _vcr.SetUpTest("update");

            Webhook webhook = await client.Webhooks.Retrieve("123...");

            await webhook.Update();
        }

        [TestMethod]
        public async Task TestDelete()
        {
            Client client = _vcr.SetUpTest("delete");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl, client);
            Webhook retrievedWebhook = await client.Webhooks.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();

            // This endpoint/method does not return anything, just make sure the request doesn't fail
            Assert.IsTrue(success);
        }
    }
}
