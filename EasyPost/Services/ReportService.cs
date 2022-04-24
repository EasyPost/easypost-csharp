using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models;

namespace EasyPost.Services
{
    public class ReportService : Service
    {
        public ReportService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Get a paginated list of reports.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a Report ID. Only retrieve ScanForms created before this id. Takes
        ///     precedence over after_id.
        ///     * {"after_id", string} String representing a Report ID. Only retrieve ScanForms created after this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <param name="type">The type of report, e.g. "shipment", "tracker", "payment_log", etc.</param>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        public async Task<ReportCollection> All(string type, Dictionary<string, object>? parameters = null)
        {
            ReportCollection reportCollection = await List<ReportCollection>($"reports/{type}", parameters);
            reportCollection.filters = parameters;
            reportCollection.type = type;
            reportCollection.Client = Client;
            return reportCollection;
        }

        /// <summary>
        ///     Create a Report.
        /// </summary>
        /// <param name="type">
        ///     The type of report, e.g. "shipment", "tracker", "payment_log", etc.
        /// </param>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"start_date", string} Date to start the report at.
        ///     * {"end_date", string} Date to end the report at.
        ///     * {"include_children", string} Whether or not to include child objects in the report.
        ///     * {"send_email", string} Whether or not to send the report via email.
        ///     * {"columns", List&lt;string&gt;} Specify the exact columns you want in your report.
        ///     * {"additional_columns", List&lt;string&gt;} Request additional columns (if any) outside of the defaults.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Report instance.</returns>
        public async Task<Report> Create(string type, Dictionary<string, object>? parameters = null)
        {
            return await Create<Report>($"reports/{type}", parameters);
        }


        /// <summary>
        ///     Retrieve a Report from its id.
        /// </summary>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        public async Task<Report> Retrieve(string id)
        {
            return await Get<Report>($"reports/{id}");
        }

        /// <summary>
        ///     Retrieve a Report from its id and type.
        /// </summary>
        /// <param name="type">Type of report, e.g. shipment, tracker, payment_log, etc.</param>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        public async Task<Report> Retrieve(string type, string id)
        {
            return await Get<Report>($"reports/{type}/{id}");
        }
    }
}
