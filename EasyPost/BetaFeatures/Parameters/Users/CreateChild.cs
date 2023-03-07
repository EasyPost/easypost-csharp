using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Users
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.UserService.CreateChild(CreateChild)"/> API calls.
    /// </summary>
    public class CreateChild : BaseParameters, IUserParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        #endregion
    }
}
