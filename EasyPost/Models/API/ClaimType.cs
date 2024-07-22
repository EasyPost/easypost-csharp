using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API;

/// <summary>
///     Claim type enum.
/// </summary>
public class ClaimType : ValueEnum
{
    /// <summary>
    ///     An enum representing a damage claim type.
    /// </summary>
    public static readonly ClaimType Damage = new(1, "damage");

    /// <summary>
    ///     An enum representing a loss claim type.
    /// </summary>
    public static readonly ClaimType Loss = new(2, "loss");

    /// <summary>
    ///     An enum representing a theft claim type.
    /// </summary>
    public static readonly ClaimType Theft = new(3, "theft");

    /// <summary>
    ///     Initializes a new instance of the <see cref="ClaimType"/> class.
    /// </summary>
    /// <param name="id">The internal ID of this enum.</param>
    /// <param name="value">The value associated with this enum.</param>
    private ClaimType(int id, object value)
        : base(id, value)
    {
    }
}
