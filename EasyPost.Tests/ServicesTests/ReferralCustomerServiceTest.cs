using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class ReferralCustomerServiceTests : UnitTest
    {
        public ReferralCustomerServiceTests() : base("referral_customer_service", TestUtils.ApiKey.Partner)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateReferral()
        {
            UseVCR("create_referral");

            ReferralCustomer referralCustomer = await Client.ReferralCustomer.CreateReferral(Fixtures.ReferralCustomer);

            Assert.NotNull(referralCustomer);
            Assert.IsType<ReferralCustomer>(referralCustomer);
            Assert.StartsWith("user_", referralCustomer.Id);
            Assert.Equal("Test Referral", referralCustomer.Name);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            ReferralCustomerCollection referralCustomerCollection = await Client.ReferralCustomer.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            List<ReferralCustomer> referralCustomers = referralCustomerCollection.ReferralCustomers;

            Assert.True(referralCustomers.Count <= Fixtures.PageSize);
            foreach (ReferralCustomer item in referralCustomers)
            {
                Assert.IsType<ReferralCustomer>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            ReferralCustomerCollection collection = await Client.ReferralCustomer.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                ReferralCustomerCollection nextPageCollection = await Client.ReferralCustomer.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.ReferralCustomers[0].Id, nextPageCollection.ReferralCustomers[0].Id);
            }
            catch (EndOfPaginationError) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestUpdateReferralEmail()
        {
            UseVCR("update_referral_email");

            ReferralCustomer referralCustomer = await Client.ReferralCustomer.CreateReferral(Fixtures.ReferralCustomer);

            if (IsRecording()) // Give the server time to process the referral user
            {
                Thread.Sleep(10000); // Wait enough time to process
            }

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.ReferralCustomer.UpdateReferralEmail(referralCustomer.Id, "email@example.com"));

            Assert.Null(possibleException);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardFromStripe()
        {
            UseVCR("add_credit_card_from_stripe");

            NotFoundError exception = await Assert.ThrowsAsync<NotFoundError>(async () =>
            {
                await Client.ReferralCustomer.AddCreditCardFromStripe(
                    ReferralCustomerKey,
                    Fixtures.Billing.PaymentMethodId,
                    PaymentMethod.Priority.Primary
                );
            });

            Assert.Equal("Stripe::PaymentMethod does not exist for the specified reference_id", exception.Message);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddBankAccountFromStripe()
        {
            UseVCR("add_bank_account_from_stripe");

            InvalidRequestError exception = await Assert.ThrowsAsync<InvalidRequestError>(async () =>
            {
                await Client.ReferralCustomer.AddBankAccountFromStripe(
                    ReferralCustomerKey,
                    Fixtures.Billing.FinancialConnectionsId,
                    Fixtures.Billing.MandateData,
                    PaymentMethod.Priority.Primary
                );
            });

            Assert.Equal(
                "account_holder_name must be present when creating a Financial Connections payment method",
                exception.Message
            );
        }

        [Fact]
        [Testing.Function]
        public async Task TestRetrieveEasypostStripeApiKey()
        {
            UseVCR("retrieve_easypost_stripe_api_key");

            string? publicKey = await Client.ReferralCustomer.RetrieveEasypostStripeApiKey();

            Assert.IsType<string>(publicKey);
            Assert.StartsWith("pk_", publicKey);
        }

        #endregion

        #endregion

        private static string ReferralCustomerKey => TestUtils.GetApiKey(TestUtils.ApiKey.Referral);
    }
}
