using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class CarrierTypeService : EasyPostService
    {
        internal CarrierTypeService(Client client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>
        [CrudOperations.Read]
        public async Task<List<CarrierType>> All()
        {
            return await List<List<CarrierType>>("carrier_types");
        }

        #endregion
    }
}
