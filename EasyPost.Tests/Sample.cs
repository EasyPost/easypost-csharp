using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Services.Beta;

namespace EasyPost.Tests
{
    public class Sample
    {
        public async Task Main()
        {
            Client client = new Client("my_api_key", ApiVersion.Latest);

            EndShipperService service = client.EndShippers;
        }
    }
}
