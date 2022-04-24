using System.Threading.Tasks;
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

        public Addresses Addresses
        {
            get { return new Addresses(this); }
        }

        public Batches Batches
        {
            get { return new Batches(this); }
        }

        public CarrierAccounts CarrierAccounts
        {
            get { return new CarrierAccounts(this); }
        }

        public CustomsInfos CustomsInfos
        {
            get { return new CustomsInfos(this); }
        }

        public CustomsItems CustomsItems
        {
            get { return new CustomsItems(this); }
        }

        public Insurances Insurances
        {
            get { return new Insurances(this); }
        }

        public Orders Orders
        {
            get { return new Orders(this); }
        }

        public Parcels Parcels
        {
            get { return new Parcels(this); }
        }

        public Pickups Pickups
        {
            get { return new Pickups(this); }
        }

        public Refunds Refunds
        {
            get { return new Refunds(this); }
        }

        public Reports Reports
        {
            get { return new Reports(this); }
        }

        public ScanForms ScanForms
        {
            get { return new ScanForms(this); }
        }

        public Shipments Shipments
        {
            get { return new Shipments(this); }
        }

        public Trackers Trackers
        {
            get { return new Trackers(this); }
        }

        public Users Users
        {
            get { return new Users(this); }
        }

        public Webhooks Webhooks
        {
            get { return new Webhooks(this); }
        }
    }
}
