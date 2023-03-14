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
                PaymentMethod _ = await Client.Beta.Referral.AddPaymentMethod("cus_123", "ba_123");
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
                PaymentRefund _ = await Client.Beta.Referral.RefundByAmount(2000);
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
                PaymentRefund _ = await Client.Beta.Referral.RefundByPaymentLog("paylog_123");
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
