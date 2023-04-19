using System;
using System.Net.Http;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Services.Beta;
using RateService = EasyPost.Services.Beta.RateService;

namespace EasyPost
{
    /// <summary>
    ///     Access beta EasyPost API endpoints.
    /// </summary>
    public class BetaClient : EasyPostClient, IDisposable
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

        // TODO: ^ Undo above, set api version as a client config and lock this client (and its services) to that version

        public ReferralService Referral { get; }

        public RateService Rate { get; }

        public CarrierMetadataService CarrierMetadata { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BetaClient"/> class.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client.</param>
        /// <param name="customHttpClient">Custom HttpClient to use if needed.</param>
        /// <param name="connectTimeoutMilliseconds">Connect timeout in milliseconds for API requests.</param>
        [Obsolete("This constructor is deprecated and will be removed in a future release. Please use the constructor that takes a ClientConfiguration object instead.")]
        internal BetaClient(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null, int? connectTimeoutMilliseconds = null)
            : base(apiKey, baseUrl, customHttpClient, connectTimeoutMilliseconds)
        {
            // TODO: Remove this constructor and migrate users to the constructor that takes a ClientConfiguration object instead.

            // We initialize the services here since initializing a new one on each property call is expensive.
            Referral = new ReferralService(this);
            Rate = new RateService(this);
            CarrierMetadata = new CarrierMetadataService(this);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BetaClient"/> class.
        /// </summary>
        /// <param name="configuration">Configuration to use with this client.</param>
#pragma warning disable IDE0021 // Ignoring since more properties will be added during construction in the future.
        public BetaClient(ClientConfiguration configuration)
            : base(configuration)
#pragma warning restore IDE0021
        {
            // We initialize the services here since initializing a new one on each property call is expensive.
            Referral = new ReferralService(this);
            Rate = new RateService(this);
            CarrierMetadata = new CarrierMetadataService(this);
        }

        public new void Dispose()
        {
            // Dispose of the base client
            base.Dispose();

            // Dispose of the services
            Referral.Dispose();
            Rate.Dispose();
            CarrierMetadata.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
