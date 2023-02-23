using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class Refunds
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.RefundService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "refund", "carrier")]
            public string? Carrier { get; set; }

            [RequestParameter(Necessity.Optional, "refund", "tracking_codes")] // yes, the param name is plural when it's really just one code
            public string? TrackingCode { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.RefundService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
