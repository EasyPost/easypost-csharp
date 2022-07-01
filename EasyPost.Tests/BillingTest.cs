using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class BillingTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("billing", TestUtils.ApiKey.Production);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to the lack of an available real payment method in tests.
        public async Task TestDeletePaymentMethod()
        {
            _vcr.SetUpTest("delete_payment_method");

            PaymentMethod paymentMethod = await Billing.RetrievePaymentMethods();
            PaymentMethodObject creditCardOrBankAccount = paymentMethod.primary_payment_method;

            bool success = await Billing.DeletePaymentMethod(creditCardOrBankAccount);

            Assert.IsTrue(success);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to the lack of an available real payment method in tests.
        public async Task TestFundWallet()
        {
            _vcr.SetUpTest("fund_wallet");

            bool success = await Billing.FundWallet("2000", PaymentMethod.Priority.Primary);

            Assert.IsTrue(success);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to having to manually add and remove a payment method from the account.
        public async Task TestRetrievePaymentMethods()
        {
            _vcr.SetUpTest("retrieve_payment_methods");

            PaymentMethod paymentMethods = await Billing.RetrievePaymentMethods();

            Assert.IsNotNull(paymentMethods.primary_payment_method);
            Assert.IsNotNull(paymentMethods.secondary_payment_method);
        }
    }
}
