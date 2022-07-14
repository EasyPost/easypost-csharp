using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class InsuranceCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("insurances")]
        public List<Insurance>? Insurances { get; set; }

        #endregion
    }
}
