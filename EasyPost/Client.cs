using System.Net.Http;
using EasyPost._base;
using EasyPost.Services;
using EasyPost.Services.Beta;

namespace EasyPost
{
    public class Client : EasyPostClient
    {
        private readonly string _apiKey;

        private readonly HttpClient? _customHttpClient;

        public AddressService Address => GetService<AddressService>();

        public ApiKeyService ApiKey => GetService<ApiKeyService>();

        public BatchService Batch => GetService<BatchService>();

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
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        public Client(string apiKey, HttpClient? customHttpClient = null) : base(apiKey, ApiVersion.General, customHttpClient)
        {
            _apiKey = apiKey;
            _customHttpClient = customHttpClient;
        }
    }
}
