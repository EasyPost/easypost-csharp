﻿// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using EasyPost;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class WebhookTest
    {
        private static string _webhookId = null;

        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("webhook");
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
                    // in case we try to delete something that's already been deleted
                }
            }
        }

        private static async Task<Webhook> CreateBasicWebhook()
        {
            Webhook webhook = await Webhook.Create(new Dictionary<string, object>
            {
                {
                    "url", Fixture.WebhookUrl
                }
            });
            _webhookId = webhook.id;  // trigger deletion after test
            return webhook;
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Webhook webhook = await CreateBasicWebhook();

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(Fixture.WebhookUrl, webhook.url);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            Webhook webhook = await CreateBasicWebhook();

            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);
        }

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            List<Webhook> webhooks = await Webhook.All();

            foreach (var item in webhooks)
            {
                Assert.IsInstanceOfType(item, typeof(Webhook));
            }
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");

            Webhook webhook = await CreateBasicWebhook();

            await webhook.Update();
            // TODO: We should call this something more intuitive in the future, since it doesn't work like the other Update function
        }

        [TestMethod]
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            Webhook webhook = await CreateBasicWebhook();
            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();
            Assert.IsTrue(success);

            _webhookId = null; // skip deletion cleanup
        }
    }
}
