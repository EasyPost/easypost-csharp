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
        public List<Carrier>? Carriers { get; internal set; }

        #endregion
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.Beta.CarrierMetadata

    public class Carrier
    {
        #region JSON Properties

        [JsonProperty("name")]
        public string? Name { get; internal set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; internal set; }

        [JsonProperty("predefined_packages")]
        public List<PredefinedPackage>? PredefinedPackages { get; internal set; }

        [JsonProperty("service_levels")]
        public List<ServiceLevel>? ServiceLevels { get; internal set; }

        [JsonProperty("shipment_options")]
        public List<ShipmentOption>? ShipmentOptions { get; internal set; }

        [JsonProperty("supported_features")]
        public List<SupportedFeature>? SupportedFeatures { get; internal set; }

        #endregion
    }

    public class PredefinedPackage
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; internal set; }

        [JsonProperty("description")]
        public string? Description { get; internal set; }

        [JsonProperty("dimensions")]
        public List<string>? Dimensions { get; internal set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; internal set; }

        [JsonProperty("max_weight")]
        public string? MaxWeight { get; internal set; } // todo: float?

        [JsonProperty("name")]
        public string? Name { get; internal set; }

        #endregion
    }

    public class ServiceLevel
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; internal set; }

        [JsonProperty("description")]
        public string? Description { get; internal set; }

        [JsonProperty("dimensions")]
        public List<string>? Dimensions { get; internal set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; internal set; }

        [JsonProperty("max_weight")]
        public string? MaxWeight { get; internal set; } // todo: float?

        [JsonProperty("name")]
        public string? Name { get; internal set; }

        #endregion
    }

    public class ShipmentOption
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; internal set; }

        [JsonProperty("deprecated")]
        public bool? Deprecated { get; internal set; }

        [JsonProperty("description")]
        public string? Description { get; internal set; }

        [JsonProperty("human_readable")]
        public string? HumanReadable { get; internal set; }

        [JsonProperty("name")]
        public string? Name { get; internal set; }

        [JsonProperty("type")]
        public string? Type { get; internal set; }

        #endregion
    }

    public class SupportedFeature
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; internal set; }

        [JsonProperty("description")]
        public string? Description { get; internal set; }

        [JsonProperty("name")]
        public string? Name { get; internal set; }

        [JsonProperty("supported")]
        public bool? Supported { get; internal set; }

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

        public static IEnumerable<SmartRateAccuracy> All() => GetAll<SmartRateAccuracy>();
    }
}
