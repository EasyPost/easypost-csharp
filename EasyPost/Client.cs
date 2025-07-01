using EasyPost._base;
using EasyPost.Services;

namespace EasyPost
{
    /// <summary>
    ///     Access EasyPost API functionality.
    /// </summary>
    public class Client : EasyPostClient
    {
        /// <summary>
        ///     Access Address-related functionality.
        /// </summary>
        public AddressService Address { get; }

        /// <summary>
        ///     Access API Key-related functionality.
        /// </summary>
        public ApiKeyService ApiKey { get; } // TODO: Recommend renaming. Because ApiKey is used as the name of the API key service, you have to access the actual configured API key via ApiKeyInUse

        /// <summary>
        ///     Access Batch-related functionality.
        /// </summary>
        public BatchService Batch { get; }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        ///     Access beta functionality.
        /// </summary>
        public BetaClient Beta { get; }

        /// <summary>
        ///     Access Billing-related functionality.
        /// </summary>
        public BillingService Billing { get; }

        /// <summary>
        ///     Access Carrier Account-related functionality.
        /// </summary>
        public CarrierAccountService CarrierAccount { get; }

        /// <summary>
        ///     Access Carrier Metadata-related functionality.
        /// </summary>
        public CarrierMetadataService CarrierMetadata { get; }

        /// <summary>
        ///     Access Carrier Type-related functionality.
        /// </summary>
        public CarrierTypeService CarrierType { get; }

        /// <summary>
        ///     Access Claim-related functionality.
        /// </summary>
        public ClaimService Claim { get; }

        /// <summary>
        ///     Access Customs Info-related functionality.
        /// </summary>
        public CustomsInfoService CustomsInfo { get; }

        /// <summary>
        ///     Access Customs Item-related functionality.
        /// </summary>
        public CustomsItemService CustomsItem { get; }

        /// <summary>
        ///     Access EndShipper-related functionality.
        /// </summary>
        public EndShipperService EndShipper { get; }

        /// <summary>
        ///     Access Event-related functionality.
        /// </summary>
        public EventService Event { get; }

        /// <summary>
        ///     Access Insurance-related functionality.
        /// </summary>
        public InsuranceService Insurance { get; }

        /// <summary>
        ///     Access Luma-related functionality.
        /// </summary>
        public LumaService Luma { get; }

        /// <summary>
        ///     Access Order-related functionality.
        /// </summary>
        public OrderService Order { get; }

        /// <summary>
        ///     Access Parcel-related functionality.
        /// </summary>
        public ParcelService Parcel { get; }

        /// <summary>
        ///     Access Pickup-related functionality.
        /// </summary>
        public PickupService Pickup { get; }

        /// <summary>
        ///     Access Rate-related functionality.
        /// </summary>
        public RateService Rate { get; }

        /// <summary>
        ///     Access Referral Customer-related functionality.
        /// </summary>
        public ReferralCustomerService ReferralCustomer { get; }

        /// <summary>
        ///     Access Refund-related functionality.
        /// </summary>
        public RefundService Refund { get; }

        /// <summary>
        ///     Access Report-related functionality.
        /// </summary>
        public ReportService Report { get; }

        /// <summary>
        ///     Access ScanForm-related functionality.
        /// </summary>
        public ScanFormService ScanForm { get; }

        /// <summary>
        ///     Access Shipment-related functionality.
        /// </summary>
        public ShipmentService Shipment { get; }

        /// <summary>
        ///     Access SmartRate-related functionality.
        /// </summary>
        public SmartRateService SmartRate { get; }

        /// <summary>
        ///     Access Tracker-related functionality.
        /// </summary>
        public TrackerService Tracker { get; }

        /// <summary>
        ///     Access User-related functionality.
        /// </summary>
        public UserService User { get; }

        /// <summary>
        ///     Access Webhook-related functionality.
        /// </summary>
        public WebhookService Webhook { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="ClientConfiguration"/> for this client.</param>
#pragma warning disable IDE0021 // Ignoring since more properties will be added during construction in the future.
        public Client(ClientConfiguration configuration)
            : base(configuration)
#pragma warning restore IDE0021
        {
            // We initialize the services here since initializing a new one on each property call is expensive.
            Address = new AddressService(this);
            ApiKey = new ApiKeyService(this);
            Batch = new BatchService(this);
            Billing = new BillingService(this);
            CarrierAccount = new CarrierAccountService(this);
            CarrierMetadata = new CarrierMetadataService(this);
            CarrierType = new CarrierTypeService(this);
            Claim = new ClaimService(this);
            CustomsInfo = new CustomsInfoService(this);
            CustomsItem = new CustomsItemService(this);
            EndShipper = new EndShipperService(this);
            Event = new EventService(this);
            Insurance = new InsuranceService(this);
            Luma = new LumaService(this);
            Order = new OrderService(this);
            Parcel = new ParcelService(this);
            Pickup = new PickupService(this);
            Rate = new RateService(this);
            ReferralCustomer = new ReferralCustomerService(this);
            Refund = new RefundService(this);
            Report = new ReportService(this);
            ScanForm = new ScanFormService(this);
            Shipment = new ShipmentService(this);
            SmartRate = new SmartRateService(this);
            Tracker = new TrackerService(this);
            User = new UserService(this);
            Webhook = new WebhookService(this);

            // We go ahead and initialize the Beta client internally here as well
            Beta = new BetaClient(configuration);
        }

        /// <inheritdoc cref="EasyPostClient.Dispose(bool)"/>
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;

            // Dispose managed state (managed objects)

            // "disposing" inherently true when called from Dispose(), so don't need to pass it in.

            // Attempt to dispose of the services (some may already be disposed)
            Address.Dispose();
            ApiKey.Dispose();
            Batch.Dispose();
            Billing.Dispose();
            CarrierAccount.Dispose();
            CarrierMetadata.Dispose();
            CarrierType.Dispose();
            Claim.Dispose();
            CustomsInfo.Dispose();
            CustomsItem.Dispose();
            EndShipper.Dispose();
            Event.Dispose();
            Insurance.Dispose();
            Luma.Dispose();
            Order.Dispose();
            Parcel.Dispose();
            Pickup.Dispose();
            Rate.Dispose();
            ReferralCustomer.Dispose();
            Refund.Dispose();
            Report.Dispose();
            ScanForm.Dispose();
            Shipment.Dispose();
            SmartRate.Dispose();
            Tracker.Dispose();
            User.Dispose();
            Webhook.Dispose();

            // Attempt to dispose of the Beta client (may already be disposed)
            Beta.Dispose();

            // Dispose of the base client
            base.Dispose(disposing);
        }
    }
}
