using System.Net.Http;
using EasyPost.Interfaces;
using EasyPost.Services;

namespace EasyPost.Clients
{
    public class V2Client : BaseClient
    {
        private const string ApiVersion = "v2";

        public AddressService Addresses
        {
            get { return new AddressService(this); }
        }

        public ApiKeyService ApiKeys
        {
            get { return new ApiKeyService(this); }
        }

        public BatchService Batches
        {
            get { return new BatchService(this); }
        }

        public CarrierAccountService CarrierAccounts
        {
            get { return new CarrierAccountService(this); }
        }

        public CarrierTypeService CarrierTypes
        {
            get { return new CarrierTypeService(this); }
        }

        public CustomsInfoService CustomsInfo
        {
            get { return new CustomsInfoService(this); }
        }

        public CustomsItemService CustomsItems
        {
            get { return new CustomsItemService(this); }
        }

        public EventService Events
        {
            get { return new EventService(this); }
        }

        public InsuranceService Insurance
        {
            get { return new InsuranceService(this); }
        }

        public OrderService Orders
        {
            get { return new OrderService(this); }
        }

        public ParcelService Parcels
        {
            get { return new ParcelService(this); }
        }

        public PickupService Pickups
        {
            get { return new PickupService(this); }
        }

        public RateService Rates
        {
            get { return new RateService(this); }
        }

        public RefundService Refunds
        {
            get { return new RefundService(this); }
        }

        public ReportService Reports
        {
            get { return new ReportService(this); }
        }

        public ScanFormService ScanForms
        {
            get { return new ScanFormService(this); }
        }

        public ShipmentService Shipments
        {
            get { return new ShipmentService(this); }
        }

        public TrackerService Trackers
        {
            get { return new TrackerService(this); }
        }

        public UserService Users
        {
            get { return new UserService(this); }
        }

        public WebhookService Webhooks
        {
            get { return new WebhookService(this); }
        }

        /// <summary>
        ///     Constructor for the v2 EasyPost client.
        /// </summary>
        /// <param name="apiKey">API key to use with this client.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not advised for general use.</param>
        public V2Client(string apiKey, HttpClient? customHttpClient = null) : base(apiKey, ApiVersion, customHttpClient)
        {
        }
    }
}
