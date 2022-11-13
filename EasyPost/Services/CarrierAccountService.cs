using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public class CarrierAccountService : EasyPostService
{
    internal CarrierAccountService(EasyPostClient client) : base(client)
    {
    }

    #region CRUD Operations

    /// <summary>
    ///     Create a CarrierAccount.
    /// </summary>
    /// <param name="parameters">
    ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
    ///     * {"type", string} Required (e.g. EndiciaAccount, UPSAccount, etc.).
    ///     * {"reference", string} External reference for carrier account.
    ///     * {"description", string} Description of carrier account.
    ///     * {"credentials", Dictionary&lt;string, string&gt;}
    ///     * {"test_credentials", Dictionary&lt;string, string&gt;}
    ///     All invalid keys will be ignored.
    /// </param>
    /// <returns>EasyPost.CarrierAccount instance.</returns>
    [CrudOperations.Create]
    public async Task<CarrierAccount> Create(Dictionary<string, object> parameters)
    {
        parameters = parameters.Wrap("carrier_account");
        return await Request<CarrierAccount>(Method.Post, "carrier_accounts", parameters);
    }

    /// <summary>
    ///     List all available carrier accounts.
    /// </summary>
    /// <returns>A list of EasyPost.CarrierAccount instances.</returns>
    [CrudOperations.Read]
    public async Task<List<CarrierAccount>> All() => await Request<List<CarrierAccount>>(Method.Get, "carrier_accounts");

    /// <summary>
    ///     Retrieve a CarrierAccount from its id.
    /// </summary>
    /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
    /// <returns>EasyPost.CarrierAccount instance.</returns>
    [CrudOperations.Read]
    public async Task<CarrierAccount> Retrieve(string id) => await Request<CarrierAccount>(Method.Get, $"carrier_accounts/{id}");

    /// <summary>
    ///     Update this CarrierAccount.
    /// </summary>
    /// <param name="parameters">See CarrierAccount.Create for more details.</param>
    [CrudOperations.Update]
    public async Task<CarrierAccount> Update(string id, Dictionary<string, object> parameters)
    {
        parameters = parameters.Wrap("carrier_account");
        return await Request<CarrierAccount>(Method.Patch, $"carrier_accounts/{id}", parameters);
    }

    /// <summary>
    ///     Remove this CarrierAccount from your account.
    /// </summary>
    /// <returns>Whether the request was successful or not.</returns>
    [CrudOperations.Delete]
    public async Task Delete(string id) => await Request(Method.Delete, $"carrier_accounts/{id}");

    #endregion
}
