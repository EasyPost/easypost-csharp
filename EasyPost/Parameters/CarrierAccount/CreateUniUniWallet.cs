using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/carrier-accounts#create-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(ACreate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateUniUniWallet : Create
    {
        /// <inheritdoc cref="EasyPost.Parameters.CarrierAccount.ACreate.Endpoint"/>
        internal override string Endpoint => Constants.CarrierAccounts.OpenCreateEndpoint;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateUniUniWallet"/> class.
        /// </summary>
        public CreateUniUniWallet()
            : base(CarrierAccountType.UniUniWalletAccount)
        {
        }
    }
}
