using System.Diagnostics.CodeAnalysis;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#regenerate-rates-for-a-shipment">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.RegenerateRates(string, RegenerateRates, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RegenerateRates : BaseParameters<Models.API.Shipment>
    {
    }
}
