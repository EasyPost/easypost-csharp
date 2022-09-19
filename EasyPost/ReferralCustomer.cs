using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost
{
    public class ReferralCustomer : BaseUser
    {
        /// <summary>
        ///    Create a Referral Customer object from a dictionary of parameters.
        ///    This function requires the Partner User's API key.
        /// </summary>
        /// <param name="parameters">Dictionary of the referral user parameters.</param>
        /// <returns>An EasyPost.ReferralCustomer instance.</returns>
        public static async Task<ReferralCustomer> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("referral_customers", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "user", parameters
                }
            });

            return await request.Execute<ReferralCustomer>();
        }

        /// <summary>
        ///     Update a Referral Customer object email.
        ///     This function requires the Partner User's API key.
        /// </summary>
        /// <param name="email">Email of the referral user to update.</param>
        /// <param name="userId">ID of the referral user to update.</param>
        /// <returns>True if update was successful, false otherwise.</returns>
        public static async Task<bool> UpdateEmail(string email, string userId)
        {
            Dictionary<string, object> wrappedParams = new Dictionary<string, object>
            {
                {
                    "user", new Dictionary<string, object>
                    {
                        {
                            "email", email
                        }
                    }
                }
            };

            Request request = new Request($"referral_customers/{userId}", Method.Put);
            request.AddParameters(wrappedParams);

            return await request.Execute();
        }

        /// <summary>
        ///     List all Referral Customer objects.
        ///     This function requires the Partner User's API key.
        /// </summary>
        /// <param name="parameters">Parameters for API call.</param>
        /// <returns>An EasyPost.ReferralCustomerCollection instance.</returns>
        public static async Task<ReferralCustomerCollection> All(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("referral_customers", Method.Get);
            request.AddParameters(parameters);

            return await request.Execute<ReferralCustomerCollection>();
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
        public static async Task<PaymentMethodObject> AddCreditCardToUser(string referralApiKey, string number, int expirationMonth, int expirationYear, string cvc, PaymentMethod.Priority? priority = null)
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
        ///     Retrieve EasyPost Stripe API key.
        /// </summary>
        /// <returns>EasyPost Stripe API key.</returns>
        private static async Task<string> RetrieveEasypostStripeApiKey()
        {
            Request request = new Request("partners/stripe_public_key", Method.Get);
            Dictionary<string, object> response = await request.Execute<Dictionary<string, object>>();
            if (!response.ContainsKey("public_key"))
            {
                return "";
            }

            return (string)response["public_key"];
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
        private static async Task<string> CreateStripeToken(string number, int expirationMonth, int expirationYear, string cvc, string easypostStripeApiKey)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("card[number]", number),
                new KeyValuePair<string, string>("card[exp_month]", expirationMonth.ToString()),
                new KeyValuePair<string, string>("card[exp_year]", expirationYear.ToString()),
                new KeyValuePair<string, string>("card[cvc]", cvc)
            };

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {easypostStripeApiKey}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.stripe.com/v1/tokens")
            {
                Content = new FormUrlEncodedContent(parameters)
            };

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
        ///     Submit Stripe credit card token to EasyPost.
        /// </summary>
        /// <param name="referralApiKey">API key of the referral user.</param>
        /// <param name="stripeObjectId">Stripe token.</param>
        /// <param name="priority">Credit card priority.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        private static async Task<PaymentMethodObject> CreateEasypostCreditCard(string referralApiKey, string stripeObjectId, PaymentMethod.Priority priority)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "credit_card", new Dictionary<string, object>
                    {
                        {
                            "stripe_object_id", stripeObjectId
                        },
                        {
                            "priority", priority.ToString().ToLower()
                        }
                    }
                }
            };

            // Custom override client with new API key
            Client client = new Client(new ClientConfiguration(referralApiKey));

            Request request = new Request("credit_cards", Method.Post);
            request.AddParameters(parameters);

            return await request.Execute<PaymentMethodObject>(clientOverride: client);
        }
    }
}
