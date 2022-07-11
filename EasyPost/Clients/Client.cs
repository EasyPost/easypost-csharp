using System.Net.Http;
using EasyPost.ApiCompatibility;
using EasyPost.Interfaces;
using EasyPost.Services.Beta;
using EasyPost.Services.V2;

namespace EasyPost.Clients
{
    public class Client : EasyPostClient
    {
        [ApiCompatibility(ApiVersion.Latest)]
        public AddressService Addresses => GetService<AddressService>(nameof(Addresses));

        [ApiCompatibility(ApiVersion.Latest)]
        public ApiKeyService ApiKeys => GetService<ApiKeyService>(nameof(ApiKeys));

        [ApiCompatibility(ApiVersion.Latest)]
        public BatchService Batches => GetService<BatchService>(nameof(Batches));

        [ApiCompatibility(ApiVersion.Latest)]
        public BillingService Billing => GetService<BillingService>(nameof(Billing));

        [ApiCompatibility(ApiVersion.Latest)]
        public CarrierAccountService CarrierAccounts => GetService<CarrierAccountService>(nameof(CarrierAccounts));

        [ApiCompatibility(ApiVersion.Latest)]
        public CarrierTypeService CarrierTypes => GetService<CarrierTypeService>(nameof(CarrierTypes));

        [ApiCompatibility(ApiVersion.Latest)]
        public CustomsInfoService CustomsInfo => GetService<CustomsInfoService>(nameof(CustomsInfo));

        [ApiCompatibility(ApiVersion.Latest)]
        public CustomsItemService CustomsItems => GetService<CustomsItemService>(nameof(CustomsItems));

        [ApiCompatibility(ApiVersion.Beta)]
        public EndShipperService EndShippers => GetService<EndShipperService>(nameof(EndShippers));

        [ApiCompatibility(ApiVersion.Latest)]
        public EventService Events => GetService<EventService>(nameof(Events));

        [ApiCompatibility(ApiVersion.Latest)]
        public InsuranceService Insurance => GetService<InsuranceService>(nameof(Insurance));

        [ApiCompatibility(ApiVersion.Latest)]
        public OrderService Orders => GetService<OrderService>(nameof(Orders));

        [ApiCompatibility(ApiVersion.Latest)]
        public ParcelService Parcels => GetService<ParcelService>(nameof(Parcels));

        [ApiCompatibility(ApiVersion.Beta)]
        public PartnerService Partners => GetService<PartnerService>(nameof(Partners));

        [ApiCompatibility(ApiVersion.Latest)]
        public PickupService Pickups => GetService<PickupService>(nameof(Pickups));

        [ApiCompatibility(ApiVersion.Latest)]
        public RateService Rates => GetService<RateService>(nameof(Rates));

        [ApiCompatibility(ApiVersion.Latest)]
        public RefundService Refunds => GetService<RefundService>(nameof(Refunds));

        [ApiCompatibility(ApiVersion.Latest)]
        public ReportService Reports => GetService<ReportService>(nameof(Reports));

        [ApiCompatibility(ApiVersion.Latest)]
        public ScanFormService ScanForms => GetService<ScanFormService>(nameof(ScanForms));

        [ApiCompatibility(ApiVersion.Latest)]
        public ShipmentService Shipments => GetService<ShipmentService>(nameof(Shipments));

        [ApiCompatibility(ApiVersion.Latest)]
        public TrackerService Trackers => GetService<TrackerService>(nameof(Trackers));

        [ApiCompatibility(ApiVersion.Latest)]
        public UserService Users => GetService<UserService>(nameof(Users));

        [ApiCompatibility(ApiVersion.Latest)]
        public WebhookService Webhooks => GetService<WebhookService>(nameof(Webhooks));

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="apiVersion">API version to use this client with.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        public Client(string apiKey, ApiVersion apiVersion, HttpClient? customHttpClient = null) : base(apiKey, apiVersion, customHttpClient)
        {
        }
    }
}
