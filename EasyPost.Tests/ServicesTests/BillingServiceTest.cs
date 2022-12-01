using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
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
                        new TestUtils.MockRequestMatchRules(Method.Post, @"^v2\/bank_accounts\/\S*\/charges$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK)
                    ),
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Post, @"^v2\/credit_cards\/\S*\/charges$"),
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
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestFundWalletNoPriorityLevel()
        {
            UseMockClient();

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Billing.FundWallet("2000"));

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
        [Testing.Exception]
        public async Task TestRetrievePaymentMethodsSummaryNoId()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"^v2\/payment_methods$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethodsSummary
                    {
                        Id = null // No ID, will throw an error when we try to interact with this summary
                    })
                )
            });

            await Assert.ThrowsAsync<InvalidObjectError>(async () => await Client.Billing.RetrievePaymentMethodsSummary());
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

        [Fact]
        [CrudOperations.Delete]
        [Testing.Logic]
        public async Task TestGetPaymentMethodByPrioritySwitchCase()
        {
            UseMockClient();

            // Deleting a payment method gets the payment method internally, which should test the switch case.
            await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Primary);
            // The payment method is not exposed by this method, so we can't assert against it. If this test doesn't throw an exception, it worked (see test below)

            // This time the internal switch case should use the secondary payment method. Again, if there's no exception thrown, it worked.
            await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Secondary);

            // Now if we pass in a bad priority level, it should throw an exception as the default for the switch case
            // Because this method only accepts enums (well, the custom enum class, but one that can't be extended/modified), we can't actually pass in a bad value.
            // Therefore, something would have to be wildly unpredictable and horrible for this to actually error out for an end user.
        }

        [Fact]
        [CrudOperations.Delete]
        [Testing.Exception]
        public async Task TestGetPaymentMethodByPriorityNoPaymentMethod()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"^v2\/payment_methods$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethodsSummary
                    {
                        Id = "summary_123",
                        PrimaryPaymentMethod = null, // null, will throw an error when we try to grab this payment method from the summary
                        SecondaryPaymentMethod = null // null, will throw an error when we try to grab this payment method from the summary
                    })
                )
            });

            // Deleting a payment method gets the payment method internally, which should execute the code that will trigger an exception.
            await Assert.ThrowsAsync<InvalidObjectError>(async () => await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Primary));
        }

        [Fact]
        [CrudOperations.Delete]
        [Testing.Exception]
        public async Task TestGetPaymentMethodByPriorityPaymentMethodNoId()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"^v2\/payment_methods$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethodsSummary
                    {
                        Id = "summary_123",
                        PrimaryPaymentMethod = new PaymentMethod
                        {
                            Id = null // No ID, will throw an error when we try to grab this payment method from the summary
                        },
                        SecondaryPaymentMethod = new PaymentMethod
                        {
                            Id = null // No ID, will throw an error when we try to grab this payment method from the summary
                        }
                    })
                )
            });

            // Deleting a payment method gets the payment method internally, which should execute the code that will trigger an exception.
            await Assert.ThrowsAsync<InvalidObjectError>(async () => await Client.Billing.DeletePaymentMethod(PaymentMethod.Priority.Primary));
        }

        #endregion

        #endregion
    }
}
