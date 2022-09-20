using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class ReferralTest : UnitTest
    {
        private static string ReferralUserKey
        {
            get { return TestUtils.GetApiKey(TestUtils.ApiKey.Referral); }
        }

        public ReferralTest() : base("referral", TestUtils.ApiKey.Partner)
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            ReferralCustomer referralCustomer = await CreateReferral();

            Assert.NotNull(referralCustomer);
            Assert.IsType<ReferralCustomer>(referralCustomer);
            Assert.NotNull(referralCustomer.Id);
            Assert.StartsWith("user_", referralCustomer.Id);
            Assert.Equal("Test Referral", referralCustomer.Name);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            ReferralCustomerCollection referralCustomerCollection = await Client.Partner.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            List<ReferralCustomer> referralCustomers = referralCustomerCollection.ReferralCustomers;

            Assert.True(referralCustomerCollection.HasMore);
            Assert.True(referralCustomers.Count <= Fixtures.PageSize);
            foreach (ReferralCustomer item in referralCustomers)
            {
                Assert.IsType<ReferralCustomer>(item);
            }
        }

        [Fact(Skip = "VCR issues prevent this from being recorded completely.")]
        [CrudOperations.Update]
        public async Task TestAddCreditCardToReferralCustomer()
        {
            UseVCR("add_credit_card_to_referral_customer");

            ReferralCustomer referralCustomer = await CreateReferral();

            Dictionary<string, object> creditCardDetails = Fixtures.CreditCardDetails;

            string creditCardNumber = (string)creditCardDetails["number"];
            int creditCardExpirationMonth = int.Parse((string)creditCardDetails["expiration_month"]);
            int creditCardExpirationYear = int.Parse((string)creditCardDetails["expiration_year"]);
            string creditCardCvc = (string)creditCardDetails["cvc"];

            PaymentMethod paymentMethod = await Client.Partner.AddCreditCardToUser(ReferralUserKey, creditCardNumber, creditCardExpirationMonth, creditCardExpirationYear, creditCardCvc, PaymentMethod.Priority.Primary);

            Assert.NotNull(paymentMethod);
            Assert.IsType<PaymentMethod>(paymentMethod);
            Assert.NotNull(paymentMethod.Id);
            Assert.EndsWith(paymentMethod.Last4, creditCardNumber);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestUpdateEmail()
        {
            UseVCR("update_email");

            ReferralCustomer referralCustomer = await CreateReferral();

            var possibleException = await Record.ExceptionAsync(async () => await Client.Partner.UpdateReferralEmail(referralCustomer.Id, "email@example.com"));

            Assert.Null(possibleException);
        }

        #endregion

        private async Task<ReferralCustomer> CreateReferral() => await Client.Partner.CreateReferral(Fixtures.ReferralUser);
    }
}
