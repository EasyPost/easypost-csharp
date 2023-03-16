using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    public class Collection : EasyPostObject
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
        /// <typeparam name="T">The type of <see cref="Collection"/> to get the next page of.</typeparam>
        /// <typeparam name="T2">The type of <see cref="EasyPost._base.EasyPostObject"/> the entries are.</typeparam>
        /// <returns>The next page of a paginated collection.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        internal async Task<T> GetNextPage<T, T2>(Func<Dictionary<string, object>, Task<T>> apiCallFunction, List<T2>? currentEntries) where T : Collection where T2 : EasyPost._base.EasyPostObject
        {
            Dictionary<string, object> parameters = BuildNextPageParameters<T2>(currentEntries);

            return await apiCallFunction(parameters);
        }

        /// <summary>
        ///     Build the parameters to retrieve the next page of a paginated collection.
        /// </summary>
        /// <param name="entries">The entries of the collection.</param>
        /// <typeparam name="T">The type of <see cref="EasyPost._base.EasyPostObject"/> the entries are.</typeparam>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of parameters to use for the subsequent API call.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there are no more items to retrieve for the paginated collection.</exception>
        protected internal virtual Dictionary<string, object> BuildNextPageParameters<T>(List<T>? entries) where T : EasyPost._base.EasyPostObject
        {
            // This method is virtual so that it can be overridden by specific collection types that need to use different parameters (e.g. EndShipperCollection doesn't use "before_id").

            if (entries == null || entries.Count == 0)
            {
                throw new EndOfPaginationError();
            }

            if (HasMore == null || !(bool)HasMore)
            {
                throw new EndOfPaginationError();
            }

            string? lastId = entries.Last()!.Id;

            return new Dictionary<string, object>
            {
                { "before_id", lastId! },
            };
        }
    }
}
