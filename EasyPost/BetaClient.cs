using System.Net.Http;
using EasyPost._base;
using EasyPost.Http;
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

        public ReferralCustomerService ReferralCustomer { get; }

        public RateService Rate { get; }

        public CarrierMetadataService CarrierMetadata { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BetaClient"/> class.
        /// </summary>
        /// <param name="configuration">Configuration for this client.</param>
        internal BetaClient(ClientConfiguration configuration)
            : base(configuration)
        {
            ReferralCustomer = new ReferralCustomerService(this);
            Rate = new RateService(this);
            CarrierMetadata = new CarrierMetadataService(this);
        }
    }
}
