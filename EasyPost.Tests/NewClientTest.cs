using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
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
            AddressCollection addressCollection = await client.AddressService.All();

            bool succeeded = await client.TrackerService.CreateList(new Dictionary<string, object>());
        }
    }
}
