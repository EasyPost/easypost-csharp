using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RefundService : EasyPostService
    {
        internal RefundService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Refund.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the refund with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of EasyPost.Refund instances.</returns>
        [CrudOperations.Create]
        public async Task<List<Refund>> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("refund");
            return await Request<List<Refund>>(Method.Post, "refunds", parameters);
        }

        /// <summary>
        ///     List all Refund objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.RefundCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<RefundCollection> All(Dictionary<string, object>? parameters = null)
        {
            return await Request<RefundCollection>(Method.Get, "refunds", parameters);
        }

        /// <summary>
        ///     Retrieve a Refund from its id.
        /// </summary>
        /// <param name="id">String representing a Refund. Starts with "rfnd_".</param>
        /// <returns>EasyPost.Refund instance.</returns>
        [CrudOperations.Read]
        public async Task<Refund> Retrieve(string id)
        {
            return await Request<Refund>(Method.Get, $"refunds/{id}");
        }

        #endregion
    }
}
