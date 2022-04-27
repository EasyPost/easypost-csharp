using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Interfaces;
using EasyPost.Models;
using EasyPost.Services;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost
{
    public class Client
    {
        private const int DefaultConnectTimeoutMilliseconds = 30000;
        private const int DefaultRequestTimeoutMilliseconds = 60000;

        private readonly ClientConfiguration _configuration;

        private readonly string _dotNetVersion;
        private readonly string _libraryVersion;

        private readonly RestClient _restClient;
        private int? _connectTimeoutMilliseconds;
        private int? _requestTimeoutMilliseconds;

        public AddressService Addresses
        {
            get { return new AddressService(this); }
        }

        public ApiKeyService ApiKeys
        {
            get { return new ApiKeyService(this); }
        }

        public BatchService Batches
        {
            get { return new BatchService(this); }
        }

        public CarrierAccountService CarrierAccounts
        {
            get { return new CarrierAccountService(this); }
        }

        public CarrierTypeService CarrierTypes
        {
            get { return new CarrierTypeService(this); }
        }

        public int ConnectTimeoutMilliseconds
        {
            get => _connectTimeoutMilliseconds ?? DefaultConnectTimeoutMilliseconds;
            set => _connectTimeoutMilliseconds = value;
        }

        public CustomsInfoService CustomsInfo
        {
            get { return new CustomsInfoService(this); }
        }

        public CustomsItemService CustomsItems
        {
            get { return new CustomsItemService(this); }
        }

        public EventService Events
        {
            get { return new EventService(this); }
        }

        public InsuranceService Insurance
        {
            get { return new InsuranceService(this); }
        }

        public OrderService Orders
        {
            get { return new OrderService(this); }
        }

        public ParcelService Parcels
        {
            get { return new ParcelService(this); }
        }

        public PickupService Pickups
        {
            get { return new PickupService(this); }
        }

        public RateService Rates
        {
            get { return new RateService(this); }
        }

        public RefundService Refunds
        {
            get { return new RefundService(this); }
        }

        public ReportService Reports
        {
            get { return new ReportService(this); }
        }

        public int RequestTimeoutMilliseconds
        {
            get => _requestTimeoutMilliseconds ?? DefaultRequestTimeoutMilliseconds;
            set => _requestTimeoutMilliseconds = value;
        }

        public ScanFormService ScanForms
        {
            get { return new ScanFormService(this); }
        }

        public ShipmentService Shipments
        {
            get { return new ShipmentService(this); }
        }

        public TrackerService Trackers
        {
            get { return new TrackerService(this); }
        }

        public UserService Users
        {
            get { return new UserService(this); }
        }

        public WebhookService Webhooks
        {
            get { return new WebhookService(this); }
        }

        private string UserAgent => $"EasyPost/v2 CSharpClient/{_libraryVersion} .NET/{_dotNetVersion}";

        /// <summary>
        ///     Constructor for the EasyPost client.
        /// </summary>
        /// <param name="clientConfiguration">EasyPost.ClientConfiguration object instance to use to configure this client.</param>
        /// <param name="customHttpClient">Custom HttpClient to pass into RestSharp if needed. Mostly for debug purposes, not advised for general use.</param>
        public Client(ClientConfiguration clientConfiguration, HttpClient? customHttpClient = null)
        {
            ServicePointManager.SecurityProtocol |= Security.GetProtocol();
            _configuration = clientConfiguration ?? throw new ArgumentNullException(nameof(clientConfiguration));

            try
            {
                Assembly assembly = typeof(Client).Assembly;
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
                _libraryVersion = info.FileVersion ?? "Unknown";
            }
            catch (Exception)
            {
                _libraryVersion = "Unknown";
            }

            _dotNetVersion = Environment.Version.ToString();

            RestClientOptions clientOptions = new RestClientOptions
            {
                Timeout = ConnectTimeoutMilliseconds,
                BaseUrl = new Uri(clientConfiguration.ApiBase),
                UserAgent = UserAgent
            };

            _restClient = customHttpClient != null ? new RestClient(customHttpClient, clientOptions) : new RestClient(clientOptions);
        }

        public Client(string apiKey, HttpClient? customHttpClient = null) : this(new ClientConfiguration(apiKey), customHttpClient)
        {
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        /// <exception cref="HttpException">An error occurred during the API request.</exception>
        internal async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, method, parameters, rootElement);
            RestRequest restRequest = PrepareRequest(request);
            RestResponse<T> response = await _restClient.ExecuteAsync<T>(restRequest);
            int statusCode = Convert.ToInt32(response.StatusCode);

            List<string>? rootElements = null;
            if (request.RootElement != null)
            {
                rootElements = new List<string>
                {
                    request.RootElement
                };
            }

            if (statusCode < 400)
            {
                var resource = JsonSerialization.ConvertJsonToObject<T>(response, null, rootElements);
                ((resource as Resource)!).Client = this;
                return resource;
            }

            Dictionary<string, Dictionary<string, object>> body;
            List<Error> errors;

            try
            {
                body = JsonSerialization.ConvertJsonToObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                errors = JsonSerialization.ConvertJsonToObject<List<Error>>(response.Content, null, new List<string>
                {
                    "error",
                    "errors"
                });
            }
            catch
            {
                throw new HttpException(statusCode, "RESPONSE.PARSE_ERROR", response.Content, new List<Error>());
            }

            throw new HttpException(
                statusCode,
                (string)body["error"]["code"],
                (string)body["error"]["message"],
                errors
            );
        }

        /// <summary>
        ///     Execute a request against the EasyPost API.
        /// </summary>
        /// <returns>Whether request was successful.</returns>
        internal async Task<bool> Request(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null)
        {
            Request request = new Request(url, method, parameters, rootElement);
            RestRequest restRequest = PrepareRequest(request);
            RestResponse response = await _restClient.ExecuteAsync(restRequest);
            return response.IsSuccessful;
        }

        /// <summary>
        ///     Prepare a request for execution by attaching required headers.
        /// </summary>
        /// <param name="request">EasyPost.Request object instance to prepare.</param>
        /// <returns>RestSharp.RestRequest object instance to execute.</returns>
        private RestRequest PrepareRequest(Request request)
        {
            request.Build();

            RestRequest restRequest = (RestRequest)request;
            restRequest.Timeout = RequestTimeoutMilliseconds;
            restRequest.AddHeader("authorization", "Bearer " + _configuration.ApiKey);
            restRequest.AddHeader("content_type", "application/json");

            return restRequest;
        }
    }
}
