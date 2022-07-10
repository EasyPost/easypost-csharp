using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class ParcelService : EasyPostService
    {
        internal ParcelService(Client client) : base(client)
        {
        }

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
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Parcel> Create(Dictionary<string, object> parameters)
        {
            return await Create<Parcel>("parcels", new Dictionary<string, object>
            {
                {
                    "parcel", parameters
                }
            });
        }

        /// <summary>
        ///     Retrieve a Parcel from its id.
        /// </summary>
        /// <param name="id">String representing a Parcel. Starts with "prcl_".</param>
        /// <returns>EasyPost.Parcel instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Parcel> Retrieve(string id)
        {
            return await Get<Parcel>($"parcels/{id}");
        }
    }
}
