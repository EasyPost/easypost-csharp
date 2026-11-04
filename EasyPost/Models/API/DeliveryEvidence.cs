using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a delivery evidence (proof of delivery) image for a <see cref="Tracker"/> object.
    /// </summary>
    public class DeliveryEvidence : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The type of delivery evidence.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"signature"</description>
        ///         </item>
        ///         <item>
        ///             <description>"delivery"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        ///     The URL of the delivery evidence image.
        ///     Image URLs aren't guaranteed to remain available,
        ///     so download and store any image you need to keep.
        /// </summary>
        [JsonProperty("image")]
        public string? Image { get; set; }

        #endregion
    }
}
