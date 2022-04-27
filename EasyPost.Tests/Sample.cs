using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    public class Sample
    {
        public async Task Main()
        {
            V2Client client = new V2Client("my_api_key");

            var c = client.CarrierTypes;

        }
    }
}
