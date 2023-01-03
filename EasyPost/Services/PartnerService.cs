using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.API;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services
{
    public class PartnerService : EasyPostService
    {
        internal PartnerService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a referral customer for the account associated with the api_key specified.
        ///     This function should be called against a Client configured with the white label partner's API key.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the referral customer with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     * {"email", string} Email on the account.
        ///     * {"phone", string} Phone number on the account.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Create]
        public async Task<ReferralCustomer> CreateReferral(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("user");
            return await Create<ReferralCustomer>("referral_customers", parameters);
        }

        /// <summary>
        ///     List all Referral Customer objects.
        ///     This function should be called against a Client configured with the white label partner's API key.
        /// </summary>
        /// <param name="parameters">Parameters for API call.</param>
        /// <returns>An EasyPost.ReferralCustomerCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<ReferralCustomerCollection> All(Dictionary<string, object>? parameters = null) => await List<ReferralCustomerCollection>("referral_customers", parameters);

        /// <summary>
        ///     Add a credit card to a Referral Customer.
        ///     This function should be called against a Client configured with the white label partner's API key.
        ///     This function requires the target Referral Customer's API key as a parameter.
        /// </summary>
        /// <param name="referralApiKey">API key of the referral customer.</param>
        /// <param name="number">Credit card number.</param>
        /// <param name="expirationMonth">Expiration month of the credit card.</param>
        /// <param name="expirationYear">Expiration year of the credit card.</param>
        /// <param name="cvc">CVC of the credit card.</param>
        /// <param name="priority">Priority of the credit card.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        /// <exception cref="Exception">When the request fails.</exception>
        [CrudOperations.Update]
        public async Task<PaymentMethod> AddCreditCardToUser(string referralApiKey, string number, int expirationMonth, int expirationYear, string cvc, PaymentMethod.Priority? priority = null)
        {
            string? easypostStripeApiKey = await RetrieveEasypostStripeApiKey();

            if (string.IsNullOrEmpty(easypostStripeApiKey))
            {
                throw new InternalServerError("Could not retrieve EasyPost Stripe API key.", 0);
            }

            // ReSharper disable once RedundantSuppressNullableWarningExpression
            string stripeToken = await CreateStripeToken(number, expirationMonth, expirationYear, cvc, easypostStripeApiKey!);

#pragma warning disable IDE0046
            if (string.IsNullOrEmpty(stripeToken))
#pragma warning restore IDE0046
            {
                throw new ExternalApiError("Could not create Stripe token, please try again later.", 0);
            }

            return await CreateEasypostCreditCard(referralApiKey, stripeToken, priority ?? PaymentMethod.Priority.Primary);
        }

        /// <summary>
        ///     Update a Referral Customer object email.
        ///     This function should be called against a Client configured with the white label partner's API key.
        /// </summary>
        /// <param name="referralId">ID of the referral customer to update.</param>
        /// <param name="email">Email of the referral customer to update.</param>
        /// <returns>A Task to update a referral's email.</returns>
        [CrudOperations.Update]
        public async Task UpdateReferralEmail(string referralId, string email)
        {
            Dictionary<string, object> parameters = new() { { "user", new Dictionary<string, object> { { "email", email } } } };

            // NOTE: This is a PATCH request, not a PUT request.
            await UpdateNoResponse($"referral_customers/{referralId}", parameters);
        }

        #endregion

        /// <summary>
        ///     Submit Stripe credit card token to EasyPost for a specified Referral Customer.
        /// </summary>
        /// <param name="referralApiKey">API key of the referral customer.</param>
        /// <param name="stripeObjectId">Stripe token.</param>
        /// <param name="priority">Credit card priority.</param>
        /// <returns>An EasyPost.PaymentMethod instance.</returns>
        private async Task<PaymentMethod> CreateEasypostCreditCard(string referralApiKey, string stripeObjectId, PaymentMethod.Priority priority)
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

            // Store the old API key
            string oldApiKey = Client!.Configuration.ApiKey;

            // Change API key temporarily to referral customer's API key.
            Client.Configuration.ApiKey = referralApiKey;

            PaymentMethod paymentMethod;
            try
            {
                // Make request
                paymentMethod = await Client.Request<PaymentMethod>(Method.Post, "credit_cards", ApiVersion.Current, parameters);
            }
            finally
            {
                Client.Configuration.ApiKey = oldApiKey;
            }

            return paymentMethod;
        }

        /// <summary>
        ///     Create a credit card token via Stripe.
        /// </summary>
        /// <param name="number">Credit card number.</param>
        /// <param name="expirationMonth">Expiration month of the credit card.</param>
        /// <param name="expirationYear">Expiration year of the credit card.</param>
        /// <param name="cvc">CVC of the credit card.</param>
        /// <param name="easypostStripeApiKey">EasyPost Stripe API key.</param>
        /// <returns>Stripe token.</returns>
        /// <exception cref="Exception">When the request fails.</exception>
        private async Task<string> CreateStripeToken(string number, int expirationMonth, int expirationYear, string cvc, string easypostStripeApiKey)
        {
            const string url = "https://api.stripe.com/v1/tokens";

            RestRequest request = new(url, Method.Post);
            request.AddHeader("Authorization", $"Bearer {easypostStripeApiKey}");
            request.AddHeader("Accept", "application/x-www-form-urlencoded");
            request.AddParameter("card[number]", number);
            request.AddParameter("card[exp_month]", expirationMonth.ToString(CultureInfo.InvariantCulture));
            request.AddParameter("card[exp_year]", expirationYear.ToString(CultureInfo.InvariantCulture));
            request.AddParameter("card[cvc]", cvc);

            RestResponse<Dictionary<string, object>> response = await Client!.ExecuteRequest<Dictionary<string, object>>(request);

            if (response.ReturnedError() || response.Data == null)
            {
                throw new ExternalApiError("Could not send card details to Stripe, please try again later.", (int)response.StatusCode);
            }

            Dictionary<string, object>? data = response.Data;

            data.TryGetValue("id", out object? id);
            return id == null
                ? throw new ExternalApiError("Could not send card details to Stripe, please try again later.", (int)response.StatusCode)
                : (string)id;
        }

        /// <summary>
        ///     Retrieve EasyPost Stripe API key.
        /// </summary>
        /// <returns>EasyPost Stripe API key.</returns>
        private async Task<string?> RetrieveEasypostStripeApiKey()
        {
            Dictionary<string, object> response = await Get<Dictionary<string, object>>("partners/stripe_public_key");

            response.TryGetValue("public_key", out object? easypostStripePublicKey);

            // ReSharper disable once MergeConditionalExpression
            return easypostStripePublicKey == null ? null : (string)easypostStripePublicKey;
        }
    }
}
