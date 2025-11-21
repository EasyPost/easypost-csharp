using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CustomerPortal
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/customer-portals#creating-a-portal-session">Parameters</a> for <see cref="EasyPost.Services.CustomerPortalService.CreateAccountLink(CreateAccountLink, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateAccountLink : BaseParameters<Models.API.CustomerPortalAccountLink>
    {
        #region Request Parameters

        /// <summary>
        ///     Type of Customer Portal session.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "session_type")]
        public string? SessionType { get; set; }

        /// <summary>
        ///     The User ID of the sub account for which the portal session is being created.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "user_id")]
        public string? UserId { get; set; }

        /// <summary>
        ///     The URL to which the sub account will be redirected if the session URL is expired, reused, or otherwise invalid.
        ///     This should trigger a new session request.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "refresh_url")]
        public string? RefreshUrl { get; set; }

        /// <summary>
        ///     The URL to which the sub account will be redirected after exiting the Customer Portal session.
        ///     This does not confirm completion of the flow; webhook or API polling is recommended for confirmation.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "return_url")]
        public string? ReturnUrl { get; set; }

        /// <summary>
        ///     Used to configure the Customer Portal session.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "metadata")]
        public Dictionary<string, object>? Metadata { get; set; }

        #endregion
    }
}
