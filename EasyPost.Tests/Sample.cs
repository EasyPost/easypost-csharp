using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    public class Sample
    {
        public async Task Main()
        {
            Client client = new Client("my_api_key");

            var c = client.CarrierTypes;

        }
    }
}
