using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;

namespace EasyPost.Services
{
    public class CarrierTypeService : EasyPostService
    {
        internal CarrierTypeService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>

        public async Task<List<CarrierType>> All()
        {
            return await List<List<CarrierType>>("carrier_types");
        }
    }
}
