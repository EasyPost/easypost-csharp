using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Parameters;
using EasyPost.Parameters.ScanForms;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#scan-form-object">EasyPost scan form</a>.
    /// </summary>
    public class ScanForm : EasyPostObject, IScanFormParameter
    {
        #region JSON Properties

        [JsonProperty("address")]
        public Address? Address { get; set; }
        [JsonProperty("batch_id")]
        public string? BatchId { get; set; }
        [JsonProperty("form_file_type")]
        public string? FormFileType { get; set; }
        [JsonProperty("form_url")]
        public string? FormUrl { get; set; }

        /// <summary>
        ///     A human-readable message for any errors that occurred during the scan form's life cycle.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("tracking_codes")]
        public List<string>? TrackingCodes { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScanForm"/> class.
        /// </summary>
        internal ScanForm()
        {
        }
    }

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="ScanForm"/>s.
    /// </summary>
    public class ScanFormCollection : PaginatedCollection<ScanForm>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="ScanForm"/>s in the collection.
        /// </summary>
        [JsonProperty("scan_forms")]
        public List<ScanForm>? ScanForms { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScanFormCollection"/> class.
        /// </summary>
        internal ScanFormCollection()
        {
        }

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<ScanForm> entries, int? pageSize = null)
        {
            All parameters = Filters != null ? (All)Filters : new All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
