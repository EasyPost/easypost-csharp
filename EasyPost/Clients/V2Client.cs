using System.Net.Http;
using EasyPost.Interfaces;
using EasyPost.Services.V2;

namespace EasyPost.Clients
{
    public class V2Client : BaseClient
    {
        private const string ApiVersion = "v2";

        public AddressService Addresses => new AddressService(this);

        public ApiKeyService ApiKeys => new ApiKeyService(this);

        public BatchService Batches => new BatchService(this);

        public CarrierAccountService CarrierAccounts => new CarrierAccountService(this);

        public CarrierTypeService CarrierTypes => new CarrierTypeService(this);

        public CustomsInfoService CustomsInfo => new CustomsInfoService(this);

        public CustomsItemService CustomsItems => new CustomsItemService(this);

        public EventService Events => new EventService(this);

        public InsuranceService Insurance => new InsuranceService(this);

        public OrderService Orders => new OrderService(this);

        public ParcelService Parcels => new ParcelService(this);

        public PickupService Pickups => new PickupService(this);

        public RateService Rates => new RateService(this);

        public RefundService Refunds => new RefundService(this);

        public ReportService Reports => new ReportService(this);

        public ScanFormService ScanForms => new ScanFormService(this);

        public ShipmentService Shipments => new ShipmentService(this);

        public TrackerService Trackers => new TrackerService(this);

        public UserService Users => new UserService(this);

        public WebhookService Webhooks => new WebhookService(this);

        /// <summary>
        ///     Constructor for the v2 EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="customHttpClient">
        ///     Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not
        ///     advised for general use.
        /// </param>
        public V2Client(string apiKey, HttpClient? customHttpClient = null) : base(apiKey, ApiVersion, customHttpClient)
        {
        }
    }
}
