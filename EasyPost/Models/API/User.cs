using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using RestSharp;

namespace EasyPost.Models.API
{
    public class User : BaseUser, IUserParameter
    {
        internal User()
        {
        }

        #region CRUD Operations

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
        public async Task<Brand> UpdateBrand(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("brand");
            return await Request<Brand>(Method.Patch, $"users/{Id}/brand", parameters);
        }

        /// <summary>
        ///     Update this <see cref="User"/>'s <see cref="Brand"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Users.UpdateBrand"/> parameter set.</param>
        /// <returns>This updated <see cref="Brand"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Brand> UpdateBrand(BetaFeatures.Parameters.Users.UpdateBrand parameters)
        {
            return await Request<Brand>(Method.Patch, $"users/{Id}/brand", parameters.ToDictionary());
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
        public async Task<User> Update(Dictionary<string, object> parameters)
        {
            await Update<User>(Method.Patch, $"users/{Id}", parameters);
            return this;
        }

        /// <summary>
        ///     Update this <see cref="User"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Users.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="User"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<User> Update(BetaFeatures.Parameters.Users.Update parameters)
        {
            await Update<User>(Method.Patch, $"users/{Id}", parameters.ToDictionary());
            return this;
        }

        /// <summary>
        ///     Delete the user.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete() => await DeleteNoResponse($"users/{Id}");

        #endregion
    }
}
