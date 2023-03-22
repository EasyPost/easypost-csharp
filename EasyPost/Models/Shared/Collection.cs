using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    public abstract class Collection : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("has_more")]
        public bool? HasMore { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of a paginated collection.
        /// </summary>
        /// <param name="apiCallFunction">The function to execute to retrieve a page (most likely an All function).</param>
        /// <param name="currentEntries">The results on the current page. Used to determine the API call parameters to retrieve the next page.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <typeparam name="TCollection">The type of <see cref="Collection"/> to get the next page of.</typeparam>
        /// <typeparam name="TEntries">The type of <see cref="EasyPost._base.EasyPostObject"/> the entries are.</typeparam>
        /// <typeparam name="TParameters">The type of <see cref="EasyPost.BetaFeatures.Parameters.BaseParameters"/> to construct for the API call.</typeparam>
        /// <returns>The next page of a paginated collection.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        internal async Task<TCollection> GetNextPage<TCollection, TEntries, TParameters>(Func<TParameters, Task<TCollection>> apiCallFunction, List<TEntries>? currentEntries, int? pageSize = null) where TCollection : Collection where TEntries : EasyPost._base.EasyPostObject where TParameters : BetaFeatures.Parameters.BaseParameters
        {
            if (currentEntries == null || currentEntries.Count == 0)
            {
                throw new EndOfPaginationError();
            }

            if (this.HasMore == null || !(bool)this.HasMore)
            {
                throw new EndOfPaginationError();
            }

            TParameters parameters = BuildNextPageParameters<TEntries, TParameters>(currentEntries, pageSize);

            return await apiCallFunction(parameters);
        }

        /// <summary>
        ///     Build the parameters to retrieve the next page of a paginated collection.
        /// </summary>
        /// <param name="entries">The entries of the collection.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <typeparam name="TEntries">The type of <see cref="EasyPost._base.EasyPostObject"/> the entries are.</typeparam>
        /// <typeparam name="TParameters">The type of <see cref="EasyPost.BetaFeatures.Parameters.BaseParameters"/> to construct for the API call.</typeparam>
        /// <returns>A T2-type set of parameters to use for the subsequent API call.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there are no more items to retrieve for the paginated collection.</exception>
        // This method is abstract and must be implemented for each collection.
        protected internal abstract TParameters BuildNextPageParameters<TEntries, TParameters>(IEnumerable<TEntries> entries, int? pageSize = null) where TEntries : EasyPost._base.EasyPostObject where TParameters : BetaFeatures.Parameters.BaseParameters;
    }
}
