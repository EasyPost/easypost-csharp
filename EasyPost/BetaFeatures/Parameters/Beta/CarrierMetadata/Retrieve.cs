using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EasyPost.Models.API.Beta;

namespace EasyPost.BetaFeatures.Parameters.Beta.CarrierMetadata
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.Beta.CarrierMetadataService.RetrieveCarrierMetadata(Retrieve)"/> API calls.
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
        ///     A list of metadata types to retrieve.
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