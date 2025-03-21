using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.Beta
{
    public class ReferralServiceTests : UnitTest
    {
        public ReferralServiceTests() : base("beta_referral_service", TestUtils.ApiKey.Referral)
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestAddPaymentMethod()
        {
            UseVCR("add_payment_method");

            try
            {
                PaymentMethod _ = await Client.Beta.ReferralCustomer.AddPaymentMethod("cus_123", "ba_123");
            }
            catch (InvalidRequestError e)
            {
                // the data we're sending is invalid, we expect an error to be thrown
                Assert.Equal(422, e.StatusCode);
                Assert.Equal("BILLING.INVALID_PAYMENT_GATEWAY_REFERENCE", e.Code);
                Assert.Equal("Invalid connect integration.", e.Message);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRefundByAmount()
        {
            UseVCR("refund_by_amount");

            try
            {
                PaymentRefund _ = await Client.Beta.ReferralCustomer.RefundByAmount(2000);
            }
            catch (InvalidRequestError e)
            {
                // the data we're sending is invalid, we expect an error to be thrown
                Assert.Equal(422, e.StatusCode);
                Assert.Equal("TRANSACTION.AMOUNT_INVALID", e.Code);
                Assert.Equal("Refund amount is invalid. Please use a valid amount or escalate to finance.", e.Message);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestRefundByPaymentLog()
        {
            UseVCR("refund_by_payment_log");

            try
            {
                PaymentRefund _ = await Client.Beta.ReferralCustomer.RefundByPaymentLog("paylog_123");
            }
            catch (InvalidRequestError e)
            {
                // the data we're sending is invalid, we expect an error to be thrown
                Assert.Equal(422, e.StatusCode);
                Assert.Equal("TRANSACTION.DOES_NOT_EXIST", e.Code);
                Assert.Equal("We could not find a transaction with that id.", e.Message);
            }
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateCreditCardClientSecret()
        {
            UseVCR("create_credit_card_client_secret");

            StripeClientSecret response = await Client.Beta.ReferralCustomer.CreateCreditCardClientSecret();

            Assert.StartsWith("seti_", response.ClientSecret);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateBankAccountClientSecret()
        {
            UseVCR("create_bank_account_client_secret");

            StripeClientSecret response = await Client.Beta.ReferralCustomer.CreateBankAccountClientSecret();

            Assert.StartsWith("fcsess_client_secret_", response.ClientSecret);
        }

        #endregion

        #endregion
    }
}
