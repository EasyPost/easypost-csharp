using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost user.
    /// </summary>
    public class User : BaseUser, IUserParameter
    {
        internal User()
        {
        }
    }
}
