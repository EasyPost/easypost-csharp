using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.V2;
using EasyPost.Parameters;
using EasyPost.Parameters.V2;

namespace EasyPost.Services.V2
{
    public class RefundService : EasyPostService
    {
        internal RefundService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     List all Refund objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.RefundCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<RefundCollection> All(All? parameters = null)
        {
            return await List<RefundCollection>("refunds", parameters);
        }

        /// <summary>
        ///     Create a Refund.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the refund with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of EasyPost.Refund instances.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<List<Refund>> Create(Refunds.Create parameters)
        {
            return await Create<List<Refund>>("refunds", parameters);
        }

        /// <summary>
        ///     Retrieve a Refund from its id.
        /// </summary>
        /// <param name="id">String representing a Refund. Starts with "rfnd_".</param>
        /// <returns>EasyPost.Refund instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Refund> Retrieve(string id)
        {
            return await Get<Refund>($"refunds/{id}");
        }
    }
}
