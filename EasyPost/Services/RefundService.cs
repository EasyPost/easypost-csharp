using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models;
using RestSharp;

namespace EasyPost.Services
{
    public class RefundService : Service
    {
        public RefundService(Client client) : base(client)
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
            return await Get<Refund>($"refunds/{id}");
        }
    }
}
