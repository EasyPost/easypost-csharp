using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class SmartrateResult : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("result")]
        public List<Smartrate> result { get; set; }

        #endregion
    }
}
