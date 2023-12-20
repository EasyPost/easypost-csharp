using System.Collections.Generic;
using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API;

/// <summary>
///     Enums representing specific carrier account types.
/// </summary>
public class CarrierAccountType : ValueEnum
{
    /// <summary>
    ///     Represents a FedEx carrier account.
    /// </summary>
    public static readonly CarrierAccountType FedEx = new CarrierAccountType(26, "FedexAccount");

    /// <summary>
    ///     Represents a FedEx SmartPost carrier account.
    /// </summary>
    public static readonly CarrierAccountType FedExSmartPost = new CarrierAccountType(30, "FedexSmartpostAccount");

    /// <summary>
    ///     Represents a UPS carrier account.
    /// </summary>
    public static readonly CarrierAccountType Ups = new CarrierAccountType(59, "UpsAccount");

    /// <summary>
    ///     Initializes a new instance of the <see cref="CarrierAccountType"/> class.
    /// </summary>
    /// <param name="id">Internal ID of the enum. Must be unique among all enums of this specific type.</param>
    /// <param name="name">Name of the carrier account type. Stored as the value associated with this enum.</param>
    private CarrierAccountType(int id, string name)
        : base(id, name)
    {
    }

    /// <summary>
    ///     Gets the name of this <see cref="CarrierAccountType"/>.
    /// </summary>
    public string Name => (string)Value;

    /// <summary>
    ///     Gets all <see cref="CarrierAccountType"/> enums.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{CarrierAccountType}"/> of all <see cref="CarrierAccountType"/> enums.</returns>
    public static IEnumerable<CarrierAccountType> All() => GetAll<CarrierAccountType>();
}
