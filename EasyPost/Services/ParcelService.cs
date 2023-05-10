using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#parcels">parcel-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ParcelService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParcelService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal ParcelService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="Parcel"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-parcel">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Parcel"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Parcel"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Parcel> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("parcel");
            return await RequestAsync<Parcel>(Method.Post, "parcels", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Parcel"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-parcel">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Parcel"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Parcel"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Parcel> Create(Parameters.Parcel.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Parcel>(Method.Post, "parcels", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a <see cref="Parcel"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-parcel">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Parcel"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The requested <see cref="Parcel"/>.</returns>
        [CrudOperations.Read]
        public async Task<Parcel> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Parcel>(Method.Get, $"parcels/{id}", cancellationToken);

        #endregion
    }
}
