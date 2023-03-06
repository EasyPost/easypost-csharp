using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Users
{
    public class CreateChild : BaseParameters, IUserParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        #endregion
    }
}
