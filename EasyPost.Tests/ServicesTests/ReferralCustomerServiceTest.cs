using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
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
        public async Task TestAddCreditCardToUser()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"pk_test_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"https://api.stripe.com/v1/tokens$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"id\":\"tok_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/credit_cards$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethod
                    {
                        Id = "summary_123",
                        Last4 = ((string)Fixtures.CreditCardDetails["number"]).Substring(12),
                    })
                )
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            PaymentMethod paymentMethod = await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary);

            Assert.NotNull(paymentMethod);
            Assert.IsType<PaymentMethod>(paymentMethod);
            Assert.NotNull(paymentMethod.Id);
            Assert.EndsWith(paymentMethod.Last4, card.Number);

            // Assert that the original API key was restored to the client properly after the request
            Assert.Equal(TestUtils.GetApiKey(TestUtils.ApiKey.Mock), Client.ApiKeyInUse);
        }

        [Fact]
        [Testing.Parameters]
        public async Task TestAddCreditCardToUserDifferentPaymentMethodPriorities()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"pk_test_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"https://api.stripe.com/v1/tokens$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"id\":\"tok_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/credit_cards$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, data: new PaymentMethod
                    {
                        Id = "summary_123",
                        Last4 = ((string)Fixtures.CreditCardDetails["number"]).Substring(12),
                    })
                )
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            PaymentMethod.Priority? priority = PaymentMethod.Priority.Primary;

            PaymentMethod paymentMethod = await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, priority);
            Assert.NotNull(paymentMethod);
            // If we've gotten here, no internal errors occurred on the method.

            // Test with other priorities.
            priority = PaymentMethod.Priority.Secondary;
            paymentMethod = await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, priority);
            Assert.NotNull(paymentMethod);

            priority = null; // Should internally default to primary priority if not specified.
            paymentMethod = await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, priority);
            Assert.NotNull(paymentMethod);
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardToUserNoPublicKey()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                {
                    new(
                        new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                        new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"\"}")
                    )
                }
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            await Assert.ThrowsAsync<InternalServerError>(async () => await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary));
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardToUserBadPublicKeyResponse()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"not_public_key\":\"random\"}")
                ),
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            await Assert.ThrowsAsync<InternalServerError>(async () => await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary));
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardToUserNoStripeToken()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"pk_test_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"https://api.stripe.com/v1/tokens$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"id\":\"\"}")
                )
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            await Assert.ThrowsAsync<ExternalApiError>(async () => await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary));
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardToUserStripeCantConnect()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"pk_test_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"https://api.stripe.com/v1/tokens$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.NotFound, content: "{}")
                )
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            await Assert.ThrowsAsync<ExternalApiError>(async () => await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary));
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardToUserStripeBadResponse()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"pk_test_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"https://api.stripe.com/v1/tokens$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"not_id\":\"random\"}")
                )
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            await Assert.ThrowsAsync<ExternalApiError>(async () => await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary));
        }

        [Fact]
        [Testing.Exception]
        public async Task TestAddCreditCardToUserEasyPostApiError()
        {
            UseMockClient(new List<TestUtils.MockRequest>
            {
                new(
                    new TestUtils.MockRequestMatchRules(Method.Get, @"v2\/partners\/stripe_public_key$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"public_key\":\"pk_test_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"https://api.stripe.com/v1/tokens$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.OK, content: "{\"id\":\"tok_12345\"}")
                ),
                new(
                    new TestUtils.MockRequestMatchRules(Method.Post, @"v2\/credit_cards$"),
                    new TestUtils.MockRequestResponseInfo(HttpStatusCode.NotFound, content: "{}")
                )
            });

            CreditCard card = new CreditCard(Fixtures.CreditCardDetails);

            await Assert.ThrowsAsync<NotFoundError>(async () => await Client.ReferralCustomer.AddCreditCardToUser(ReferralCustomerKey, card.Number, card.ExpirationMonth, card.ExpirationYear, card.Cvc, PaymentMethod.Priority.Primary));

            // Assert that the original API key was restored to the client properly even after the failed request
            Assert.Equal(TestUtils.GetApiKey(TestUtils.ApiKey.Mock), Client.ApiKeyInUse);
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

        #endregion

        #endregion

        private static string ReferralCustomerKey => TestUtils.GetApiKey(TestUtils.ApiKey.Referral);

        private sealed class CreditCard
        {
            internal readonly string Number;

            internal readonly int ExpirationMonth;

            internal readonly int ExpirationYear;

            internal readonly string Cvc;

            internal CreditCard(IReadOnlyDictionary<string, object> details)
            {
                Number = (string)details["number"];
                ExpirationMonth = int.Parse((string)details["expiration_month"], NumberStyles.Number, CultureInfo.InvariantCulture);
                ExpirationYear = int.Parse((string)details["expiration_year"], NumberStyles.Number, CultureInfo.InvariantCulture);
                Cvc = (string)details["cvc"];
            }
        }
    }
}
