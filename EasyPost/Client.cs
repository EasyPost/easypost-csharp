using System.Net.Http;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Services;

namespace EasyPost
{
    public class Client : EasyPostClient
    {
        public AddressService Address { get; }

        public ApiKeyService ApiKey { get; } // TODO: Recommend renaming. Because ApiKey is used as the name of the API key service, you have to access the actual configured API key via ApiKeyInUse

        public BatchService Batch { get; }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
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

        public PickupService Pickup { get; }

        public RateService Rate { get; }
        
        public ReferralCustomerService ReferralCustomer { get; }

        public RefundService Refund { get; }

        public ReportService Report { get; }

        public ScanFormService ScanForm { get; }

        public ShipmentService Shipment { get; }

        public TrackerService Tracker { get; }

        public UserService User { get; }

        public WebhookService Webhook { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="configuration">Configuration for this client.</param>
#pragma warning disable IDE0021 // Ignoring since more properties will be added during construction in the future.
        public Client(ClientConfiguration configuration)
            : base(configuration)
#pragma warning restore IDE0021
        {
            // We initialize the services here since initializing a new one on each property call is expensive.
            Address = new AddressService(this);
            ApiKey = new ApiKeyService(this);
            Batch = new BatchService(this);
            Billing = new BillingService(this);
            CarrierAccount = new CarrierAccountService(this);
            CarrierType = new CarrierTypeService(this);
            CustomsInfo = new CustomsInfoService(this);
            CustomsItem = new CustomsItemService(this);
            EndShipper = new EndShipperService(this);
            Event = new EventService(this);
            Insurance = new InsuranceService(this);
            Order = new OrderService(this);
            Parcel = new ParcelService(this);
            Pickup = new PickupService(this);
            Rate = new RateService(this);
            ReferralCustomer = new ReferralCustomerService(this);
            Refund = new RefundService(this);
            Report = new ReportService(this);
            ScanForm = new ScanFormService(this);
            Shipment = new ShipmentService(this);
            Tracker = new TrackerService(this);
            User = new UserService(this);
            Webhook = new WebhookService(this);

            // We go ahead and initialize the Beta client internally here as well
            Beta = new BetaClient(configuration);
        }

        protected override void Dispose(bool disposing)
        {
            // ref: https://dzone.com/articles/when-and-how-to-use-dispose-and-finalize-in-c
            // ref: https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1063#pseudo-code-example
            if (disposing)
            {
                // Dispose managed state (managed objects).
                // "disposing" inherently true when called from Dispose(), so don't need to pass it in.

                // Dispose of the services
                Address.Dispose();
                ApiKey.Dispose();
                Batch.Dispose();
                Billing.Dispose();
                CarrierAccount.Dispose();
                CarrierType.Dispose();
                CustomsInfo.Dispose();
                CustomsItem.Dispose();
                EndShipper.Dispose();
                Event.Dispose();
                Insurance.Dispose();
                Order.Dispose();
                Parcel.Dispose();
                Pickup.Dispose();
                Rate.Dispose();
                ReferralCustomer.Dispose();
                Refund.Dispose();
                Report.Dispose();
                ScanForm.Dispose();
                Shipment.Dispose();
                Tracker.Dispose();
                User.Dispose();
                Webhook.Dispose();

                // Dispose of the Beta client
                Beta.Dispose();
            }

            // Free native resources (unmanaged objects) and override a finalizer below.

            // Dispose of the base client
            base.Dispose(disposing);
        }
    }
}
