using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ReferralTest
    {
        private TestUtils.VCR _vcr;

        private static string ReferralUserKey
        {
            get { return TestUtils.GetApiKey(TestUtils.ApiKey.Referral); }
        }

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("referral", TestUtils.ApiKey.Partner);
        }

        private static async Task<ReferralCustomer> CreateReferral()
        {
            return await ReferralCustomer.Create(Fixture.ReferralUser);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            ReferralCustomer referral = await CreateReferral();

            Assert.IsNotNull(referral);
            Assert.IsInstanceOfType(referral, typeof(ReferralCustomer));
            Assert.IsNotNull(referral.id);
            Assert.IsTrue(referral.id.StartsWith("user_"));
            Assert.AreEqual("Test Referral", referral.name);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");

            ReferralCustomer referral = await CreateReferral();
            bool response = await ReferralCustomer.UpdateEmail("email@example.com", referral.id);

            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            };

            ReferralCustomerCollection referralCustomerCollection = await ReferralCustomer.All(parameters);
            List<ReferralCustomer> referralCustomers = referralCustomerCollection.referral_customers;

            Assert.IsNotNull(referralCustomers);
            Assert.IsTrue(referralCustomers.Count <= Fixture.PageSize);
            Assert.IsNotNull(referralCustomerCollection.has_more);
            foreach (ReferralCustomer referral in referralCustomers)
            {
                Assert.IsInstanceOfType(referral, typeof(ReferralCustomer));
            }
        }

        [Ignore]
        [TestMethod]
        public async Task TestReferralAddCreditCard()
        {
            // This test will not record the Stripe API calls, nor the EasyPost add credit card call, since both use different API clients.
            _vcr.SetUpTest("referral_add_credit_card");

            Dictionary<string, object> parameters = Fixture.CreditCardDetails;

            PaymentMethodObject creditCard = await ReferralCustomer.AddCreditCardToUser(ReferralUserKey,
                (string)parameters["number"],
                int.Parse((string)parameters["expiration_month"]),
                int.Parse((string)parameters["expiration_year"]),
                (string)parameters["cvc"],
                PaymentMethod.Priority.Primary
            );

            Assert.IsInstanceOfType(creditCard, typeof(PaymentMethodObject));
            Assert.IsTrue(creditCard.id.StartsWith("card_"));
            Assert.AreEqual(((string)Fixture.CreditCardDetails["number"]).Substring(12), creditCard.last4);
        }
    }
}
