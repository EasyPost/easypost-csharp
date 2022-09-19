using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public class User : BaseUser
    {
        /// <summary>
        ///     Delete the user.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete()
        {
            Request request = new Request("users/{id}", Method.Delete);
            request.AddUrlSegment("id", id);
            return await request.Execute();
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
        public async Task Update(Dictionary<string, object> parameters)
        {
            Request request = new Request("users/{id}", Method.Patch);
            request.AddUrlSegment("id", id);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "user", parameters
                }
            });

            Merge(await request.Execute<User>());
        }

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
        public async Task<Brand> UpdateBrand(Dictionary<string, object> parameters)
        {
            Dictionary<string, object> wrappedParameters = new Dictionary<string, object>
            {
                {
                    "brand", parameters
                }
            };


            Request request = new Request("users/{id}/brand", Method.Patch);
            request.AddParameters(wrappedParameters);
            request.AddUrlSegment("id", id);

            return await request.Execute<Brand>();
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
        public static async Task<User> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("users", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "user", parameters
                }
            });

            return await request.Execute<User>();
        }


        /// <summary>
        ///     Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<User> Retrieve(string? id = null)
        {
            Request request;

            if (id == null)
            {
                request = new Request("users", Method.Get);
            }
            else
            {
                request = new Request("users/{id}", Method.Get);
                request.AddUrlSegment("id", id);
            }

            return await request.Execute<User>();
        }


        /// <summary>
        ///     Retrieve the current user.
        /// </summary>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<User> RetrieveMe()
        {
            Request request = new Request("users", Method.Get);

            return await request.Execute<User>();
        }
    }
}
