using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
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
            UseVCR("delete_payment_method", ApiVersion.Latest);

            bool success = await Client.Billing.DeletePaymentMethod(PaymentMethodPriority.Primary);

            Assert.IsTrue(success);
        }

        [Fact(Skip = "Skipping due to the lack of an available real payment method in tests.")]
        public async Task TestFundWallet()
        {
            UseVCR("fund_wallet", ApiVersion.Latest);

            bool success = await Client.Billing.FundWallet(new Billing.Fund
            {
                Amount = "2000"
            }, PaymentMethodPriority.Primary);

            Assert.IsTrue(success);
        }

        [Fact(Skip = "Skipping due to having to manually add and remove a payment method from the account.")]
        public async Task TestRetrievePaymentMethods()
        {
            UseVCR("retrieve_payment_methods", ApiVersion.Latest);

            PaymentMethodsSummary paymentMethodsSummary = await Client.Billing.RetrievePaymentMethodsSummary();

            Assert.IsNotNull(paymentMethodsSummary.PrimaryPaymentMethod);
            Assert.IsNotNull(paymentMethodsSummary.SecondaryPaymentMethod);
        }
    }
}
