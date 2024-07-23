using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API;

/// <summary>
///     Claim payment method enum.
/// </summary>
public class ClaimPaymentMethod : ValueEnum
{
    /// <summary>
    ///     An enum representing paying a claim reimbursement via a mailed check.
    /// </summary>
    public static readonly ClaimPaymentMethod MailedCheck = new(1, "mailed_check");

    /// <summary>
    ///     An enum representing paying a claim reimbursement via a wallet refund.
    /// </summary>
    public static readonly ClaimPaymentMethod EasyPostWallet = new(2, "easypost_wallet");

    /// <summary>
    ///     Initializes a new instance of the <see cref="ClaimPaymentMethod"/> class.
    /// </summary>
    /// <param name="id">The internal ID of this enum.</param>
    /// <param name="value">The value associated with this enum.</param>
    private ClaimPaymentMethod(int id, object value)
        : base(id, value)
    {
    }
}
