using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Models;
using EasyPost.Services;

namespace EasyPost
{
    public class ApiClient
    {
        private readonly Client _client;

        public ApiClient(string apiKey)
        {
            _client = new Client(new ClientConfiguration(apiKey));
        }

        public async Task<T> Execute<T>(Request request) where T : new()
        {
            return await _client.Execute<T>(request);
        }

        public async Task<bool> Execute(Request request)
        {
            return await _client.Execute(request);
        }

        public AddressService AddressService
        {
            get { return new AddressService(this); }
        }

        public ApiKeyService ApiKeyService
        {
            get { return new ApiKeyService(this); }
        }

        public BatchService BatchService
        {
            get { return new BatchService(this); }
        }

        public CarrierAccountService CarrierAccountService
        {
            get { return new CarrierAccountService(this); }
        }

        public CarrierTypeService CarrierTypeService
        {
            get { return new CarrierTypeService(this); }
        }

        public CustomsInfoService CustomsInfoService
        {
            get { return new CustomsInfoService(this); }
        }

        public CustomsItemService CustomsItemService
        {
            get { return new CustomsItemService(this); }
        }

        public EventService EventService
        {
            get { return new EventService(this); }
        }

        public InsuranceService InsuranceService
        {
            get { return new InsuranceService(this); }
        }

        public OrderService OrderService
        {
            get { return new OrderService(this); }
        }

        public ParcelService ParcelService
        {
            get { return new ParcelService(this); }
        }

        public PickupService PickupService
        {
            get { return new PickupService(this); }
        }

        public Rates Rates
        {
            get { return new Rates(this); }
        }

        public RefundService RefundService
        {
            get { return new RefundService(this); }
        }

        public ReportService ReportService
        {
            get { return new ReportService(this); }
        }

        public ScanFormService ScanFormService
        {
            get { return new ScanFormService(this); }
        }

        public ShipmentService ShipmentService
        {
            get { return new ShipmentService(this); }
        }

        public TrackerService TrackerService
        {
            get { return new TrackerService(this); }
        }

        public UserService UserService
        {
            get { return new UserService(this); }
        }

        public WebhookService WebhookService
        {
            get { return new WebhookService(this); }
        }
    }
}
