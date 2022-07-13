using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class AddressCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; set; }

        #endregion
    }
}
