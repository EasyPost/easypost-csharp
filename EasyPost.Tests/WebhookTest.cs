#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class WebhookTest
    {
        private static string _easypostWebhookId = null;
        private static TestUtils.VCR _vcr;

        [TestCleanup]
        public async Task Cleanup()
        {
            if (_easypostWebhookId != null)
            {
                try
                {
                    Webhook retrievedWebhook = await Webhook.Retrieve(_easypostWebhookId);
                    await retrievedWebhook.Delete();
                    _easypostWebhookId = null;
                }
                catch
                {
                    // in case we try to delete something that's already been deleted
                }
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("webhook");
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
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Webhook webhook = await CreateBasicWebhook();

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(Fixture.WebhookUrl, webhook.url);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            Webhook webhook = await CreateBasicWebhook();
            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();
            Assert.IsTrue(success);

            _easypostWebhookId = null; // skip deletion cleanup
        }

        [TestMethod]
        public async Task TestEnableDisable()
        {
            _vcr.SetUpTest("update");

            Webhook webhook = await CreateBasicWebhook();

            await webhook.Update();
            // TODO: We should call this something more intuitive in the future, since it doesn't work like the other Update function
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
        public void TestValidate()
        {
            byte[] data = Fixture.EventBody;

            // ReSharper disable once StringLiteralTypo
            const string webhookSecret = "sécret";
            Dictionary<string, object?> headers = new Dictionary<string, object?>
            {
                {
                    "X-Hmac-Signature", "hmac-sha256-hex=e93977c8ccb20363d51a62b3fe1fc402b7829be1152da9e88cf9e8d07115a46b"
                }
            };

            Event @event = Webhook.ValidateWebhook(data, headers, webhookSecret);
            Assert.IsNotNull(@event);
        }

        private static async Task<Webhook> CreateBasicWebhook()
        {
            Webhook webhook = await Webhook.Create(new Dictionary<string, object>
            {
                {
                    "url", Fixture.WebhookUrl
                }
            });
            _easypostWebhookId = webhook.id; // trigger deletion after test
            return webhook;
        }
    }
}
