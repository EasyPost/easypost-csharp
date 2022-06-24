using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Services.Beta;
using Xunit;

namespace EasyPost.Tests
{
    public class Sample
    {
        [Fact]
        public async Task Main()
        {
            Client client = new Client("my_api_key", ApiVersion.Latest);

            EndShipperService service = client.EndShippers;
        }
    }
}
