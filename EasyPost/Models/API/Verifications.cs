using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/addresses#verifications-object">EasyPost verifications object</a>.
    /// </summary>
    public class Verifications : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     Verification results for whether this address is deliverable.
        ///     Also makes minor corrections to spelling and formatting.
        ///     U.S. addresses will have their <see cref="EasyPost.Models.API.Address.Residential"/> status checked and set as needed.
        /// </summary>
        [JsonProperty("delivery")]
        public Verification? Delivery { get; set; }

        /// <summary>
        ///     Verification results for the address's ZIP + 4 code.
        ///     Only applicable to U.S. addresses.
        /// </summary>
        [JsonProperty("zip4")]
        public Verification? Zip4 { get; set; }

        #endregion

    }
}
