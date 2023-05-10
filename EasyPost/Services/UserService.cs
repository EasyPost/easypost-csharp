using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Parameters.Users;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#users">user-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal UserService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a child <see cref="User"/> for the current account.
        ///     <a href="https://www.easypost.com/docs/api#create-a-child-user">Related API documentation.</a>
        /// </summary>
        /// <param name="parameters">Parameters to create the child <see cref="User"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The created child <see cref="User"/>.</returns>
        [CrudOperations.Create]
        public async Task<User> CreateChild(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("user");
            return await RequestAsync<User>(Method.Post, "users", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a child <see cref="User"/> for the current account.
        ///     <a href="https://www.easypost.com/docs/api#create-a-child-user">Related API documentation.</a>
        /// </summary>
        /// <param name="parameters">Parameters to create the child <see cref="User"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The created child <see cref="User"/>.</returns>
        [CrudOperations.Create]
        public async Task<User> CreateChild(CreateChild parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal CreateChild method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<User>(Method.Post, "users", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a child <see cref="User"/>.
        ///     If no ID is specified, the current <see cref="User"/> will be returned.
        /// </summary>
        /// <param name="id">The ID of the <see cref="User"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The retrieved <see cref="User"/>.</returns>
        [CrudOperations.Read]
        public async Task<User> Retrieve(string? id = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                return await RetrieveMe(cancellationToken);
            }

            return await RequestAsync<User>(Method.Get, $"users/{id}", cancellationToken);
        }

        /// <summary>
        ///     Retrieve the current <see cref="User"/>.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The current <see cref="User"/>.</returns>
        [CrudOperations.Read]
        public async Task<User> RetrieveMe(CancellationToken cancellationToken = default) => await RequestAsync<User>(Method.Get, "users", cancellationToken);

        /// <summary>
        ///     Update a <see cref="User"/>'s <see cref="Brand"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-a-brand">Related API documentation.</a>
        /// </summary>
        /// <param name="id">The ID of the <see cref="User"/> to update the <see cref="Brand"/> of.</param>
        /// <param name="parameters">Parameters to update the <see cref="Brand"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Brand"/>.</returns>
        [CrudOperations.Create]
        public async Task<Brand> UpdateBrand(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("brand");
            return await RequestAsync<Brand>(Method.Put, $"users/{id}/brand", cancellationToken, parameters);
        }

        /// <summary>
        ///     Update a <see cref="User"/>'s <see cref="Brand"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-a-brand">Related API documentation.</a>
        /// </summary>
        /// <param name="id">The ID of the <see cref="User"/> to update the <see cref="Brand"/> of.</param>
        /// <param name="parameters">Parameters to update the <see cref="Brand"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Brand"/>.</returns>
        [CrudOperations.Create]
        public async Task<Brand> UpdateBrand(string id, UpdateBrand parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Brand>(Method.Put, $"users/{id}/brand", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Update a <see cref="User"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-a-user">Related API documentation.</a>
        /// </summary>
        /// <param name="parameters">Data to update <see cref="User"/> with.</param>
        /// <param name="id">The ID of the <see cref="User"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="User"/>.</returns>
        [CrudOperations.Update]
        public async Task<User> Update(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<User>(Method.Put, $"users/{id}", cancellationToken, parameters);
        }

        /// <summary>
        ///     Update a <see cref="User"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-a-user">Related API documentation.</a>
        /// </summary>
        /// <param name="parameters">Data to update <see cref="User"/> with.</param>
        /// <param name="id">The ID of the <see cref="User"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="User"/>.</returns>
        [CrudOperations.Update]
        public async Task<User> Update(string id, Update parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<User>(Method.Put, $"users/{id}", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Delete a child <see cref="User"/>.
        ///     <a href="https://www.easypost.com/docs/api#delete-a-child-user">Related API documentation.</a>
        /// </summary>
        /// <param name="id">The ID of the child <see cref="User"/> to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>None.</returns>
        [CrudOperations.Delete]
        public async Task Delete(string id, CancellationToken cancellationToken = default) => await RequestAsync(Method.Delete, $"users/{id}", cancellationToken);

        #endregion
    }
}
