using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#verifications-object">EasyPost verifications object</a>.
    /// </summary>
    public class Verifications : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("delivery")]
        public Verification? Delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification? Zip4 { get; set; }

        #endregion

    }
}
