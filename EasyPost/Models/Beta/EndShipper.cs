using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<EndShipper> Update(Dictionary<string, object> parameters)
        {
            CheckFunctionalityCompatible(nameof(Update));

            return await Update<EndShipper>(Method.Put, $"end_shippers/{id}", new Dictionary<string, object>
            {
                {
                    "address", parameters
                }
            });
        }
        // EndShipper needs Put, not Patch
    }
}
