using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class WebhookTest
    {
        private static string _easypostWebhookId = null;
        private static TestUtils.VCR _vcr;

        private static WebhookTestService _webhookTestService;

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
            _webhookTestService = new WebhookTestService(_vcr);
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

            Webhook webhook = await CreateBasicWebhook(_webhookTestService);

            Assert.IsInstanceOfType(webhook, typeof(Webhook));
            Assert.IsTrue(webhook.id.StartsWith("hook_"));
            Assert.AreEqual(_webhookTestService.WebhookUrl, webhook.url);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            Webhook webhook = await CreateBasicWebhook(_webhookTestService);
            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            bool success = await retrievedWebhook.Delete();
            Assert.IsTrue(success);

            _easypostWebhookId = null; // skip deletion cleanup
        }

        [TestMethod]
        public async Task TestEnableDisable()
        {
            _vcr.SetUpTest("update");

            Webhook webhook = await CreateBasicWebhook(_webhookTestService);

            await webhook.Update();
            // TODO: We should call this something more intuitive in the future, since it doesn't work like the other Update function
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            Webhook webhook = await CreateBasicWebhook(_webhookTestService);

            Webhook retrievedWebhook = await Webhook.Retrieve(webhook.id);

            Assert.IsInstanceOfType(retrievedWebhook, typeof(Webhook));
            Assert.AreEqual(webhook, retrievedWebhook);
        }

        [Ignore] // webhooks can take a while to be triggered and sent to the webhook url, so we can't reliably test this
        [TestMethod]
        public async Task TestWebhookSecret()
        {
            _vcr.SetUpTest("webhook_secret");

            const string webhookField = "x-hmac-signature";

            Webhook webhook = await CreateBasicWebhook(_webhookTestService);

            // set the webhook secret
            await webhook.Update(new Dictionary<string, object>
            {
                {
                    "webhook_secret", "secret"
                }
            });

            // verify the webhook secret is set
            Assert.IsTrue(await TriggerAndVerifyWebhook(_webhookTestService, null, new List<string>
            {
                webhookField
            }));
        }

        private static async Task<Webhook> CreateBasicWebhook(WebhookTestService webhookTestService)
        {
            await webhookTestService.SetUp();
            Webhook webhook = await Webhook.Create(new Dictionary<string, object>
            {
                {
                    "url", webhookTestService.WebhookUrl
                }
            });
            _easypostWebhookId = webhook.id; // trigger deletion after test
            return webhook;
        }

        private static async Task<bool> TriggerAndVerifyWebhook(WebhookTestService testService, IEnumerable<string> presentBodyFields, IEnumerable<string> presentHeaderFields)
        {
            // reset the stored webhook data
            await testService.Reset();

            // trigger the webhook by buying a shipment in test mode
            Shipment shipment = await ShipmentTest.CreateFullShipment();
            await shipment.Buy(shipment.LowestRate());

            // wait for the webhook to be triggered
            if (_vcr.IsRecording())
            {
                Thread.Sleep(60000); // wait a whole minute for the webhook to be triggered
            }

            // verify the webhook data
            Dictionary<string, object> lastEvent = await testService.GetLatestEvent();
            if (lastEvent == null)
            {
                return false;
            }

            if (presentBodyFields != null)
            {
                if (presentBodyFields.Any(field => !lastEvent.ContainsKey(field)))
                {
                    return false;
                }
            }

            if (presentHeaderFields != null)
            {
                Dictionary<string, object> headers = WebhookTestService.GetHeadersForWebhook(lastEvent);
                if (presentHeaderFields.Any(field => headers.ContainsKey(field)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
