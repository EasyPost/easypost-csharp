using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class CarrierTypeService : Service
    {
        internal CarrierTypeService(BaseClient client) : base(client)
        {
        }

        /// <summary>
        ///     Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>
        public async Task<List<CarrierType>> All() => await List<List<CarrierType>>("carrier_types");
    }
}
