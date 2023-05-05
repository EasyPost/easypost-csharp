using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#user-object">EasyPost user</a>.
    /// </summary>
    public class User : BaseUser, IUserParameter
    {
        internal User()
        {
        }
    }
}
