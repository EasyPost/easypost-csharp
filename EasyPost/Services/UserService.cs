using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;

namespace EasyPost.Services
{
    public class UserService : EasyPostService
    {
        internal UserService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Create a child user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<User> CreateChild(Users.Create parameters)
        {
            return await Create<User>("users", parameters);
        }

        /// <summary>
        ///     Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<User> Retrieve(string? id = null)
        {
            if (id == null)
            {
                return await Get<User>("users");
            }

            return await Get<User>($"users/{id}");
        }


        /// <summary>
        ///     Retrieve the current user.
        /// </summary>
        /// <returns>EasyPost.User instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<User> RetrieveMe()
        {
            return await Retrieve();
        }
    }
}
