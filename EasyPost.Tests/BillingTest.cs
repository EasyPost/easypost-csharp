using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class PaymentTest : UnitTest
    {
        public PaymentTest() : base("payment", TestUtils.ApiKey.Production)
        {
        }

        #region CRUD Operations

        [Fact(Skip = "Skipping due to the lack of an available real payment method in tests.")]
        [CrudOperations.Create]
        public async Task TestFundWallet()
        {
            UseVCR("fund_wallet");

            var possibleException = await Record.ExceptionAsync(async () => await Client.Billing.FundWallet("2000", PaymentMethod.Priority.Primary));

            Assert.Null(possibleException);
        }

        [Fact(Skip = "Skipping due to having to manually add and remove a payment method from the account.")]
        [CrudOperations.Read]
        public async Task TestRetrievePaymentMethods()
        {
            UseVCR("retrieve_payment_methods");

            PaymentMethodsSummary paymentMethodsSummary = await Client.Billing.RetrievePaymentMethodsSummary();

            Assert.NotNull(paymentMethodsSummary.primary_payment_method);
            Assert.NotNull(paymentMethodsSummary.secondary_payment_method);
        }

        [Fact(Skip = "Skipping due to the lack of an available real payment method in tests.")]
        [CrudOperations.Delete]
        public async Task TestDeletePaymentMethod()
        {
            UseVCR("delete_payment_method");

            var possibleException = await Record.ExceptionAsync(async () => await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Primary));

            Assert.Null(possibleException);
        }

        #endregion
    }
}
