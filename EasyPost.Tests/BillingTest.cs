using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class PaymentTest : UnitTest
    {
        public PaymentTest() : base("payment", TestUtils.ApiKey.Production)
        {
        }

        [Fact(Skip = "Skipping due to the lack of an available real payment method in tests.")]
        public async Task TestDeletePaymentMethod()
        {
            UseVCR("delete_payment_method");

            await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Primary);

            // TODO: Assert something here
        }

        [Fact(Skip = "Skipping due to the lack of an available real payment method in tests.")]
        public async Task TestFundWallet()
        {
            UseVCR("fund_wallet");

            await Client.Billing.FundWallet("2000", PaymentMethod.Priority.Primary);

            // TODO: Assert something here
        }

        [Fact(Skip = "Skipping due to having to manually add and remove a payment method from the account.")]
        public async Task TestRetrievePaymentMethods()
        {
            UseVCR("retrieve_payment_methods");

            PaymentMethodsSummary paymentMethodsSummary = await Client.Billing.RetrievePaymentMethodsSummary();

            Assert.IsNotNull(paymentMethodsSummary.primary_payment_method);
            Assert.IsNotNull(paymentMethodsSummary.secondary_payment_method);
        }
    }
}
