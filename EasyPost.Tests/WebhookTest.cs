// using System.Collections.Generic;
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
        private TestUtils.VCR _vcr;
        private string _webhookId = null;

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

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("webhook");

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            List<Webhook> webhooks = await Webhook.All();

            foreach (Webhook item in webhooks)
            {
                Assert.IsInstanceOfType(item, typeof(Webhook));
            }
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl);

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(Fixture.WebhookUrl, webhook.url);

            _webhookId = webhook.id; // trigger deletion
        }

        [TestMethod]
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl);
            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();

            // This endpoint/method does not return anything, just make sure the request doesn't fail
            Assert.IsTrue(success);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            Webhook webhook = await CreateBasicWebhook(Fixture.WebhookUrl);

            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);

            _webhookId = webhook.id; // trigger deletion
        }

        // Cannot be easily tested - requires a disabled webhook
        [Ignore]
        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");

            Webhook webhook = await Webhook.Retrieve("123...");

            await webhook.Update();
        }

        private static async Task<Webhook> CreateBasicWebhook(string url) =>
            await Webhook.Create(new Dictionary<string, object>
            {
                {
                    "url", url
                }
            });
    }
}
