using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API.Beta;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services.Beta
{
    public class PartnerService : EasyPostService
    {
        internal PartnerService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a referral user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the referral user with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     * {"email", string} Email on the account.
        ///     * {"phone", string} Phone number on the account.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        [CrudOperations.Create]
        public async Task<Referral> CreateReferral(Dictionary<string, object> parameters) =>
            await Create<Referral>("referral_customers", parameters, overrideApiVersion: ApiVersion.Beta);

        #endregion
    }
}
