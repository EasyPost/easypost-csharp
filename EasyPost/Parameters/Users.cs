using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Users
    {
        public sealed class Update : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "email")]
            public string? Email { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "password")]
            public string? Password { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "password_confirmation")]
            public string? PasswordConfirmation { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "current_password")]
            public string? CurrentPassword { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "name")]
            public string? Name { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "phone_number")]
            public string? PhoneNumber { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "recharge_amount")]
            public string? RechargeAmount { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "secondary_recharge_amount")]
            public string? SecondaryRechargeAmount { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "recharge_threshold")]
            public string? RechargeThreshold { internal get; set; }

            #endregion

            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class UpdateBrand : RequestParameters
        {
            // TODO: What are the parameters? (wrapped in "brand")

            public UpdateBrand(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "email")]
            public string? Email { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "password")]
            public string? Password { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "password_confirmation")]
            public string? PasswordConfirmation { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "current_password")]
            public string? CurrentPassword { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "name")]
            public string? Name { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "phone_number")]
            public string? PhoneNumber { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "recharge_amount")]
            public string? RechargeAmount { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "secondary_recharge_amount")]
            public string? SecondaryRechargeAmount { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "user", "recharge_threshold")]
            public string? RechargeThreshold { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
