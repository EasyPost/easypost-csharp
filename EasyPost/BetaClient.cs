using System.Net.Http;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Services.Beta;

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
         * API version that will be hit by setting the `overrideApiVersion` parameter to the desired version (defaults to ApiVersion.General otherwise).
         *
         * From a design perspective, this allows us to specifically redirect certain API calls to certain API versions on an individual basis.
         *
         * When you are migrating a service from beta to general, you should remove/change the overrideApiVersion parameter from each function that is being migrated.
         */

        // TODO: Add doc links

        /// <summary>
        ///     Access beta Carrier Metadata-related functionality.
        /// </summary>
        public CarrierMetadataService CarrierMetadata { get; }

        /// <summary>
        ///     Access beta Rate-related functionality.
        /// </summary>
        public RateService Rate { get; }

        /// <summary>
        ///     Access beta Referral Customer-related functionality.
        /// </summary>
        public ReferralCustomerService ReferralCustomer { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BetaClient"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="ClientConfiguration"/> for this client.</param>
        internal BetaClient(ClientConfiguration configuration)
            : base(configuration)
        {
            CarrierMetadata = new CarrierMetadataService(this);
            Rate = new RateService(this);
            ReferralCustomer = new ReferralCustomerService(this);
        }

        /// <inheritdoc cref="EasyPostClient.Dispose(bool)"/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects).

                // Dispose of the services
                CarrierMetadata.Dispose();
                Rate.Dispose();
                ReferralCustomer.Dispose();
            }

            // Free native resources (unmanaged objects) and override a finalizer below.

            // Dispose of the base client
            base.Dispose(disposing);
        }
    }
}
