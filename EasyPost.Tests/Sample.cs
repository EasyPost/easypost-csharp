using System.Threading.Tasks;

namespace EasyPost.Tests
{
    public class Sample
    {
        public async Task Main()
        {
            Client client = new Client("api_key");

            // Put beta feature services under beta namespace
            // i.e. client.Beta.Endshippers.Create

            // blind calls return void, throw api exception if error

            // google doc for all exception types

            // all collections are paginated, but hold off until later

            // all static functions are called by XService

            // verify instance/static functions are the same

            // singular resource names

            // remove request parameters
        }
    }
}
