using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ScanForm : EasyPostObject, IScanFormParameter
    {
        #region JSON Properties

        [JsonProperty("address")]
        public Address? Address { get; internal set; }
        [JsonProperty("batch_id")]
        public string? BatchId { get; internal set; }
        [JsonProperty("form_file_type")]
        public string? FormFileType { get; internal set; }
        [JsonProperty("form_url")]
        public string? FormUrl { get; internal set; }
        [JsonProperty("message")]
        public string? Message { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("tracking_codes")]
        public List<string>? TrackingCodes { get; internal set; }

        #endregion

        internal ScanForm()
        {
        }
    }

    public class ScanFormCollection : PaginatedCollection<ScanForm>
    {
        #region JSON Properties

        [JsonProperty("scan_forms")]
        public List<ScanForm>? ScanForms { get; internal set; }

        #endregion

        internal ScanFormCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<ScanForm> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.ScanForms.All parameters = Filters != null ? (BetaFeatures.Parameters.ScanForms.All)Filters : new BetaFeatures.Parameters.ScanForms.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
