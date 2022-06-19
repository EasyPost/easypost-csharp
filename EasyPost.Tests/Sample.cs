using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Services.V2;

namespace EasyPost.Tests
{
    public class Sample
    {
        public async Task Main()
        {
            Client client = new Client("my_api_key", ApiVersion.Latest);

            CarrierTypeService c = client.CarrierTypes;
        }
    }
}
