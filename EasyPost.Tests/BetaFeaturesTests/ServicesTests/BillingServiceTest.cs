using System.Collections.Generic;
using System.Net;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class BillingServiceTests : UnitTest
    {
        public BillingServiceTests() : base("billing_service_with_parameters")
        {
        }

        protected override IEnumerable<TestUtils.MockRequest> MockRequests
        {
            get
            {
                return new List<TestUtils.MockRequest>
                {
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/bank_accounts\/\S*\/charges$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/credit_cards\/\S*\/charges$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Delete, @"v2\/bank_accounts\/\S*$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Delete, @"v2\/credit_cards\/\S*$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/payment_methods$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethodsSummary
                        {
                            Id = "summary_123",
                            PrimaryPaymentMethod = new PaymentMethod
                            {
                                Id = "card_123",
                                Last4 = "1234",
                            },
                            SecondaryPaymentMethod = new PaymentMethod
                            {
                                Id = "bank_123",
                                BankName = "Mock Bank",
                            },
                        })
                    ),
                };
            }
        }

        #region Tests

        #region Test CRUD Operations

        #endregion

        #endregion
    }
}
