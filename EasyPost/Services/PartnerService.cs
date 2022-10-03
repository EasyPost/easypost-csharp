using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services
{
    public class PartnerService : EasyPostService
    {
        internal PartnerService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a referral user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the referral user with. Valid pairs:
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
        ///     This function requires the Partner User's API key.
        /// </summary>
        /// <param name="parameters">Parameters for API call.</param>
        /// <returns>An EasyPost.ReferralCustomerCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<ReferralCustomerCollection> All(Dictionary<string, object>? parameters = null)
        {
            return await List<ReferralCustomerCollection>("referral_customers", parameters);
        }

        /// <summary>
        ///     Add a credit card to a Referral Customer.
        ///     This function requires the Referral User's API key.
        /// </summary>
        /// <param name="referralApiKey">API key of the referral user.</param>
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
            string easypostStripeApiKey = await RetrieveEasypostStripeApiKey();
            string stripeToken;

            try
            {
                stripeToken = await CreateStripeToken(number, expirationMonth, expirationYear, cvc, easypostStripeApiKey);
            }
            catch (Exception e)
            {
                throw new Exception("Could not send card details to Stripe, please try again later.", e);
            }

            return await CreateEasypostCreditCard(referralApiKey, stripeToken, priority ?? PaymentMethod.Priority.Primary);
        }

        /// <summary>
        ///     Update a Referral Customer object email.
        ///     This function requires the Partner User's API key.
        /// </summary>
        /// <param name="referralId">ID of the referral user to update.</param>
        /// <param name="email">Email of the referral user to update.</param>
        [CrudOperations.Update]
        public async Task UpdateReferralEmail(string referralId, string email)
        {
            var parameters = new Dictionary<string, object> { { "user", new Dictionary<string, object> { { "email", email } } } };
            // NOTE: This is a PATCH request, not a PUT request.
            await UpdateNoResponse($"referral_customers/{referralId}", parameters);
        }

        #endregion

        /// <summary>
        ///     Submit Stripe credit card token to EasyPost.
        /// </summary>
        /// <param name="referralApiKey">API key of the referral user.</param>
        /// <param name="stripeObjectId">Stripe token.</param>
        /// <param name="priority">Credit card priority.</param>
        /// <returns>An EasyPost.PaymentMethod instance.</returns>
        private async Task<PaymentMethod> CreateEasypostCreditCard(string referralApiKey, string stripeObjectId, PaymentMethod.Priority priority)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "credit_card", new Dictionary<string, object>
                    {
                        { "stripe_object_id", stripeObjectId },
                        { "priority", priority.ToString().ToLower() }
                    }
                }
            };

            // Custom override client with new API key
            Client tempClient = Client!.Clone<Client>(overrideApiKey: referralApiKey);
            return await tempClient.Request<PaymentMethod>(Method.Post, "credit_cards", ApiVersion.Current, parameters);
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
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {easypostStripeApiKey}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            const string url = "https://api.stripe.com/v1/tokens";

#if NET5_0
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string?, string?>>
                {
                    new KeyValuePair<string?, string?>("card[number]", number),
                    new KeyValuePair<string?, string?>("card[exp_month]", expirationMonth.ToString()),
                    new KeyValuePair<string?, string?>("card[exp_year]", expirationYear.ToString()),
                    new KeyValuePair<string?, string?>("card[cvc]", cvc)
                })
            };
#else
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("card[number]", number),
                    new KeyValuePair<string, string>("card[exp_month]", expirationMonth.ToString()),
                    new KeyValuePair<string, string>("card[exp_year]", expirationYear.ToString()),
                    new KeyValuePair<string, string>("card[cvc]", cvc)
                })
            };
#endif

            HttpResponseMessage response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            Dictionary<string, object> responseDictionary = JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>(responseString);
            if (!responseDictionary.ContainsKey("id"))
            {
                throw new Exception("Could not create Stripe token, please try again later.");
            }

            return (string)responseDictionary["id"];
        }

        /// <summary>
        ///     Retrieve EasyPost Stripe API key.
        /// </summary>
        /// <returns>EasyPost Stripe API key.</returns>
        private async Task<string> RetrieveEasypostStripeApiKey()
        {
            Dictionary<string, object> response = await Get<Dictionary<string, object>>("partners/stripe_public_key");
            if (!response.ContainsKey("public_key"))
            {
                return "";
            }

            return (string)response["public_key"];
        }
    }
}
