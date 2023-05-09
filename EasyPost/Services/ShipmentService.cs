using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#shipments">shipment-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ShipmentService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ShipmentService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal ShipmentService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Shipment"/>.</param>
        /// <param name="withCarbonOffset">Whether to include a carbon offset fee for the <see cref="Shipment"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Shipment"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Shipment> Create(Dictionary<string, object> parameters, bool withCarbonOffset = false, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("shipment");
            parameters.Add("carbon_offset", withCarbonOffset);
            return await RequestAsync<Shipment>(Method.Post, "shipments", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Shipment"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Shipment"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Shipment> Create(BetaFeatures.Parameters.Shipments.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Shipment>(Method.Post, "shipments", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="Shipment"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-shipments">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Shipment"/>s on.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ShipmentCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ShipmentCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            ShipmentCollection collection = await RequestAsync<ShipmentCollection>(Method.Get, "shipments", cancellationToken, parameters);
            collection.Filters = BetaFeatures.Parameters.Shipments.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Shipment"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-shipments">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="Shipment"/>s on.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ShipmentCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ShipmentCollection> All(BetaFeatures.Parameters.Shipments.All parameters, CancellationToken cancellationToken = default)
        {
            ShipmentCollection collection = await RequestAsync<ShipmentCollection>(Method.Get, "shipments", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ShipmentCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-shipments">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="ShipmentCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="ShipmentCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ShipmentCollection> GetNextPage(ShipmentCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ShipmentCollection, BetaFeatures.Parameters.Shipments.All>(async parameters => await All(parameters, cancellationToken), collection.Shipments, pageSize);

        /// <summary>
        ///     Retrieve a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The requested <see cref="Shipment"/>.</returns>
        [CrudOperations.Read]
        public async Task<Shipment> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Shipment>(Method.Get, $"shipments/{id}", cancellationToken);

        /// <summary>
        ///     Get the <see cref="SmartRate"/>s for a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-time-in-transit-statistics-across-all-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to get rates for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="SmartRate"/>s.</returns>
        [CrudOperations.Read]
        public async Task<List<SmartRate>> GetSmartRates(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<SmartRate>>(Method.Get, $"shipments/{id}/smartrate", cancellationToken, rootElement: "result");
        }

        /// <summary>
        ///     Retrieve the estimated delivery date of each rate for a <see cref="Shipment"/> via the SmartRates API.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-estimated-delivery-date-and-total-transit-days-across-all-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to get rates for.</param>
        /// <param name="plannedShipDate">The planned shipment date.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RateWithEstimatedDeliveryDate"/>s.</returns>
        [CrudOperations.Read]
        public async Task<List<RateWithEstimatedDeliveryDate>> RetrieveEstimatedDeliveryDate(string id, string plannedShipDate, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "planned_ship_date", plannedShipDate },
            };
            return await RequestAsync<List<RateWithEstimatedDeliveryDate>>(Method.Get, $"shipments/{id}/smartrate/delivery_date", cancellationToken, parameters, "rates");
        }

        /// <summary>
        ///     Retrieve the estimated delivery date of each rate for a <see cref="Shipment"/> via the SmartRates API.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-estimated-delivery-date-and-total-transit-days-across-all-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to get rates for.</param>
        /// <param name="parameters">The <see cref="BetaFeatures.Parameters.Shipments.RetrieveEstimatedDeliveryDate"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RateWithEstimatedDeliveryDate"/>s.</returns>
        [CrudOperations.Read]
        public async Task<List<RateWithEstimatedDeliveryDate>> RetrieveEstimatedDeliveryDate(string id, BetaFeatures.Parameters.Shipments.RetrieveEstimatedDeliveryDate parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<RateWithEstimatedDeliveryDate>>(Method.Get, $"shipments/{id}/smartrate/delivery_date", cancellationToken, parameters.ToDictionary(), "rates");
        }

        /// <summary>
        ///     Purchase a label for a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#buy-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to purchase the label for.</param>
        /// <param name="rateId">The ID of the <see cref="Rate"/> to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the <see cref="Shipment"/> for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The ID of the <see cref="EndShipper"/> to use for this purchase.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The purchased <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, string? rateId, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null, CancellationToken cancellationToken = default)
        {
            if (rateId == null)
            {
                throw new MissingParameterError("rateId");
            }

            Dictionary<string, object> parameters = new()
            {
                { "rate", new Dictionary<string, object> { { "id", rateId } } },
                { "insurance", insuranceValue ?? string.Empty },
                { "carbon_offset", withCarbonOffset },
            };

            if (endShipperId != null)
            {
                parameters.Add("end_shipper", endShipperId);
            }

            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/buy", cancellationToken, parameters);
        }

        /// <summary>
        ///     Purchase a label for a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#buy-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to purchase the label for.</param>
        /// <param name="rate">The <see cref="Rate"/> to purchase the <see cref="Shipment"/> with.</param>
        /// <param name="insuranceValue">The value to insure the <see cref="Shipment"/> for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The ID of the <see cref="EndShipper"/> to use for this purchase.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The purchased <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, Rate rate, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null, CancellationToken cancellationToken = default)
        {
            if (rate == null)
            {
                throw new MissingParameterError("rate");
            }

            return await Buy(id, rate.Id, insuranceValue, withCarbonOffset, endShipperId, cancellationToken);
        }

        /// <summary>
        ///     Purchase a label for this <see cref="Shipment"/> with the given <see cref="Rate"/>.
        ///     <a href="https://www.easypost.com/docs/api#buy-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to purchase the label for.</param>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.Buy"/> parameters set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, BetaFeatures.Parameters.Shipments.Buy parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/buy", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Generate a <see cref="PostageLabel"/> for a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#convert-the-label-format-of-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to generate a label for.</param>
        /// <param name="fileFormat">Format to generate the <see cref="PostageLabel"/> in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string id, string fileFormat, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };

            return await RequestAsync<Shipment>(Method.Get, $"shipments/{id}/label", cancellationToken, parameters);
        }

        /// <summary>
        ///     Generate a <see cref="PostageLabel"/> for a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#convert-the-label-format-of-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to generate a label for.</param>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.GenerateLabel"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string id, BetaFeatures.Parameters.Shipments.GenerateLabel parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Shipment>(Method.Get, $"shipments/{id}/label", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Insure a <see cref="Shipment"/> for the given amount.
        ///     <a href="https://www.easypost.com/docs/api#insure-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to insure.</param>
        /// <param name="amount">The amount to insure the <see cref="Shipment"/> for. Currency is provided when creating a <see cref="Shipment"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(string id, double amount, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "amount", amount } };

            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/insure", cancellationToken, parameters);
        }

        /// <summary>
        ///     Insure a <see cref="Shipment"/> for the given amount.
        ///     <a href="https://www.easypost.com/docs/api#insure-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to insure.</param>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.Insure"/> parameters set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(string id, BetaFeatures.Parameters.Shipments.Insure parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/insure", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Send a refund request to the carrier a <see cref="Shipment"/> was purchased from.
        ///     <a href="https://www.easypost.com/docs/api#refund-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to refund.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Refund(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/refund", cancellationToken);
        }

        /// <summary>
        ///     Refresh the <see cref="Rate"/>s for a <see cref="Shipment"/>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to refresh rates for.</param>
        /// <param name="parameters">Optional dictionary of parameters for the API request.</param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when re-rating the shipment.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> RegenerateRates(string id, Dictionary<string, object>? parameters = null, bool withCarbonOffset = false, CancellationToken cancellationToken = default)
        {
            parameters ??= new Dictionary<string, object>();

            parameters.Add("carbon_offset", withCarbonOffset);

            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/rerate", cancellationToken, parameters);
        }

        /// <summary>
        ///     Refresh the <see cref="Models.API.Rate"/>s for a <see cref="Shipment"/>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Shipment"/> to refresh rates for.</param>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.RegenerateRates"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="Shipment"/>.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> RegenerateRates(string id, BetaFeatures.Parameters.Shipments.RegenerateRates parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Shipment>(Method.Post, $"shipments/{id}/rerate", cancellationToken, parameters.ToDictionary());
        }

        #endregion
    }
}
