using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class AddressCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("addresses")]
        public List<Address> addresses { get; set; }

        #endregion
    }
}
