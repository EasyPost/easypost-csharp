using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class EndShipper : EasyPostObject, IEndShipperParameter
    {
        #region JSON Properties

        [JsonProperty("city")]
        public string? City { get; internal set; }
        [JsonProperty("company")]
        public string? Company { get; internal set; }
        [JsonProperty("country")]
        public string? Country { get; internal set; }
        [JsonProperty("email")]
        public string? Email { get; internal set; }
        [JsonProperty("error")]
        public string? Error { get; internal set; }
        [JsonProperty("message")]
        public string? Message { get; internal set; }
        [JsonProperty("name")]
        public string? Name { get; internal set; }
        [JsonProperty("phone")]
        public string? Phone { get; internal set; }
        [JsonProperty("state")]
        public string? State { get; internal set; }
        [JsonProperty("street1")]
        public string? Street1 { get; internal set; }
        [JsonProperty("street2")]
        public string? Street2 { get; internal set; }
        [JsonProperty("zip")]
        public string? Zip { get; internal set; }

        #endregion

        internal EndShipper()
        {
        }
    }

    public class EndShipperCollection : PaginatedCollection<EndShipper>
    {
        #region JSON Properties

        [JsonProperty("end_shippers")]
        public List<EndShipper>? EndShippers { get; internal set; }

        #endregion

        internal EndShipperCollection()
        {
        }

        // Cannot currently get the next page of EndShippers, so this is not implemented.
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<EndShipper> entries, int? pageSize = null) => throw new EndOfPaginationError();
    }
}
