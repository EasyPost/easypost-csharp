namespace EasyPost.Parameters.V2
{
    public static class EndShippers
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.EndShipperService.Create"/> API calls.
        /// </summary>
        public sealed class Create : Addresses.Create
        {
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.EndShipperService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.EndShipper"/> update API calls.
        /// </summary>
        public sealed class Update : Addresses.Update
        {
        }
    }
}
