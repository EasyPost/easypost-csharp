using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InsuranceService : EasyPostService
    {
        internal InsuranceService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an Insurance.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the insurance with. Valid pairs:
        ///     * {"to_address", Address} The actual destination of the package to be insured
        ///     * {"from_address", Address} The actual origin of the package to be insured
        ///     * {"tracking_code", string} The tracking code associated with the non-EasyPost-purchased package you'd like to
        ///     insure
        ///     * {"reference", string} Optional. A unique value that may be used in place of ID when doing Retrieve calls for this
        ///     insurance
        ///     * {"amount", decimal} The USD value of contents you would like to insure. Currently the maximum is $5000
        ///     * {"carrier", string} Optional. The carrier associated with the tracking_code you provided. The carrier will get
        ///     auto-detected if none is provided
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Insurance instance.</returns>
        [CrudOperations.Create]
        public async Task<Insurance> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("insurance");
            return await Create<Insurance>("insurances", parameters);
        }

        /// <summary>
        ///     List all Insurance objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Insurance ID. Starts with "ins_". Only retrieve insurances created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Insurance ID. Starts with "ins_". Only retrieve insurances created
        ///     after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve insurances created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve insurances created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.InsuranceCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<InsuranceCollection> All(Dictionary<string, object>? parameters = null)
        {
            return await List<InsuranceCollection>("insurances", parameters);
        }

        /// <summary>
        ///     Retrieve an Insurance from its id.
        /// </summary>
        /// <param name="id">String representing an Insurance. Starts with "ins_".</param>
        /// <returns>EasyPost.Insurance instance.</returns>
        [CrudOperations.Read]
        public async Task<Insurance> Retrieve(string id)
        {
            return await Get<Insurance>($"insurances/{id}");
        }

        #endregion
    }
}
