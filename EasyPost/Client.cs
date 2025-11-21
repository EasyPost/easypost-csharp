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
        public AddressService Address => new AddressService(this);

        /// <summary>
        ///     Access API Key-related functionality.
        /// </summary>
        public ApiKeyService ApiKey => new ApiKeyService(this); // TODO: Recommend renaming. Because ApiKey is used as the name of the API key service, you have to access the actual configured API key via ApiKeyInUse

        /// <summary>
        ///     Access Batch-related functionality.
        /// </summary>
        public BatchService Batch => new BatchService(this);

        /// <summary>
        ///     Access beta functionality.
        /// </summary>
        public BetaClient Beta { get; }

        /// <summary>
        ///     Access Billing-related functionality.
        /// </summary>
        public BillingService Billing => new BillingService(this);

        /// <summary>
        ///     Access Carrier Account-related functionality.
        /// </summary>
        public CarrierAccountService CarrierAccount => new CarrierAccountService(this);

        /// <summary>
        ///     Access Carrier Metadata-related functionality.
        /// </summary>
        public CarrierMetadataService CarrierMetadata => new CarrierMetadataService(this);

        /// <summary>
        ///     Access Carrier Type-related functionality.
        /// </summary>
        public CarrierTypeService CarrierType => new CarrierTypeService(this);

        /// <summary>
        ///     Access Claim-related functionality.
        /// </summary>
        public ClaimService Claim => new ClaimService(this);

        /// <summary>
        ///     Access CustomerPortal-related functionality.
        /// </summary>
        public CustomerPortalService CustomerPortal => new CustomerPortalService(this);

        /// <summary>
        ///     Access Customs Info-related functionality.
        /// </summary>
        public CustomsInfoService CustomsInfo => new CustomsInfoService(this);

        /// <summary>
        ///     Access Customs Item-related functionality.
        /// </summary>
        public CustomsItemService CustomsItem => new CustomsItemService(this);

        /// <summary>
        ///     Access Embeddable-related functionality.
        /// </summary>
        public EmbeddableService Embeddable => new EmbeddableService(this);

        /// <summary>
        ///     Access EndShipper-related functionality.
        /// </summary>
        public EndShipperService EndShipper => new EndShipperService(this);

        /// <summary>
        ///     Access Event-related functionality.
        /// </summary>
        public EventService Event => new EventService(this);

        /// <summary>
        ///     Access Insurance-related functionality.
        /// </summary>
        public InsuranceService Insurance => new InsuranceService(this);

        /// <summary>
        ///     Access Luma-related functionality.
        /// </summary>
        public LumaService Luma => new LumaService(this);

        /// <summary>
        ///     Access Order-related functionality.
        /// </summary>
        public OrderService Order => new OrderService(this);

        /// <summary>
        ///     Access Parcel-related functionality.
        /// </summary>
        public ParcelService Parcel => new ParcelService(this);

        /// <summary>
        ///     Access Pickup-related functionality.
        /// </summary>
        public PickupService Pickup => new PickupService(this);

        /// <summary>
        ///     Access Rate-related functionality.
        /// </summary>
        public RateService Rate => new RateService(this);

        /// <summary>
        ///     Access Referral Customer-related functionality.
        /// </summary>
        public ReferralCustomerService ReferralCustomer => new ReferralCustomerService(this);

        /// <summary>
        ///     Access Refund-related functionality.
        /// </summary>
        public RefundService Refund => new RefundService(this);

        /// <summary>
        ///     Access Report-related functionality.
        /// </summary>
        public ReportService Report => new ReportService(this);

        /// <summary>
        ///     Access ScanForm-related functionality.
        /// </summary>
        public ScanFormService ScanForm => new ScanFormService(this);

        /// <summary>
        ///     Access Shipment-related functionality.
        /// </summary>
        public ShipmentService Shipment => new ShipmentService(this);

        /// <summary>
        ///     Access SmartRate-related functionality.
        /// </summary>
        public SmartRateService SmartRate => new SmartRateService(this);

        /// <summary>
        ///     Access Tracker-related functionality.
        /// </summary>
        public TrackerService Tracker => new TrackerService(this);

        /// <summary>
        ///     Access User-related functionality.
        /// </summary>
        public UserService User => new UserService(this);

        /// <summary>
        ///     Access Webhook-related functionality.
        /// </summary>
        public WebhookService Webhook => new WebhookService(this);

        /// <summary>
        ///     Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="ClientConfiguration"/> for this client.</param>
        public Client(ClientConfiguration configuration)
            : base(configuration)
        {
            Beta = new BetaClient(configuration);
        }
    }
}
