using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class InsuranceService : Service
    {
        internal InsuranceService(BaseClient client) : base(client)
        {
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
        public async Task<InsuranceCollection> All(Dictionary<string, object>? parameters = null) => await List<InsuranceCollection>("insurances", parameters);

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
        public async Task<Insurance> Create(Dictionary<string, object> parameters) =>
            await Create<Insurance>("insurances", new Dictionary<string, object>
            {
                {
                    "insurance", parameters
                }
            });

        /// <summary>
        ///     Retrieve an Insurance from its id.
        /// </summary>
        /// <param name="id">String representing an Insurance. Starts with "ins_".</param>
        /// <returns>EasyPost.Insurance instance.</returns>
        public async Task<Insurance> Retrieve(string id) => await Get<Insurance>($"insurances/{id}");
    }
}
