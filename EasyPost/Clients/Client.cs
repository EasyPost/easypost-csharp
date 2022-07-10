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
        public AddressService AddressesEasyPost => GetService<AddressService>(nameof(AddressesEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public ApiKeyService ApiKeysEasyPost => GetService<ApiKeyService>(nameof(ApiKeysEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public BatchService BatchesEasyPost => GetService<BatchService>(nameof(BatchesEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public BillingService BillingEasyPost => GetService<BillingService>(nameof(BillingEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public CarrierAccountService CarrierAccountsEasyPost => GetService<CarrierAccountService>(nameof(CarrierAccountsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public CarrierTypeService CarrierTypesEasyPost => GetService<CarrierTypeService>(nameof(CarrierTypesEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public CustomsInfoService CustomsInfoEasyPost => GetService<CustomsInfoService>(nameof(CustomsInfoEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public CustomsItemService CustomsItemsEasyPost => GetService<CustomsItemService>(nameof(CustomsItemsEasyPost));

        [ApiCompatibility(ApiVersion.Beta)]
        public EndShipperService EndShippersEasyPost => GetService<EndShipperService>(nameof(EndShippersEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public EventService EventsEasyPost => GetService<EventService>(nameof(EventsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public InsuranceService InsuranceEasyPost => GetService<InsuranceService>(nameof(InsuranceEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public OrderService OrdersEasyPost => GetService<OrderService>(nameof(OrdersEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public ParcelService ParcelsEasyPost => GetService<ParcelService>(nameof(ParcelsEasyPost));

        [ApiCompatibility(ApiVersion.Beta)]
        public PartnerService PartnersEasyPost => GetService<PartnerService>(nameof(PartnersEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public PickupService PickupsEasyPost => GetService<PickupService>(nameof(PickupsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public RateService RatesEasyPost => GetService<RateService>(nameof(RatesEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public RefundService RefundsEasyPost => GetService<RefundService>(nameof(RefundsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public ReportService ReportsEasyPost => GetService<ReportService>(nameof(ReportsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public ScanFormService ScanFormsEasyPost => GetService<ScanFormService>(nameof(ScanFormsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public ShipmentService ShipmentsEasyPost => GetService<ShipmentService>(nameof(ShipmentsEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public TrackerService TrackersEasyPost => GetService<TrackerService>(nameof(TrackersEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public UserService UsersEasyPost => GetService<UserService>(nameof(UsersEasyPost));

        [ApiCompatibility(ApiVersion.Latest)]
        public WebhookService WebhooksEasyPost => GetService<WebhookService>(nameof(WebhooksEasyPost));

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
