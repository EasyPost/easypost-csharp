using System.Threading.Tasks;

namespace EasyPost.Tests
{
    public class Sample
    {
        public Task Main()
        {
            Client client = new Client("api_key");
            return Task.CompletedTask;
        }
    }
}
