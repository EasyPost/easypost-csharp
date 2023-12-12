using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters.Beta.User;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services.Beta
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#users">user-related beta functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal UserService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     List all Child <see cref="User"/> objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a User ID. Starts with "user_". Only retrieve users created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an User ID. Starts with "user_". Only retrieve users created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve users created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve users created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ChildUserCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ChildUserCollection> AllChildren(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            ChildUserCollection collection = await RequestAsync<ChildUserCollection>(Method.Get, "users/children", cancellationToken, parameters, overrideApiVersion: ApiVersion.Beta);
            collection.Filters = Parameters.Beta.User.AllChildren.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all Child <see cref="User"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.Beta.User.AllChildren"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="ChildUserCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ChildUserCollection> AllChildren(AllChildren parameters, CancellationToken cancellationToken = default)
        {
            ChildUserCollection collection = await RequestAsync<ChildUserCollection>(Method.Get, "users/children", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ChildUserCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="ChildUserCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="ChildUserCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ChildUserCollection> GetNextPageOfChildren(ChildUserCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ChildUserCollection, AllChildren>(async parameters => await AllChildren((AllChildren)parameters, cancellationToken), collection.Children, pageSize);

        #endregion
    }
}
