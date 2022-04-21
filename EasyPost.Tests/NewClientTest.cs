using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class NewClientTest
    {
        [TestMethod]
        public async Task Test()
        {
            ApiClient client = new ApiClient("api_key");
            AddressCollection addressCollection = await client.Addresses.All();

            Shipment shipment = await client.Shipments.Create();
        }
    }
}
