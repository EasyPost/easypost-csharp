using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing line items of a <a href="https://docs.easypost.com/docs/shipments">Shipment</a>.
    /// </summary>
    public class LineItem : EasyPostObject, Parameters.ILineItemParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The total value of the line item.
        /// </summary>
        [JsonProperty("total_line_value")]
        public string? TotalLineValue { get; set; }

        /// <summary>
        ///     The description of the item.
        /// </summary>
        [JsonProperty("item_description")]
        public string? ItemDescription { get; set; }

        #endregion
    }
}
