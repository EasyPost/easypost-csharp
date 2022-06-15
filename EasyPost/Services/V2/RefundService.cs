using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class RefundService : Service
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
        public async Task<RefundCollection> All(Dictionary<string, object>? parameters = null)
        {
            CheckFunctionalityCompatible(nameof(All));

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
        public async Task<List<Refund>> Create(Dictionary<string, object> parameters)
        {
            CheckFunctionalityCompatible(nameof(Create));

            return await Create<List<Refund>>("refunds", new Dictionary<string, object>
            {
                {
                    "refund", parameters
                }
            });
        }

        /// <summary>
        ///     Retrieve a Refund from its id.
        /// </summary>
        /// <param name="id">String representing a Refund. Starts with "rfnd_".</param>
        /// <returns>EasyPost.Refund instance.</returns>
        public async Task<Refund> Retrieve(string id)
        {
            CheckFunctionalityCompatible(nameof(Retrieve));

            return await Get<Refund>($"refunds/{id}");
        }
    }
}
