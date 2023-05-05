using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#carrier-types">carrier type-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierTypeService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CarrierTypeService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
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
        public async Task<List<CarrierType>> All(CancellationToken cancellationToken = default) => await RequestAsync<List<CarrierType>>(Method.Get, "carrier_types", cancellationToken);

        #endregion
    }
}
