using System.Collections.Generic;
using System.Linq;
using EasyPost.Models.Shared;
using EasyPost.Parameters.Beta.User;
using Newtonsoft.Json;

namespace EasyPost.Models.API.Beta;

/// <summary>
///     Class representing a collection of EasyPost Child <see cref="User"/>s.
/// </summary>
public class ChildUserCollection : PaginatedCollection<User>
{
    #region JSON Properties

    /// <summary>
    ///     The Child <see cref="User"/>s in the collection.
    /// </summary>
    [JsonProperty("children")]
    public List<User>? Children { get; set; }

    #endregion

    /// <summary>
    ///     Construct the parameter set for retrieving the next page of this paginated collection.
    /// </summary>
    /// <param name="entries">The entries on the current page of this paginated collection.</param>
    /// <param name="pageSize">The request size of the next page.</param>
    /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
    /// <returns>A TParameters-type parameters set.</returns>
    public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<User> entries, int? pageSize = null)
    {
        AllChildren parameters = Filters != null ? (AllChildren)Filters : new AllChildren();

        parameters.BeforeId = entries.Last().Id;

        if (pageSize != null)
        {
            parameters.PageSize = pageSize;
        }

        return (parameters as TParameters)!;
    }
}
