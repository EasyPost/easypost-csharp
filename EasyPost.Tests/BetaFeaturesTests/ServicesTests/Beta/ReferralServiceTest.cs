using System.Threading.Tasks;
using EasyPost.Exceptions.API;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests.Beta
{
    public class ReferralServiceTests : UnitTest
    {
        public ReferralServiceTests() : base("beta_referral_service_with_parameters", TestUtils.ApiKey.Referral)
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
                BetaFeatures.Parameters.ReferralCustomers.AddPaymentMethod parameters = new()
                {
                    StripeCustomerId = "cus_123",
                    PaymentMethodReference = "ba_123",
                };

                PaymentMethod _ = await Client.Beta.Referral.AddPaymentMethod(parameters);
            }
            catch (InvalidRequestError e)
            {
                // the data we're sending is invalid, we expect an error to be thrown
                Assert.Equal(422, e.StatusCode);
                Assert.Equal("BILLING.INVALID_PAYMENT_GATEWAY_REFERENCE", e.Code);
                Assert.Equal("Invalid Payment Gateway Reference.", e.Message);
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
                BetaFeatures.Parameters.ReferralCustomers.RefundByAmount parameters = new()
                {
                    Amount = 2000,
                };

                PaymentRefund _ = await Client.Beta.Referral.RefundByAmount(parameters);
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
                BetaFeatures.Parameters.ReferralCustomers.RefundByPaymentLog parameters = new()
                {
                    PaymentLogId = "paylog_123",
                };

                PaymentRefund _ = await Client.Beta.Referral.RefundByPaymentLog(parameters);
            }
            catch (InvalidRequestError e)
            {
                // the data we're sending is invalid, we expect an error to be thrown
                Assert.Equal(422, e.StatusCode);
                Assert.Equal("TRANSACTION.DOES_NOT_EXIST", e.Code);
                Assert.Equal("We could not find a transaction with that id.", e.Message);
            }
        }

        #endregion

        #endregion
    }
}
