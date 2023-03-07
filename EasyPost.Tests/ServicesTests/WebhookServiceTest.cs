using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class WebhookServiceTests : UnitTest
    {
        // NOTE: Because the API does not allow two webhooks with the same URL,
        // and these tests run in parallel, each test needs to have a unique URL.

        public WebhookServiceTests() : base("webhook_service") =>
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

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            const string url = "https://example.com/create";

            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
            Assert.Equal(url, webhook.Url);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
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
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            const string url = "https://example.com/retrieve";

            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            Webhook retrievedWebhook = await Client.Webhook.Retrieve(webhook.Id);

            Assert.IsType<Webhook>(retrievedWebhook);
            Assert.Equal(webhook, retrievedWebhook);
        }

        #endregion

        [Fact]
        [Testing.Function]
        public void TestValidateWebHook()
        {
            UseVCR("validate_webhook");

            byte[] eventData = Fixtures.EventBody;

            // ReSharper disable once StringLiteralTypo
            const string webhookSecret = "sécret";
            Dictionary<string, object?> headers = new() { { "X-Hmac-Signature", "hmac-sha256-hex=e93977c8ccb20363d51a62b3fe1fc402b7829be1152da9e88cf9e8d07115a46b" } };

            Event @event = Client.Webhook.ValidateWebhook(eventData, headers, webhookSecret);

            Assert.Equal("batch.created", @event.Description);
        }

        [Fact]
        [Testing.Exception]
        public void TestValidateWebhookWithInvalidSecret()
        {
            UseVCR("validate_webhook_with_invalid_secret");

            byte[] eventData = Fixtures.EventBody;

            const string webhookSecret = "invalid_secret";
            Dictionary<string, object?> headers = new() { { "X-Hmac-Signature", "hmac-sha256-hex=e93977c8ccb20363d51a62b3fe1fc402b7829be1152da9e88cf9e8d07115a46b" } };

            try
            {
                Event _ = Client.Webhook.ValidateWebhook(eventData, headers, webhookSecret);
            }
            catch (SignatureVerificationError error)
            {
                Assert.Equal(Constants.ErrorMessages.InvalidWebhookSignature, error.Message);
            }
        }

        [Fact]
        [Testing.Exception]
        public void TestValidateWebhookWithMissingSecret()
        {
            UseVCR("validate_webhook_with_missing_secret");

            byte[] eventData = Fixtures.EventBody;

            const string webhookSecret = "123";
            Dictionary<string, object?> headers = new() { { "some-header", "some-value" } };

            try
            {
                Event _ = Client.Webhook.ValidateWebhook(eventData, headers, webhookSecret);
            }
            catch (SignatureVerificationError error)
            {
                Assert.Equal(Constants.ErrorMessages.InvalidWebhookSignature, error.Message);
            }
        }

        #endregion
    }
}
