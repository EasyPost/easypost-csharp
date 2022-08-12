using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class ScanFormService : EasyPostService
    {
        internal ScanFormService(Client client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a ScanForm.
        /// </summary>
        /// <param name="shipments">Shipments to be associated with the ScanForm. Only id is required.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        [CrudOperations.Create]
        public async Task<ScanForm> Create(List<Shipment> shipments)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "shipments", shipments
                }
            };
            parameters = parameters.Wrap("scan_form");
            return await Create<ScanForm>("scan_forms", parameters);
        }


        /// <summary>
        ///     Get a paginated list of scan forms.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> All(Dictionary<string, object>? parameters = null)
        {
            ScanFormCollection scanFormCollection = await List<ScanFormCollection>("scan_forms", parameters);
            scanFormCollection.Filters = parameters;
            scanFormCollection.Client = Client; // specifically needs a v2 client
            return scanFormCollection;
        }

        /// <summary>
        ///     Retrieve a ScanForm from its id.
        /// </summary>
        /// <param name="id">String representing a scan form, starts with "sf_".</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanForm> Retrieve(string id)
        {
            return await Get<ScanForm>($"scan_forms/{id}");
        }

        #endregion
    }
}
