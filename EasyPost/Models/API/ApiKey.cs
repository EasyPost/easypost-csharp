using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ApiKey : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("key")]
        public string? Key { get; set; }

        #endregion
    }
}
