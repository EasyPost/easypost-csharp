using System.Net.Http;
using EasyPost.ApiCompatibility;
using EasyPost.Interfaces;
using EasyPost.Services.Beta;
using EasyPost.Services.V2;

namespace EasyPost.Clients
{
    public class Client : BaseClient
    {
        [ApiCompatibility(ApiVersion.V2)]
        public AddressService Addresses => GetService<AddressService>(nameof(Addresses));

        [ApiCompatibility(ApiVersion.V2)]
        public ApiKeyService ApiKeys => GetService<ApiKeyService>(nameof(ApiKeys));

        [ApiCompatibility(ApiVersion.V2)]
        public BatchService Batches => GetService<BatchService>(nameof(Batches));

        [ApiCompatibility(ApiVersion.V2)]
        public CarrierAccountService CarrierAccounts => GetService<CarrierAccountService>(nameof(CarrierAccounts));

        [ApiCompatibility(ApiVersion.V2)]
        public CarrierTypeService CarrierTypes => GetService<CarrierTypeService>(nameof(CarrierTypes));

        [ApiCompatibility(ApiVersion.V2)]
        public CustomsInfoService CustomsInfo => GetService<CustomsInfoService>(nameof(CustomsInfo));

        [ApiCompatibility(ApiVersion.V2)]
        public CustomsItemService CustomsItems => GetService<CustomsItemService>(nameof(CustomsItems));

        [ApiCompatibility(ApiVersion.Beta)]
        public EndShipperService EndShippers => GetService<EndShipperService>(nameof(EndShippers));

        [ApiCompatibility(ApiVersion.V2)]
        public EventService Events => GetService<EventService>(nameof(Events));

        [ApiCompatibility(ApiVersion.V2)]
        public InsuranceService Insurance => GetService<InsuranceService>(nameof(Insurance));

        [ApiCompatibility(ApiVersion.V2)]
        public OrderService Orders => GetService<OrderService>(nameof(Orders));

        [ApiCompatibility(ApiVersion.V2)]
        public ParcelService Parcels => GetService<ParcelService>(nameof(Parcels));

        [ApiCompatibility(ApiVersion.Beta)]
        public PartnerService Partners => GetService<PartnerService>(nameof(Partners));

        [ApiCompatibility(ApiVersion.V2)]
        public PaymentService Payments => GetService<PaymentService>(nameof(Payments));

        [ApiCompatibility(ApiVersion.V2)]
        public PickupService Pickups => GetService<PickupService>(nameof(Pickups));

        [ApiCompatibility(ApiVersion.V2)]
        public RateService Rates => GetService<RateService>(nameof(Rates));

        [ApiCompatibility(ApiVersion.V2)]
        public RefundService Refunds => GetService<RefundService>(nameof(Refunds));

        [ApiCompatibility(ApiVersion.V2)]
        public ReportService Reports => GetService<ReportService>(nameof(Reports));

        [ApiCompatibility(ApiVersion.V2)]
        public ScanFormService ScanForms => GetService<ScanFormService>(nameof(ScanForms));

        [ApiCompatibility(ApiVersion.V2)]
        public ShipmentService Shipments => GetService<ShipmentService>(nameof(Shipments));

        [ApiCompatibility(ApiVersion.V2)]
        public TrackerService Trackers => GetService<TrackerService>(nameof(Trackers));

        [ApiCompatibility(ApiVersion.V2)]
        public UserService Users => GetService<UserService>(nameof(Users));

        [ApiCompatibility(ApiVersion.V2)]
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
