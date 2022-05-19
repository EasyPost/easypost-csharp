using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class UserService : Service
    {
        internal UserService(BaseClient client) : base(client)
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
        public async Task<User> Create(Dictionary<string, object> parameters) =>
            await Create<User>("users", new Dictionary<string, object>
            {
                {
                    "user", parameters
                }
            });


        /// <summary>
        ///     Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
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
        public async Task<User> RetrieveMe() => await Retrieve();
    }
}
