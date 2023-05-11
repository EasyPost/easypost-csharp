using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#referral-customers">referral customer-related functionality</a>.
    /// </summary>
    public class ReferralCustomerService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReferralCustomerService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal ReferralCustomerService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="ReferralCustomer"/> for the account associated with the API key used.
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the white label partner's API key.
        ///     <a href="https://www.easypost.com/docs/api#create-a-referral-customer">Referral Customer API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="ReferralCustomer"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ReferralCustomer"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<ReferralCustomer> CreateReferral(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("user");
            return await RequestAsync<ReferralCustomer>(Method.Post, "referral_customers", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="ReferralCustomer"/> for the account associated with the API key used.
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the white label partner's API key.
        ///     <a href="https://www.easypost.com/docs/api#create-a-referral-customer">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="ReferralCustomer"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ReferralCustomer"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<ReferralCustomer> CreateReferral(Parameters.ReferralCustomer.CreateReferralCustomer parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal CreateReferral method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<ReferralCustomer>(Method.Post, "referral_customers", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="ReferralCustomer"/>s.
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the white label partner's API key.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-referral-customers">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="ReferralCustomer"/>s.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ReferralCustomerCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ReferralCustomerCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            ReferralCustomerCollection collection = await RequestAsync<ReferralCustomerCollection>(Method.Get, "referral_customers", cancellationToken, parameters);
            collection.Filters = Parameters.ReferralCustomer.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="ReferralCustomer"/>s.
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the white label partner's API key.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-referral-customers">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="ReferralCustomer"/>s.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ReferralCustomerCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ReferralCustomerCollection> All(Parameters.ReferralCustomer.All parameters, CancellationToken cancellationToken = default)
        {
            ReferralCustomerCollection collection = await RequestAsync<ReferralCustomerCollection>(Method.Get, "referral_customers", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ReferralCustomerCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-referral-customers">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="ReferralCustomerCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="ReferralCustomerCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ReferralCustomerCollection> GetNextPage(ReferralCustomerCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ReferralCustomerCollection, Parameters.ReferralCustomer.All>(async parameters => await All(parameters, cancellationToken), collection.ReferralCustomers, pageSize);

        /// <summary>
        ///     Add a credit card to a <see cref="ReferralCustomer"/>.
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the white label partner's API key.
        ///     This function requires the target <see cref="ReferralCustomer"/>'s API key as a parameter.
        ///     <a href="https://www.easypost.com/docs/api#add-payment-method-to-referral-user">Related API documentation</a>.
        /// </summary>
        /// <param name="referralApiKey">API key of the <see cref="ReferralCustomer"/>.</param>
        /// <param name="number">Credit card number.</param>
        /// <param name="expirationMonth">Expiration month of the credit card.</param>
        /// <param name="expirationYear">Expiration year of the credit card.</param>
        /// <param name="cvc">CVC of the credit card.</param>
        /// <param name="priority">Priority of the credit card.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The <see cref="PaymentMethod"/> that was added.</returns>
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Update]
        public async Task<PaymentMethod> AddCreditCardToUser(string referralApiKey, string number, int expirationMonth, int expirationYear, string cvc, PaymentMethod.Priority? priority = null, CancellationToken cancellationToken = default)
        {
            string? easypostStripeApiKey = await RetrieveEasypostStripeApiKey(cancellationToken);

            if (string.IsNullOrEmpty(easypostStripeApiKey))
            {
                throw new InternalServerError("Could not retrieve EasyPost Stripe API key.", 0);
            }

            // ReSharper disable once RedundantSuppressNullableWarningExpression
            string stripeToken = await CreateStripeToken(number, expirationMonth, expirationYear, cvc, easypostStripeApiKey!, cancellationToken);

#pragma warning disable IDE0046
            if (string.IsNullOrEmpty(stripeToken))
#pragma warning restore IDE0046
            {
                throw new ExternalApiError("Could not create Stripe token, please try again later.", 0);
            }

            return await CreateEasypostCreditCard(referralApiKey, stripeToken, priority ?? PaymentMethod.Priority.Primary, cancellationToken);
        }

        /// <summary>
        ///     Update a <see cref="ReferralCustomer"/>'s email.
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the white label partner's API key.
        ///     <a href="https://www.easypost.com/docs/api#update-a-referral-customer">Related API documentation</a>.
        /// </summary>
        /// <param name="referralId">The ID of the <see cref="ReferralCustomer"/> to update.</param>
        /// <param name="email">The new email address for the <see cref="ReferralCustomer"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>None.</returns>
        [CrudOperations.Update]
        public async Task UpdateReferralEmail(string referralId, string email, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "user", new Dictionary<string, object> { { "email", email } } } };

            await RequestAsync(Method.Put, $"referral_customers/{referralId}", cancellationToken, parameters);
        }

        #endregion

        /// <summary>
        ///     Submit Stripe credit card token to EasyPost for a specified <see cref="ReferralCustomer"/>.
        /// </summary>
        /// <param name="referralApiKey">API key of the <see cref="ReferralCustomer"/>.</param>
        /// <param name="stripeObjectId">Stripe token.</param>
        /// <param name="priority">Credit card priority.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The <see cref="PaymentMethod"/> that was created.</returns>
        private async Task<PaymentMethod> CreateEasypostCreditCard(string referralApiKey, string stripeObjectId, PaymentMethod.Priority priority, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                {
                    "credit_card",
                    new Dictionary<string, object>
                    {
                        { "stripe_object_id", stripeObjectId },
                        { "priority", priority.ToString().ToLowerInvariant() },
                    }
                },
            };

            // store the original client's API key
            string originalApiKey = Client.ApiKeyInUse;

            // set the client's API key to the referral customer's API key
            Client.ApiKeyInUse = referralApiKey;

            PaymentMethod paymentMethod;
            try
            {
                // Make request
                paymentMethod = await Client.RequestAsync<PaymentMethod>(Method.Post, "credit_cards", ApiVersion.Current, cancellationToken, parameters);
            }
            finally
            {
                // reset the client's API key to the original API key
                Client.ApiKeyInUse = originalApiKey;
            }

            return paymentMethod;
        }

        /// <summary>
        ///     Create a credit card token via Stripe.
        ///     <a href="https://stripe.com/docs/api/tokens">Related API documentation</a>.
        /// </summary>
        /// <param name="number">Credit card number.</param>
        /// <param name="expirationMonth">Expiration month of the credit card.</param>
        /// <param name="expirationYear">Expiration year of the credit card.</param>
        /// <param name="cvc">CVC of the credit card.</param>
        /// <param name="easypostStripeApiKey">EasyPost Stripe API key.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>Stripe token.</returns>
        /// <exception cref="Exception">When the request fails.</exception>
        private async Task<string> CreateStripeToken(string number, int expirationMonth, int expirationYear, string cvc, string easypostStripeApiKey, CancellationToken cancellationToken = default)
        {
            const string url = "https://api.stripe.com/v1/tokens";

            HttpRequestMessage request = new(Method.Post.HttpMethod, url);

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {easypostStripeApiKey}" },
                { "Accept", "application/x-www-form-urlencoded" },
            };

            foreach (KeyValuePair<string, string> header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            // add parameters
#pragma warning disable SA0001 // Nullability
#pragma warning disable CS8620 // Nullability
            Dictionary<string, string?> parameters = new Dictionary<string, string?>
            {
                { "card[number]", number },
                { "card[exp_month]", expirationMonth.ToString(CultureInfo.InvariantCulture) },
                { "card[exp_year]", expirationYear.ToString(CultureInfo.InvariantCulture) },
                { "card[cvc]", cvc },
            };
            request.Content = new FormUrlEncodedContent(parameters);
#pragma warning restore SA0001 // Nullability
#pragma warning restore CS8620 // Nullability

            HttpResponseMessage response = await Client.ExecuteRequest(request, cancellationToken);

            if (response.ReturnedError())
            {
                throw new ExternalApiError("Could not send card details to Stripe, please try again later.", (int)response.StatusCode);
            }

#if NETSTANDARD2_0 || NETCOREAPP3_1
            string content = await response.Content.ReadAsStringAsync();
#else
            string content = await response.Content.ReadAsStringAsync(cancellationToken);
#endif
            Dictionary<string, object> data = JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>(content);

            // Dispose of the request and response
            request.Dispose();
            response.Dispose();

            data.TryGetValue("id", out object? id);
            return id == null
                ? throw new ExternalApiError("Could not send card details to Stripe, please try again later.", (int)response.StatusCode)
                : (string)id;
        }

        /// <summary>
        ///     Retrieve EasyPost Stripe API key.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost Stripe API key.</returns>
        private async Task<string?> RetrieveEasypostStripeApiKey(CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> response = await RequestAsync<Dictionary<string, object>>(Method.Get, "partners/stripe_public_key", cancellationToken);

            response.TryGetValue("public_key", out object? easypostStripePublicKey);

            // ReSharper disable once MergeConditionalExpression
            return easypostStripePublicKey == null ? null : (string)easypostStripePublicKey;
        }
    }
}
