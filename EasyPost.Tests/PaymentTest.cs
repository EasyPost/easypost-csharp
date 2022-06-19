using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class PaymentTest : UnitTest
    {
        public PaymentTest() : base("payment", TestUtils.ApiKey.Production)
        {
        }

        [Fact]
        // need to manually add details via dashboard when recording
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            PaymentMethodSummary summary = await GetPaymentMethodSummary();

            Assert.IsNotNull(summary.PrimaryPaymentMethod);
        }

        [Fact(Skip = "Lack of an available real credit card in tests.")]
        public async Task TestDelete()
        {
            UseVCR("delete", ApiVersion.Latest);

            PaymentMethodSummary summary = await GetPaymentMethodSummary();

            CreditCard creditCard = summary.PrimaryPaymentMethod;

            bool success = await creditCard.Delete();

            Assert.IsTrue(success);
        }

        [Fact(Skip = "Lack of an available real credit card in tests.")]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestFund()
        {
            UseVCR("fund", ApiVersion.Latest);

            PaymentMethodSummary summary = await GetPaymentMethodSummary();

            CreditCard creditCard = summary.PrimaryPaymentMethod;

            CreditCardFunding funding = await creditCard.Fund("20");

            Assert.IsNotNull(funding);
            Assert.IsInstanceOfType(funding, typeof(CreditCardFunding));
        }

        private async Task<PaymentMethodSummary> GetPaymentMethodSummary() => await Client.Payments.All();
    }
}
