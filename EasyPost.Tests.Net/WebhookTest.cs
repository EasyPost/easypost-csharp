// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class WebhookTest
    {
        private string webhookId = null;

        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "webhook", true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (webhookId != null)
            {
                try
                {
                    Webhook retrievedWebhook = Webhook.Retrieve(webhookId);
                    retrievedWebhook.Delete();
                    webhookId = null;
                }
                catch
                {
                }
            }
        }

        private static Webhook CreateBasicWebhook(string url)
        {
            return Webhook.Create(new Dictionary<string, object>
            {
                {
                    "url", url
                }
            });
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            string url = "https://testcreate.com";

            Webhook webhook = CreateBasicWebhook(url);

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(url, webhook.url);

            webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");

            string url = "https://testretrieve.com";


            Webhook webhook = CreateBasicWebhook(url);

            Webhook retrievedWebhook = Webhook.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook.id, retrievedWebhook.id);

            webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public void TestAll()
        {
            VCR.Replay("all");

            List<Webhook> webhooks = Webhook.All();

            foreach (var item in webhooks)
            {
                Assert.IsInstanceOfType(item, typeof(Webhook));
            }
        }

        // Cannot be easily tested - requires a disabled webhook
        [Ignore]
        [TestMethod]
        public void TestUpdate()
        {
            VCR.Replay("update");
        }

        [TestMethod]
        public void TestDelete()
        {
            VCR.Replay("delete");

            string url = "https://testdelete.com";


            Webhook webhook = CreateBasicWebhook(url);
            Webhook retrievedWebhook = Webhook.Retrieve(webhook.id);

            bool success = retrievedWebhook.Delete();

            // This endpoint/method does not return anything, just make sure the request doesn't fail
            Assert.IsTrue(success);
        }
    }
}
