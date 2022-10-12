using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using RestSharp;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class BillingServiceTests : UnitTest
    {
        public BillingServiceTests() : base("billing_service")
        {
        }

        protected override IEnumerable<TestUtils.MockRequest> MockRequests
        {
            get
            {
                return new List<TestUtils.MockRequest>
                {
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"^v2\/bank_accounts\/\S*\/charge$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"^v2\/credit_cards\/\S*\/charge$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Delete, @"^v2\/bank_accounts\/\S*$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Delete, @"^v2\/credit_cards\/\S*$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Get, @"^v2\/payment_methods$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethodsSummary
                        {
                            Id = "summary_123",
                            PrimaryPaymentMethod = new PaymentMethod
                            {
                                Id = "card_123",
                                Client = Client,
                                Last4 = "1234",
                            },
                            SecondaryPaymentMethod = new PaymentMethod
                            {
                                Id = "bank_123",
                                Client = Client,
                                BankName = "Mock Bank",
                            }
                        })
                    )
                };
            }
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestFundWallet()
        {
            UseMockClient();

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Billing.FundWallet("2000", PaymentMethod.Priority.Primary));

            Assert.Null(possibleException);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrievePaymentMethodsSummary()
        {
            UseMockClient();

            PaymentMethodsSummary paymentMethodsSummary = await Client.Billing.RetrievePaymentMethodsSummary();

            Assert.NotNull(paymentMethodsSummary.PrimaryPaymentMethod);
            Assert.NotNull(paymentMethodsSummary.SecondaryPaymentMethod);
        }

        [Fact]
        [CrudOperations.Delete]
        [Testing.Function]
        public async Task TestDeletePaymentMethod()
        {
            UseMockClient();

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Primary));

            Assert.Null(possibleException);
        }

        #endregion

        #endregion
    }
}
