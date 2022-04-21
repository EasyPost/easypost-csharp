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

        public Shipments Shipments
        {
            get { return new Shipments(this); }
        }
    }
}
