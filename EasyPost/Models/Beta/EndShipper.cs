using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.Base;
using RestSharp;

namespace EasyPost.Models.Beta
{
    public class EndShipper : Address
    {
        /// <summary>
        ///     Update this EndShipper. Must pass in all properties (new and existing).
        /// </summary>
        /// <param name="parameters">See EndShipper.Create for more details.</param>
        [ApiCompatibility(ApiVersion.Beta)]
        public async Task<EndShipper> Update(Dictionary<string, object> parameters)
        {
            return await Update<EndShipper>(Method.Put, $"end_shippers/{Id}", new Dictionary<string, object>
            {
                {
                    "address", parameters
                }
            });
        }
        // EndShipper needs Put, not Patch
    }
}
