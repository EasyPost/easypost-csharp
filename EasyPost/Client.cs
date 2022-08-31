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

        public BetaClient Beta { get; }

        public BillingService Billing => GetService<BillingService>();

        public CarrierAccountService CarrierAccount => GetService<CarrierAccountService>();

        public CarrierTypeService CarrierType => GetService<CarrierTypeService>();

        public CustomsInfoService CustomsInfo => GetService<CustomsInfoService>();

        public CustomsItemService CustomsItem => GetService<CustomsItemService>();

        public EventService Event => GetService<EventService>();

        public InsuranceService Insurance => GetService<InsuranceService>();

        public OrderService Order => GetService<OrderService>();

        public ParcelService Parcel => GetService<ParcelService>();

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
        /// <param name="baseUrl">Base URL to use with this client. Must include API version.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed.</param>
        public Client(string apiKey, string? baseUrl = null, HttpClient? customHttpClient = null) : base(apiKey, ApiVersion.General, baseUrl, customHttpClient)
        {
            // We go ahead and initialize the Beta client internally here as well, since initializing a new one on each property call is expensive and causes lockups with the HttpClient library.
            Beta = new BetaClient(apiKey, baseUrl, customHttpClient);

            /*
             * NOTE: The current architecture of beta support is solid enough.
             * Technically, any service or model can have a beta or GA client (since they all accept the base EasyPostClient abstract class).
             * Whenever a service is used to generate a model, the client is passed into the model so that the model can use it for instance methods.
             * Currently, EndShipper and Partner are the only models that have beta support, and the only types of models that they can generate expect a beta client.
             * A future issue might come when a beta endpoint call (made using a beta client) returns a GA model. In that case, the GA model would inherit the beta client,
             * potentially causing bad things to happen. I (Nate) have a few ideas about how to mitigate this if it were to appear, but since that's not currently the case,
             * it's hard to predict how this would play out.
             */
        }
    }
}
