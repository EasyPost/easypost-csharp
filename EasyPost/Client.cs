using System.Net.Http;
using EasyPost._base;
using EasyPost.Services;

namespace EasyPost
{
    public class Client : EasyPostClient
    {
        public AddressService Address { get; }

        public ApiKeyService ApiKey { get; }

        public BatchService Batch { get; }

        public BetaClient Beta { get; }

        public BillingService Billing { get; }

        public CarrierAccountService CarrierAccount { get; }

        public CarrierTypeService CarrierType { get; }

        public CustomsInfoService CustomsInfo { get; }

        public CustomsItemService CustomsItem { get; }

        public EndShipperService EndShipper { get; }

        public EventService Event { get; }

        public InsuranceService Insurance { get; }

        public OrderService Order { get; }

        public ParcelService Parcel { get; }

        public PartnerService Partner { get; }

        public PickupService Pickup { get; }

        public RateService Rate { get; }

        public RefundService Refund { get; }

        public ReportService Report { get; }

        public ScanFormService ScanForm { get; }

        public ShipmentService Shipment { get; }

        public TrackerService Tracker { get; }

        public UserService User { get; }

        public WebhookService Webhook { get; }

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. Must include API version.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed.</param>
        public Client(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null) : base(apiKey, baseUrl, customHttpClient)
        {
            // We go ahead and initialize the Beta client internally here as well, since initializing a new one on each property call is expensive and causes lockups with the HttpClient library.
            Beta = new BetaClient(apiKey, baseUrl, customHttpClient);

            // We also initialize the services here, since they are all singletons and we don't want to create a new one each time.
            Address = GetService<AddressService>();
            ApiKey = GetService<ApiKeyService>();
            Batch = GetService<BatchService>();
            Billing = GetService<BillingService>();
            CarrierAccount = GetService<CarrierAccountService>();
            CarrierType = GetService<CarrierTypeService>();
            CustomsInfo = GetService<CustomsInfoService>();
            CustomsItem = GetService<CustomsItemService>();
            EndShipper = GetService<EndShipperService>();
            Event = GetService<EventService>();
            Insurance = GetService<InsuranceService>();
            Order = GetService<OrderService>();
            Parcel = GetService<ParcelService>();
            Partner = GetService<PartnerService>();
            Pickup = GetService<PickupService>();
            Rate = GetService<RateService>();
            Refund = GetService<RefundService>();
            Report = GetService<ReportService>();
            ScanForm = GetService<ScanFormService>();
            Shipment = GetService<ShipmentService>();
            Tracker = GetService<TrackerService>();
            User = GetService<UserService>();
            Webhook = GetService<WebhookService>();
        }
    }
}
