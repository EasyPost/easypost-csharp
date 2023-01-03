using System.Net.Http;
using EasyPost._base;
using EasyPost.Services.Beta;

namespace EasyPost
{
    /// <summary>
    ///     Access beta EasyPost API endpoints.
    /// </summary>
    public class BetaClient : EasyPostClient
    {
        /*
         * NOTE to maintainer: This class stores all beta features. Users can only access this by calling `myClient.Beta`. It cannot be initialized directly.
         *
         * However, this class doesn't actually dictate that these services will use the beta API endpoint.
         *
         * In each function in each service (beta or not), when the HTTP call is being made, we can optionally override the
         * API version that will be hit by setting the `overrideApiVersion` parameter to the desired version (defaults to ApiVersion.General otherwise).
         *
         * From a design perspective, this allows us to specifically redirect certain API calls to certain API versions on an individual basis.
         *
         * When you are migrating a service from beta to general, you should remove/change the overrideApiVersion parameter from each function that is being migrated.
         */

        public ReferralService Referral { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BetaClient"/> class.
        ///     Constructor for the EasyPost beta client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed.</param>
        internal BetaClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null)
            : base(apiKey, baseUrl, customHttpClient)
        {
            Referral = new ReferralService(this);
        }
    }
}
