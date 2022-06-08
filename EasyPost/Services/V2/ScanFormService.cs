using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class ScanFormService : Service
    {
        internal ScanFormService(Client client) : base(client)
        {
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
        public async Task<ScanFormCollection> All(Dictionary<string, object>? parameters = null)
        {
            ScanFormCollection scanFormCollection = await List<ScanFormCollection>("scan_forms", parameters);
            scanFormCollection.filters = parameters;
            scanFormCollection.Client = Client; // specifically needs a v2 client
            return scanFormCollection;
        }

        /// <summary>
        ///     Create a ScanForm.
        /// </summary>
        /// <param name="shipments">Shipments to be associated with the ScanForm. Only id is required.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public async Task<ScanForm> Create(List<Shipment> shipments) =>
            await Create<ScanForm>("scan_forms", new Dictionary<string, object>
            {
                {
                    "scan_form", new Dictionary<string, object>
                    {
                        {
                            "shipments", shipments
                        }
                    }
                }
            });

        /// <summary>
        ///     Retrieve a ScanForm from its id.
        /// </summary>
        /// <param name="id">String representing a scan form, starts with "sf_".</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public async Task<ScanForm> Retrieve(string id) => await Get<ScanForm>($"scan_forms/{id}");
    }
}
