using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Annotations;
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
            return await Create<User>("users", parameters);
        }

        [CrudOperations.Create]
        public async Task<User> CreateChild(BetaFeatures.Parameters.Users.CreateChild parameters)
        {
            // Because the normal CreateChild method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<User>("users", parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<User> Retrieve(string? id = null) => id == null ? await Get<User>("users") : await Get<User>($"users/{id}");

        /// <summary>
        ///     Retrieve the current user.
        /// </summary>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Read]
        public async Task<User> RetrieveMe() => await Retrieve();

        #endregion
    }
}
