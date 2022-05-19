using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Services.V2;

namespace EasyPost.Tests
{
    public class Sample
    {
        public async Task Main()
        {
            V2Client client = new V2Client("my_api_key");

            CarrierTypeService c = client.CarrierTypes;
        }
    }
}
