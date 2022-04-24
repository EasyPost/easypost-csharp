using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models;

namespace EasyPost.Services
{
    public class CarrierTypeService : Service
    {
        public CarrierTypeService(Client client) : base(client)
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
