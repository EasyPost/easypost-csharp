using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Embeddable
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/embeddables#create-an-embeddable-session">Parameters</a> for <see cref="EasyPost.Services.EmbeddableService.CreateSession(CreateSession, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateSession : BaseParameters<Models.API.EmbeddablesSession>
    {
        #region Request Parameters

        /// <summary>
        ///     The integrator’s domain in bare-host format (e.g., example.com), excluding protocol and subdomains.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "origin_host")]
        public string? OriginHost { get; set; }

        /// <summary>
        ///     The User ID of the sub account for which the embeddable session is being created.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "user_id")]
        public string? UserId { get; set; }

        #endregion
    }
}
