using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Exceptions;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    public class Collection : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("filters")]
        public All? Filters { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        #endregion

        protected void UpdateFilters(IEnumerable<EasyPostObject>? elements, string propertyName)
        {
            Filters ??= new Parameters.All();
            Filters.BeforeId = (elements ?? throw new PropertyMissingException(propertyName)).Last().Id ?? throw new PropertyMissingException("id");

            if (Client == null)
            {
                throw new ClientNotConfiguredException();
            }
        }
    }
}
