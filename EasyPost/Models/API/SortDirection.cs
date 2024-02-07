using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Enum representing the sort direction for records.
    /// </summary>
    public class SortDirection : ValueEnum
    {
        /// <summary>
        ///     Ascending sort direction.
        /// </summary>
        public static readonly SortDirection Ascending = new(1, "asc");

        /// <summary>
        ///     Descending sort direction.
        /// </summary>
        public static readonly SortDirection Descending = new(2, "desc");

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaymentMethodType"/> class.
        /// </summary>
        /// <param name="id">The internal ID of this enum.</param>
        /// <param name="endpoint">The endpoint associated with this enum.</param>
        private SortDirection(int id, string endpoint)
            : base(id, endpoint)
        {
        }
    }
}
