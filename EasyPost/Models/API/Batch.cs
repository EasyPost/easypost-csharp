using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Batch : EasyPostObject, IBatchParameter
    {
        #region JSON Properties

        [JsonProperty("error")]
        public string? Error { get; internal set; }
        [JsonProperty("label_url")]
        public string? LabelUrl { get; internal set; }
        [JsonProperty("message")]
        public string? Message { get; internal set; }
        [JsonProperty("num_shipments")]
        public int? NumShipments { get; internal set; }
        [JsonProperty("reference")]
        public string? Reference { get; internal set; }
        [JsonProperty("scan_form")]
        public ScanForm? ScanForm { get; internal set; }
        [JsonProperty("shipments")]
        public List<BatchShipment>? Shipments { get; internal set; }
        [JsonProperty("state")]
        public string? State { get; internal set; }
        [JsonProperty("status")]
        public Dictionary<string, int>? Status { get; internal set; }

        #endregion

        internal Batch()
        {
        }
    }

    public class BatchCollection : PaginatedCollection<Batch>
    {
        #region JSON Properties

        [JsonProperty("batches")]
        public List<Batch>? Batches { get; internal set; }

        #endregion

        internal BatchCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Batch> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Shipments.All parameters = Filters != null ? (BetaFeatures.Parameters.Shipments.All)Filters : new BetaFeatures.Parameters.Shipments.All();

            // TODO: Batches get returned in reverse order from everything else (oldest first instead of newest first), so this needs to be "after_id" instead of "before_id"
            parameters.AfterId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
