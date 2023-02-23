using EasyPost.Utilities.Annotations;

namespace EasyPost.Beta.Parameters.V2
{
    public static class Insurance
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.InsuranceService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "insurance", "amount")]
            public double? Amount { get; set; }

            [RequestParameter(Necessity.Optional, "insurance", "carrier")]
            public string? Carrier { get; set; }

            [RequestParameter(Necessity.Optional, "insurance", "from_address")]
            public EasyPost.Models.API.Address? FromAddress { get; set; }

            [RequestParameter(Necessity.Optional, "insurance", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "insurance", "to_address")]
            public EasyPost.Models.API.Address? ToAddress { get; set; }

            [RequestParameter(Necessity.Optional, "insurance", "tracking_code")]
            public string? TrackingCode { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.InsuranceService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
