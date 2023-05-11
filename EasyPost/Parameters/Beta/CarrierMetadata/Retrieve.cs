using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EasyPost.Models.API.Beta;

namespace EasyPost.Parameters.Beta.CarrierMetadata
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#retrieve-carrier-metadata">Parameters</a> for <see cref="EasyPost.Services.Beta.CarrierMetadataService.RetrieveCarrierMetadata(Retrieve, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Retrieve : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     A list of carrier names to retrieve metadata for.
        /// </summary>
        public List<string>? Carriers { get; set; }

        /// <summary>
        ///     A list of <see cref="CarrierMetadataType"/>s to retrieve.
        /// </summary>
        public List<CarrierMetadataType>? Types { get; set; }

        #endregion

        /// <summary>
        ///     Override the default <see cref="BaseParameters.ToDictionary"/> method to handle the unique serialization requirements for this parameter set.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        internal override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> data = new();

            if (Carriers != null)
            {
                data.Add("carriers", string.Join(",", Carriers));
            }

            if (Types != null)
            {
                data.Add("types", string.Join(",", Types.Select(x => x.ToString())));
            }

            return data;
        }
    }
}
