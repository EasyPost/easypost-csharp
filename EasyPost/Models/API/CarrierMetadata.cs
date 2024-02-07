using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Beta.CarrierMetadata
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#carriermetadata-object">EasyPost carrier metadata summary</a>.
    /// </summary>
    public class CarrierMetadata : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     A list of <see cref="Carrier"/> objects representing the carriers that are supported by EasyPost.
        /// </summary>
        [JsonProperty("carriers")]
        public List<Carrier>? Carriers { get; set; }

        #endregion
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.Beta.CarrierMetadata

    /// <summary>
    ///     Class representing a carrier in an EasyPost carrier metadata summary.
    /// </summary>
    public class Carrier
    {
        #region JSON Properties

        /// <summary>
        ///     The name (key) of the carrier.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     The human-readable name of the carrier.
        /// </summary>
        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        /// <summary>
        ///     A list of <see cref="PredefinedPackage"/> objects representing the predefined packages that are supported by the carrier.
        /// </summary>
        [JsonProperty("predefined_packages")]
        public List<PredefinedPackage>? PredefinedPackages { get; set; }

        /// <summary>
        ///     A list of <see cref="ServiceLevel"/> objects representing the service levels that are supported by the carrier.
        /// </summary>
        [JsonProperty("service_levels")]
        public List<ServiceLevel>? ServiceLevels { get; set; }

        /// <summary>
        ///     A list of <see cref="ShipmentOption"/> objects representing the shipment options that are supported by the carrier.
        /// </summary>
        [JsonProperty("shipment_options")]
        public List<ShipmentOption>? ShipmentOptions { get; set; }

        /// <summary>
        ///     A list of <see cref="SupportedFeature"/> objects representing the features that are supported by the carrier.
        /// </summary>
        [JsonProperty("supported_features")]
        public List<SupportedFeature>? SupportedFeatures { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class representing a predefined package in an EasyPost carrier metadata summary.
    /// </summary>
    public class PredefinedPackage
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the <see cref="Carrier"/> associated with this predefined package.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The description of the predefined package.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The dimensions available for the predefined package.
        /// </summary>
        [JsonProperty("dimensions")]
        public List<string>? Dimensions { get; set; }

        /// <summary>
        ///     The human-readable name of the predefined package.
        /// </summary>
        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        /// <summary>
        ///     The maximum weight of the predefined package.
        /// </summary>
        [JsonProperty("max_weight")]
        public string? MaxWeight { get; set; } // todo: float?

        /// <summary>
        ///     The name (key) of the predefined package.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class representing a service level in an EasyPost carrier metadata summary.
    /// </summary>
    public class ServiceLevel
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the <see cref="Carrier"/> associated with this service level.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The description of the service level.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The package dimensions available for the service level.
        /// </summary>
        [JsonProperty("dimensions")]
        public List<string>? Dimensions { get; set; }

        /// <summary>
        ///     The human-readable name of the service level.
        /// </summary>
        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        /// <summary>
        ///     The maximum weight for the service level.
        /// </summary>
        [JsonProperty("max_weight")]
        public string? MaxWeight { get; set; } // todo: float?

        /// <summary>
        ///     The name (key) of the service level.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class representing a shipment option in an EasyPost carrier metadata summary.
    /// </summary>
    public class ShipmentOption
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the <see cref="Carrier"/> associated with this shipment option.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     Whether or not the shipment option is deprecated.
        /// </summary>
        [JsonProperty("deprecated")]
        public bool? Deprecated { get; set; }

        /// <summary>
        ///     The description of the shipment option.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The human-readable name of the shipment option.
        /// </summary>
        [JsonProperty("human_readable")]
        public string? HumanReadable { get; set; }

        /// <summary>
        ///     The name (key) of the shipment option.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     The type of the shipment option (e.g. "boolean", "float", "string").
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class representing a supported feature in an EasyPost carrier metadata summary.
    /// </summary>
    public class SupportedFeature
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the <see cref="Carrier"/> associated with this supported feature.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The description of the supported feature.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The human-readable name of the supported feature.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Whether or not the feature is supported by the carrier.
        /// </summary>
        [JsonProperty("supported")]
        public bool? Supported { get; set; }

        #endregion
    }

    /// <summary>
    ///     Represents the types of metadata that can be retrieved for a carrier.
    /// </summary>
    public class CarrierMetadataType : ValueEnum
    {
        /// <summary>
        ///     An enum representing the service levels metadata type.
        /// </summary>
        public static readonly CarrierMetadataType ServiceLevels = new(1, "service_levels");

        /// <summary>
        ///    An enum representing the predefined packages metadata type.
        /// </summary>
        public static readonly CarrierMetadataType PredefinedPackages = new(2, "predefined_packages");

        /// <summary>
        ///     An enum representing the shipment options metadata type.
        /// </summary>
        public static readonly CarrierMetadataType ShipmentOptions = new(3, "shipment_options");

        /// <summary>
        ///     An enum representing the supported features metadata type.
        /// </summary>
        public static readonly CarrierMetadataType SupportedFeatures = new(4, "supported_features");

        private CarrierMetadataType(int id, string name)
            : base(id, name)
        {
        }

        /// <summary>
        ///     Gets all possible <see cref="CarrierMetadataType"/> values.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all possible <see cref="CarrierMetadataType"/> values.</returns>
        public static IEnumerable<CarrierMetadataType> All() => GetAll<CarrierMetadataType>();
    }
}
