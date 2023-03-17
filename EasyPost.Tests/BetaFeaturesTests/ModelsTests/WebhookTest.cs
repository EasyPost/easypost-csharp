using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
    public class WebhookTests : UnitTest
    {
        // NOTE: Because the API does not allow two webhooks with the same URL,
        // and these tests run in parallel, each test needs to have a unique URL.

        public WebhookTests() : base("webhook_with_parameters") =>
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
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdate()
        {
            UseVCR("update");

            const string url = "https://example.com/update";
            BetaFeatures.Parameters.Webhooks.Create webhookParameters = new()
            {
                Url = url,
            };
            Webhook webhook = await Client.Webhook.Create(webhookParameters);
            CleanUpAfterTest(webhook.Id);

            BetaFeatures.Parameters.Webhooks.Update updateParameters = new()
            {
            };
            // Sending an empty payload will toggle the active status of the webhook silently.
            webhook = await webhook.Update(updateParameters);

            // We can only update the secret, but that's not returned as part of the Webhook object, so we have no property to check to see if it was updated.
            Assert.IsType<Webhook>(webhook);
            Assert.StartsWith("hook_", webhook.Id);
        }

        #endregion

        #endregion
    }
}
