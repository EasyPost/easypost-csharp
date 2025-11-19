using EasyPost._base;

namespace EasyPost
{
    /// <summary>
    ///     Access beta EasyPost API functionality.
    /// </summary>
    public class BetaClient : EasyPostClient
    {
        /*
         * NOTE to maintainer: This class stores all beta features. Users can only access this by calling `myClient.Beta`. It cannot be initialized directly.
         *
         * However, this class doesn't actually dictate that these services will use the beta API endpoint.
         *
         * In each function in each service (beta or not), when the HTTP call is being made, we can optionally override the
         * API version that will be hit by setting the `overrideApiVersion` parameter to the desired version (defaults to ApiVersion.Current otherwise).
         *
         * From a design perspective, this allows us to specifically redirect certain API calls to certain API versions on an individual basis.
         *
         * When you are migrating a service from beta to general, you should remove/change the overrideApiVersion parameter from each function that is being migrated.
         */

        // TODO: Add doc links

        /// <summary>
        ///     Access beta Rate-related functionality.
        /// </summary>
        public Services.Beta.RateService Rate { get; }

        /// <summary>
        ///     Access beta Referral Customer-related functionality.
        /// </summary>
        public Services.Beta.ReferralCustomerService ReferralCustomer { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BetaClient"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="ClientConfiguration"/> for this client.</param>
        internal BetaClient(ClientConfiguration configuration)
            : base(configuration)
        {
            Rate = new Services.Beta.RateService(this);
            ReferralCustomer = new Services.Beta.ReferralCustomerService(this);
        }
    }
}
