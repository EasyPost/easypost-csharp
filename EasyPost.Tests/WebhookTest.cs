using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class WebhookTest : UnitTest
    {
        // NOTE: Because the API does not allow two webhooks with the same URL,
        // and these tests run in parallel, each test needs to have a unique URL.

        public WebhookTest() : base("webhook") =>
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

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            const string url = "https://example.com/create";

            Webhook webhook = await CreateBasicWebhook(url);

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
            Assert.Equal(url, webhook.Url);
        }

        [Fact]
        [CrudOperations.Read]
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
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            const string url = "https://example.com/retrieve";

            Webhook webhook = await CreateBasicWebhook(url);

            Webhook retrievedWebhook = await Client.Webhook.Retrieve(webhook.Id);

            Assert.IsType<Webhook>(retrievedWebhook);
            Assert.Equal(webhook, retrievedWebhook);
        }

        [Fact]
        [CrudOperations.Read]
        public void TestValidateWebHook()
        {
            UseVCR("validate");

            byte[] data = Fixtures.EventBody;

            // ReSharper disable once StringLiteralTypo
            const string webhookSecret = "sécret";
            Dictionary<string, object?> headers = new Dictionary<string, object?> { { "X-Hmac-Signature", "hmac-sha256-hex=e93977c8ccb20363d51a62b3fe1fc402b7829be1152da9e88cf9e8d07115a46b" } };

            Event @event = Client.Webhook.ValidateWebhook(data, headers, webhookSecret);

            Assert.Equal("batch.created", @event.Description);
        }

        [Fact]
        [CrudOperations.Read]
        public void TestValidateWebhookInvalidSecret()
        {
            UseVCR("validate_invalid_secret");

            byte[] data = Fixtures.EventBody;

            const string webhookSecret = "invalid_secret";
            Dictionary<string, object?> headers = new Dictionary<string, object?> { { "X-Hmac-Signature", "hmac-sha256-hex=e93977c8ccb20363d51a62b3fe1fc402b7829be1152da9e88cf9e8d07115a46b" } };

            try
            {
                Event _ = Client.Webhook.ValidateWebhook(data, headers, webhookSecret);
            }
            catch (SignatureVerificationError error)
            {
                Assert.Equal(Constants.ErrorMessages.InvalidWebhookSignature, error.Message);
            }
        }

        [Fact]
        [CrudOperations.Read]
        public void TestValidateWebhookMissingSecret()
        {
            UseVCR("validate_missing_secret");

            byte[] data = Fixtures.EventBody;

            const string webhookSecret = "123";
            Dictionary<string, object?> headers = new Dictionary<string, object?> { { "some-header", "some-value" } };

            try
            {
                Event _ = Client.Webhook.ValidateWebhook(data, headers, webhookSecret);
            }
            catch (SignatureVerificationError error)
            {
                Assert.Equal(Constants.ErrorMessages.InvalidWebhookSignature, error.Message);
            }
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestUpdate()
        {
            UseVCR("update");

            const string url = "https://example.com/update";

            Webhook webhook = await CreateBasicWebhook(url);

            webhook = await webhook.Update();

            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
        }

        [Fact]
        [CrudOperations.Delete]
        public async Task TestDelete()
        {
            UseVCR("delete");

            const string url = "https://example.com/delete";

            Webhook webhook = await CreateBasicWebhook(url);
            Webhook retrievedWebhook = await Client.Webhook.Retrieve(webhook.Id);

            var possibleException = await Record.ExceptionAsync(async () => await retrievedWebhook.Delete());

            Assert.Null(possibleException);

            SkipCleanUpAfterTest();
        }

        #endregion

        private async Task<Webhook> CreateBasicWebhook(string url)
        {
            Webhook webhook = await Client.Webhook.Create(new Dictionary<string, object> { { "url", url } });
            CleanUpAfterTest(webhook.Id);

            return webhook;
        }
    }
}
