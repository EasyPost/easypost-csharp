using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.TaxIdentifiers
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#tax-identifiers">Parameters</a> for <see cref="Shipments.Create.TaxIdentifiers"/> property.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ITaxIdentifierParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Which entity the tax identifier belongs to (<c>"SENDER"</c> or <c>"RECEIVER"</c>).
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "entity")]
        public string? Entity { get; set; }

        /// <summary>
        ///     The tax identifier number.
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "tax_id")]
        public string? TaxId { get; set; }

        /// <summary>
        ///     The <a href="https://support.easypost.com/hc/en-us/articles/4412101923213">type</a> of tax identification number.
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "tax_id_type")]
        public string? TaxIdType { get; set; }

        /// <summary>
        ///     The country that issued the tax identification number.
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "issuing_country")]
        public string? IssuingCountry { get; set; }

        #endregion
    }
}
