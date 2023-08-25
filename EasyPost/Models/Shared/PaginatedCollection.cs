using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    /// <summary>
    ///     A <a href="https://www.easypost.com/docs/api#pagination">paginated collection</a> of <see cref="EasyPost._base.EasyPostObject"/>s.
    /// </summary>
    public abstract class PaginatedCollection<TEntries> : _base.EasyPostObject where TEntries : _base.EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("has_more")]
        public bool? HasMore { get; set; }

        #endregion

        /// <summary>
        ///     The filter parameters used to retrieve this collection.
        /// </summary>
        internal Parameters.BaseParameters<TEntries>? Filters { get; set; }

        /// <summary>
        ///     Get the next page of a paginated collection.
        /// </summary>
        /// <param name="apiCallFunction">The function to execute to retrieve a page (most likely an All function).</param>
        /// <param name="currentEntries">The results on the current page. Used to determine the API call parameters to retrieve the next page.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <typeparam name="TCollection">The type of <see cref="PaginatedCollection{TCollection}"/> to get the next page of.</typeparam>
        /// <typeparam name="TParameters">The type of <see cref="Parameters.BaseParameters"/> to construct for the API call.</typeparam>
        /// <returns>The next page of a paginated collection.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        internal async Task<TCollection> GetNextPage<TCollection, TParameters>(Func<TParameters, Task<TCollection>> apiCallFunction, List<TEntries>? currentEntries, int? pageSize = null) where TCollection : PaginatedCollection<TEntries> where TParameters : Parameters.BaseParameters<TEntries>
        {
            if (currentEntries == null || currentEntries.Count == 0)
            {
                throw new EndOfPaginationError();
            }

            if (HasMore == null || !(bool)HasMore)
            {
                throw new EndOfPaginationError();
            }

            TParameters parameters = BuildNextPageParameters<TParameters>(currentEntries, pageSize);

            return await apiCallFunction(parameters);
        }

        /// <summary>
        ///     Build the parameters to retrieve the next page of a paginated collection.
        /// </summary>
        /// <param name="entries">The entries of the collection.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <typeparam name="TParameters">The type of <see cref="Parameters.BaseParameters"/> to construct for the API call.</typeparam>
        /// <returns>A TParameters-type set of parameters to use for the subsequent API call.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there are no more items to retrieve for the paginated collection.</exception>
        // This method is abstract and must be implemented for each collection.
        protected internal abstract TParameters BuildNextPageParameters<TParameters>(IEnumerable<TEntries> entries, int? pageSize = null) where TParameters : Parameters.BaseParameters<TEntries>;
    }
}
