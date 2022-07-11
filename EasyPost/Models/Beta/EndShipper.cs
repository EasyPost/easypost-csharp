using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.Base;
using EasyPost.Parameters.Beta;
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
        public async Task<EndShipper> Update(EndShippers.Update parameters)
        {
            return await Update<EndShipper>(Method.Put, $"end_shippers/{Id}", parameters);
        }
        // EndShipper needs Put, not Patch
    }
}
