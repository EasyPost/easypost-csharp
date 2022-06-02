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

            PaymentMethod paymentMethods = await PaymentMethod.All();

            Assert.IsNotNull(paymentMethods.primary_payment_method);
            Assert.IsNotNull(paymentMethods.secondary_payment_method);
        }

        [TestMethod]
        // When recording, can only record in one framework, then manually copy cassette to other frameworks.
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            PaymentMethod paymentMethods = await PaymentMethod.All();

            CreditCard creditCard = paymentMethods.primary_payment_method;

            bool success = await creditCard.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        [Ignore]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestFund()
        {
            _vcr.SetUpTest("fund");

            PaymentMethod paymentMethods = await PaymentMethod.All();

            CreditCard creditCard = paymentMethods.primary_payment_method;

            CreditCardFund funding = await creditCard.Fund("10");

            Assert.IsNotNull(funding);
            Assert.IsInstanceOfType(funding, typeof(CreditCardFund));
        }
    }
}
