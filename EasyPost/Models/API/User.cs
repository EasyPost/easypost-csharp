using EasyPost.Models.Shared;
using EasyPost.Parameters;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.User
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
#pragma warning disable CA1724 // Naming conflicts with Parameters.User
