using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class WebhookServiceTests : UnitTest
    {
        public WebhookServiceTests() : base("webhook_service") =>
            CleanupFunction = async id =>
            {
                try
                {
                    Webhook retrievedWebhook = await Client.Webhook.Retrieve(id);
                    await Client.Webhook.Delete(retrievedWebhook.Id);
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

            string url = $"https://example.com/create/{TestUtils.NetVersion}";

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

            string url = $"https://example.com/retrieve/{TestUtils.NetVersion}";

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
            string webhookHmacSignature = Fixtures.WebhookHmacSignature;
            string webhookSecret = Fixtures.WebhookSecret;

            Dictionary<string, object?> headers = new() { { "X-Hmac-Signature", webhookHmacSignature } };

            Event @event = Client.Webhook.ValidateWebhook(eventData, headers, webhookSecret);

            Assert.Equal("tracker.updated", @event.Description);
            Assert.Equal(614.4, @event.Result.Weight);
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

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            string url = $"https://example.com/update/{TestUtils.NetVersion}";

            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            if (IsRecording()) // Give the server time to process the webhook
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            webhook = await Client.Webhook.Update(webhook.Id, new Dictionary<string, object>());

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
        }

        [Fact]
        [CrudOperations.Delete]
        [Testing.Function]
        public async Task TestDelete()
        {
            UseVCR("delete");

            string url = $"https://example.com/delete/{TestUtils.NetVersion}";

            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            Webhook retrievedWebhook = await Client.Webhook.Retrieve(webhook.Id);

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Webhook.Delete(retrievedWebhook.Id));

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
        }

        #endregion
    }
}
