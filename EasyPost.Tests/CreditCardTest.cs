using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CreditCardTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("credit_card", TestUtils.ApiKey.Production);
        }

        [Ignore]
        [TestMethod]
        // When recording, can only record in one framework, then manually copy cassette to other frameworks.
        public async Task TestDelete()
        {
            _vcr.SetUpTest("delete");

            PaymentMethod paymentMethod = await PaymentMethod.All();
            CreditCard creditCard = paymentMethod.primary_payment_method;

            bool success = await CreditCard.Delete(creditCard.id);

            Assert.IsTrue(success);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestFund()
        {
            _vcr.SetUpTest("fund");

            bool success = await CreditCard.Fund("2000", CreditCard.Priority.Primary);

            Assert.IsTrue(success);
        }
    }
}
