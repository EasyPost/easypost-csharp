using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class PaymentMethodTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("payment_method", TestUtils.ApiKey.Production);
        }

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            PaymentMethod paymentMethods = await Billing.RetrievePaymentMethods();

            Assert.IsNotNull(paymentMethods.primary_payment_method);
            Assert.IsNotNull(paymentMethods.secondary_payment_method);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestDeleteCreditCard()
        {
            _vcr.SetUpTest("delete_credit_card");

            PaymentMethod paymentMethod = await Billing.RetrievePaymentMethods();
            PaymentMethodObject creditCard = paymentMethod.primary_payment_method;

            bool success = await creditCard.Delete();
            // ^ instance method, can also do below
            // bool success = await Billing.DeletePaymentMethod(creditCard.id);

            Assert.IsTrue(success);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestFund()
        {
            _vcr.SetUpTest("fund");

            bool success = await Billing.FundWallet("2000", PaymentMethod.Priority.Primary);

            Assert.IsTrue(success);
        }
    }
}
