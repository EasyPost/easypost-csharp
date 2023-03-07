using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.TaxIdentifiers
{
    public class Create : BaseParameters, ITaxIdenfierParameter
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
