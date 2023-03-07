using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

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

        [CrudOperations.Create]
        public async Task<Parcel> Create(BetaFeatures.Parameters.Parcels.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<Parcel>("parcels", parameters.ToDictionary());
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
