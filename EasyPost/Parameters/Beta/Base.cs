using EasyPost._base;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.Beta
{
    /// <summary>
    ///     Base class for all parameters used in requests to <see cref="ApiVersion.Beta"/> endpoints.
    /// </summary>
#pragma warning disable SA1649
    public abstract class RequestParameters
#pragma warning restore SA1649
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestParameters"/> class.
        /// </summary>
        internal RequestParameters()
        {
        }
    }

    /// <summary>
    ///     Base class for all parameters used in "create" requests to <see cref="ApiVersion.Beta"/> endpoints.
    /// </summary>
    public abstract class CreateRequestParameters : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRequestParameters"/> class.
        /// </summary>
        internal CreateRequestParameters()
        {
        }
    }

    /// <summary>
    ///     Base class for all parameters used in "update" requests to <see cref="ApiVersion.Beta"/> endpoints.
    /// </summary>
    public abstract class UpdateRequestParameters : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequestParameters"/> class.
        /// </summary>
        internal UpdateRequestParameters()
        {
        }
    }

    /// <summary>
    ///     Base class for all parameters used in "list" requests to <see cref="ApiVersion.Beta"/> endpoints.
    /// </summary>
    public abstract class AllRequestParameters : RequestParameters
    {
        #region Request Parameters

        /// <summary>
        ///     Only records created after the given ID will be included. May not be used with <see cref="EasyPost.Parameters.Beta.AllRequestParameters.BeforeId"/>.
        /// </summary>
        [RequestParameter(Necessity.Optional, "after_id")]
        public string? AfterId { get; set; }

        /// <summary>
        ///     Only records created before the given ID will be included. May not be used with <see cref="EasyPost.Parameters.Beta.AllRequestParameters.AfterId"/>.
        /// </summary>
        [RequestParameter(Necessity.Optional, "before_id")]
        public string? BeforeId { get; set; }

        /// <summary>
        ///     Only return records created before this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="EasyPost.Parameters.Beta.AllRequestParameters.StartDatetime"/>.
        /// </summary>
        [RequestParameter(Necessity.Optional, "end_datetime")]
        public string? EndDatetime { get; set; }

        /// <summary>
        ///     The number of records to return on each page. The maximum value is 100, and default is 20.
        /// </summary>
        [RequestParameter(Necessity.Optional, "page_size")]
        public int PageSize { get; set; } = 20;

        /// <summary>
        ///     Only return records created after this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="EasyPost.Parameters.Beta.AllRequestParameters.EndDatetime"/>.
        /// </summary>
        [RequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="AllRequestParameters"/> class.
        /// </summary>
        internal AllRequestParameters()
        {
        }
    }
}
