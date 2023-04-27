using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserService : EasyPostService
    {
        internal UserService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a child user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Create]
        public async Task<User> CreateChild(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("user");
            return await Request<User>(Method.Post, "users", parameters);
        }

        /// <summary>
        ///     Create a child <see cref="User"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Users.CreateChild"/> parameter set.</param>
        /// <returns><see cref="User"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<User> CreateChild(BetaFeatures.Parameters.Users.CreateChild parameters)
        {
            // Because the normal CreateChild method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<User>(Method.Post, "users", parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<User> Retrieve(string? id = null) => id == null ? await Request<User>(Method.Get, "users") : await Request<User>(Method.Get, $"users/{id}");

        /// <summary>
        ///     Retrieve the current user.
        /// </summary>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<User> RetrieveMe() => await Retrieve();

        /// <summary>
        ///     Update the User's brand.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the brand with. Valid pairs:
        ///     * {"ad", string} Base64 encoded string for a png, gif, jpeg, or svg.
        ///     * {"ad_href", string} Valid URL under 255 characters
        ///     * {"background_color", string} Valid hex code
        ///     * {"color", string} Valid hex code
        ///     * {"logo", string} Base64 encoded string for a png, gif, jpeg, or svg
        ///     * {"logo_href", string} Valid URL under 255 characters
        ///     * {"theme", string} "theme1" or "theme2"
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Brand instance.</returns>
        [CrudOperations.Create]
        public async Task<Brand> UpdateBrand(string? id, Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("brand");
            return await Request<Brand>(Method.Put, $"users/{id}/brand", parameters);
        }

        /// <summary>
        ///     Update this <see cref="User"/>'s <see cref="Brand"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Users.UpdateBrand"/> parameter set.</param>
        /// <returns>This updated <see cref="Brand"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Brand> UpdateBrand(string? id, BetaFeatures.Parameters.Users.UpdateBrand parameters)
        {
            return await Request<Brand>(Method.Put, $"users/{id}/brand", parameters.ToDictionary());
        }

        /// <summary>
        ///     Update the User associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     * {"email", string} Email on the account. Can only be updated on the parent account.
        ///     * {"phone_number", string} Phone number on the account. Can only be updated on the parent account.
        ///     * {"recharge_amount", int} Recharge amount for the account in cents. Can only be updated on the parent account.
        ///     * {"secondary_recharge_amount", int} Secondary recharge amount for the account in cents. Can only be updated on the
        ///     parent account.
        ///     * {"recharge_threshold", int} Recharge threshold for the account in cents. Can only be updated on the parent
        ///     account.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>The updated User.</returns>
        [CrudOperations.Update]
        public async Task<User> Update(string? id, Dictionary<string, object> parameters)
        {
            return await Request<User>(Method.Put, $"users/{id}", parameters);
        }

        /// <summary>
        ///     Update this <see cref="User"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Users.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="User"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<User> Update(string? id, BetaFeatures.Parameters.Users.Update parameters)
        {
            return await Request<User>(Method.Put, $"users/{id}", parameters.ToDictionary());
        }

        /// <summary>
        ///     Delete the user.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete(string? id) => await Request(Method.Delete, $"users/{id}");

        #endregion
    }
}
