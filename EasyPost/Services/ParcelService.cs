using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ParcelService : EasyPostService
    {
        internal ParcelService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Parcel.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the parcel with. Valid pairs:
        ///     * {"length", int}
        ///     * {"width", int}
        ///     * {"height", int}
        ///     * {"weight", double}
        ///     * {"predefined_package", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Parcel instance.</returns>
        [CrudOperations.Create]
        public async Task<Parcel> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("parcel");
            return await Create<Parcel>("parcels", parameters);
        }

        /// <summary>
        ///     Retrieve a Parcel from its id.
        /// </summary>
        /// <param name="id">String representing a Parcel. Starts with "prcl_".</param>
        /// <returns>EasyPost.Parcel instance.</returns>
        [CrudOperations.Read]
        public async Task<Parcel> Retrieve(string id) => await Get<Parcel>($"parcels/{id}");

        #endregion
    }
}
