using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.User
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/users/child-users#create-a-child-user">Parameters</a> for <see cref="EasyPost.Services.UserService.CreateChild(CreateChild, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateChild : BaseParameters<Models.API.User>, IUserParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The name of the new <see cref="Models.API.User"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        #endregion
    }
}
