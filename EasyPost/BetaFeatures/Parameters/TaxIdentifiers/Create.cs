using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.TaxIdentifiers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.BetaFeatures.Parameters.Shipments.Create.TaxIdentifiers"/> property.
    /// </summary>
    public class Create : BaseParameters, ITaxIdentifierParameter
    {
        #region Request Parameters

        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "entity")]
        public string? Entity { get; set; }

        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "tax_id")]
        public string? TaxId { get; set; }

        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "tax_id_type")]
        public string? TaxIdType { get; set; }

        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "issuing_country")]
        public string? IssuingCountry { get; set; }

        #endregion
    }
}
