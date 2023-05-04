using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.EndShippers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.EndShipper.Update(Update)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseUpdateParameters<EndShipper>
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "city")]
        public string? City { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "company")]
        public string? Company { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "country")]
        public string? Country { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "name")]
        public string? Name { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state")]
        public string? State { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion

        public static new Update FromObject(EndShipper obj)
        {
            return new Update
            {
                City = obj.City,
                Country = obj.Country,
                Company = obj.Company,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                State = obj.State,
                Street1 = obj.Street1,
                Street2 = obj.Street2,
                Zip = obj.Zip,
            };
        }
    }
}
