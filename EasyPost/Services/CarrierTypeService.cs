using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierTypeService : EasyPostService
    {
        internal CarrierTypeService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Get all available carrier types.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierType instances.</returns>
        [CrudOperations.Read]
        public async Task<List<CarrierType>> All() => await RequestAsync<List<CarrierType>>(Method.Get, "carrier_types");

        #endregion
    }
}
