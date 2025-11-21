using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of Embeddable-related functionality.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EmbeddableService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EmbeddableService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal EmbeddableService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an Embeddables Session.
        /// </summary>
        /// <param name="parameters">Data to use to create the Embeddables Session.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="EmbeddablesSession"/> object.</returns>
        [CrudOperations.Create]
        public async Task<EmbeddablesSession> CreateSession(Parameters.Embeddable.CreateSession parameters, CancellationToken cancellationToken = default) => await RequestAsync<EmbeddablesSession>(Method.Post, "embeddables/session", cancellationToken, parameters.ToDictionary());

        #endregion
    }
}
