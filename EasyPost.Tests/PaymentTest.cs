using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class PaymentTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("payment", TestUtils.ApiKey.Production);

        private static async Task<PaymentMethodSummary> GetPaymentMethodSummary(V2Client client) => await client.Payments.All();

        [TestMethod]
        // need to manually add details via dashboard when recording
        public async Task TestAll()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("all");

            PaymentMethodSummary summary = await GetPaymentMethodSummary(client);

            Assert.IsNotNull(summary.primary_payment_method);
        }

        [Ignore]
        [TestMethod]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestDelete()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("delete");

            PaymentMethodSummary summary = await GetPaymentMethodSummary(client);

            CreditCard creditCard = summary.primary_payment_method;

            bool success = await creditCard.Delete();

            Assert.IsTrue(success);
        }

        [TestMethod]
        [Ignore]
        // Skipping due to the lack of an available real credit card in tests.
        public async Task TestFund()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("fund");

            PaymentMethodSummary summary = await GetPaymentMethodSummary(client);

            CreditCard creditCard = summary.primary_payment_method;

            CreditCardFunding funding = await creditCard.Fund("20");

            Assert.IsNotNull(funding);
            Assert.IsInstanceOfType(funding, typeof(CreditCardFunding));
        }
    }
}
