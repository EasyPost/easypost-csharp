using System.Net.Http;
using EasyPost._base;
using EasyPost.Services;

namespace EasyPost
{
    public class Client : EasyPostClient
    {
        public AddressService Address => GetService<AddressService>();

        public ApiKeyService ApiKey => GetService<ApiKeyService>();

        public BatchService Batch => GetService<BatchService>();

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public BetaClient Beta { get; }

        public BillingService Billing => GetService<BillingService>();

        public CarrierAccountService CarrierAccount => GetService<CarrierAccountService>();

        public CarrierTypeService CarrierType => GetService<CarrierTypeService>();

        public CustomsInfoService CustomsInfo => GetService<CustomsInfoService>();

        public CustomsItemService CustomsItem => GetService<CustomsItemService>();

        public EndShipperService EndShipper => GetService<EndShipperService>();

        public EventService Event => GetService<EventService>();

        public InsuranceService Insurance => GetService<InsuranceService>();

        public OrderService Order => GetService<OrderService>();

        public ParcelService Parcel => GetService<ParcelService>();

        public PartnerService Partner => GetService<PartnerService>();

        public PickupService Pickup => GetService<PickupService>();

        public RateService Rate => GetService<RateService>();

        public RefundService Refund => GetService<RefundService>();

        public ReportService Report => GetService<ReportService>();

        public ScanFormService ScanForm => GetService<ScanFormService>();

        public ShipmentService Shipment => GetService<ShipmentService>();

        public TrackerService Tracker => GetService<TrackerService>();

        public UserService User => GetService<UserService>();

        public WebhookService Webhook => GetService<WebhookService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="baseUrl">Base URL to use with this client. Must include API version.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed.</param>
#pragma warning disable IDE0021 // Ignoring since more properties will be added during construction in the future.
        public Client(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null)
            : base(apiKey, baseUrl, customHttpClient)
#pragma warning restore IDE0021
        {
            // We go ahead and initialize the Beta client internally here as well, since initializing a new one on each property call is expensive and causes lockups with the HttpClient library.
            Beta = new BetaClient(apiKey, baseUrl, customHttpClient);
        }
    }
}
