using System.Collections.Generic;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;

namespace EasyPost.Models.API.Beta
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Beta.CarrierMetadata
    public class CarrierMetadata
    {
        #region JSON Properties

        [JsonProperty("carriers")]
        public List<Carrier>? Carriers { get; set; }

        #endregion
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.Beta.CarrierMetadata

    public class Carrier
    {
        #region JSON Properties

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        [JsonProperty("predefined_packages")]
        public List<PredefinedPackage>? PredefinedPackages { get; set; }

        [JsonProperty("service_levels")]
        public List<ServiceLevel>? ServiceLevels { get; set; }

        [JsonProperty("shipment_options")]
        public List<ShipmentOption>? ShipmentOptions { get; set; }

        [JsonProperty("supported_features")]
        public List<SupportedFeature>? SupportedFeatures { get; set; }

        #endregion
    }

    public class PredefinedPackage
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("dimensions")]
        public List<string>? Dimensions { get; set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        [JsonProperty("max_weight")]
        public string? MaxWeight { get; set; } // todo: float?

        [JsonProperty("name")]
        public string? Name { get; set; }

        #endregion
    }

    public class ServiceLevel
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("dimensions")]
        public List<string>? Dimensions { get; set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        [JsonProperty("max_weight")]
        public string? MaxWeight { get; set; } // todo: float?

        [JsonProperty("name")]
        public string? Name { get; set; }

        #endregion
    }

    public class ShipmentOption
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        [JsonProperty("deprecated")]
        public bool? Deprecated { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion
    }

    public class SupportedFeature
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("supported")]
        public bool? Supported { get; set; }

        #endregion
    }

    /// <summary>
    ///     Represents the types of metadata that can be retrieved for a carrier.
    /// </summary>
    public class CarrierMetadataType : ValueEnum
    {
        public static readonly CarrierMetadataType ServiceLevels = new(1, "service_levels");
        public static readonly CarrierMetadataType PredefinedPackages = new(2, "predefined_packages");
        public static readonly CarrierMetadataType ShipmentOptions = new(3, "shipment_options");
        public static readonly CarrierMetadataType SupportedFeatures = new(4, "supported_features");

        private CarrierMetadataType(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<SmartrateAccuracy> All() => GetAll<SmartrateAccuracy>();
    }
}
