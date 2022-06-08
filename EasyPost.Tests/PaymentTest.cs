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
            UseVCR("all", ApiVersion.V2);

            PaymentMethodSummary summary = await GetPaymentMethodSummary();

            Assert.IsNotNull(summary.primary_payment_method);
        }

        [Fact(Skip = "Lack of an available real credit card in tests.")]
        public async Task TestDelete()
        {
            UseVCR("delete", ApiVersion.V2);

            PaymentMethodSummary summary = await GetPaymentMethodSummary();

            CreditCard creditCard = summary.primary_payment_method;

            bool success = await creditCard.Delete();

            Assert.IsTrue(success);
        }

        [Fact(Skip = "Lack of an available real credit card in tests.")]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestFund()
        {
            UseVCR("fund", ApiVersion.V2);

            PaymentMethodSummary summary = await GetPaymentMethodSummary();

            CreditCard creditCard = summary.primary_payment_method;

            CreditCardFunding funding = await creditCard.Fund("20");

            Assert.IsNotNull(funding);
            Assert.IsInstanceOfType(funding, typeof(CreditCardFunding));
        }

        private async Task<PaymentMethodSummary> GetPaymentMethodSummary() => await Client.Payments.All();
    }
}
