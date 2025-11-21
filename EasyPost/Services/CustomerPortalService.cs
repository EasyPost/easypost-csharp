using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of CustomerPortal-related functionality.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomerPortalService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerPortalService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal CustomerPortalService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Portal Session.
        /// </summary>
        /// <param name="parameters">Data to use to create the Portal Session.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="CustomerPortalAccountLink"/> object.</returns>
        [CrudOperations.Create]
        public async Task<CustomerPortalAccountLink> CreateAccountLink(Parameters.CustomerPortal.CreateAccountLink parameters, CancellationToken cancellationToken = default) => await RequestAsync<CustomerPortalAccountLink>(Method.Post, "customer_portal/account_link", cancellationToken, parameters.ToDictionary());

        #endregion
    }
}
