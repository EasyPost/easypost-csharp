using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RefundService : EasyPostService
    {
        internal RefundService(EasyPostClient client)
            : base(client)
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
            return await Create<List<Refund>>("refunds", parameters);
        }

        /// <summary>
        ///     Create a <see cref="Refund"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Refunds.Create"/> parameter set.</param>
        /// <returns><see cref="Refund"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<List<Refund>> Create(BetaFeatures.Parameters.Refunds.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<List<Refund>>("refunds", parameters.ToDictionary());
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
        public async Task<RefundCollection> All(Dictionary<string, object>? parameters = null) => await List<RefundCollection>("refunds", parameters);

        /// <summary>
        ///     List all <see cref="Refund"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Refunds.All"/> parameter set.</param>
        /// <returns><see cref="RefundCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<RefundCollection> All(BetaFeatures.Parameters.Refunds.All parameters) => await List<RefundCollection>("refunds", parameters.ToDictionary());

        /// <summary>
        ///     Retrieve a Refund from its id.
        /// </summary>
        /// <param name="id">String representing a Refund. Starts with "rfnd_".</param>
        /// <returns>EasyPost.Refund instance.</returns>
        [CrudOperations.Read]
        public async Task<Refund> Retrieve(string id) => await Get<Refund>($"refunds/{id}");

        #endregion
    }
}
