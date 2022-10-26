using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions;
using EasyPost.Utilities;
using EasyVCR;
using RestSharp;

namespace EasyPost.Tests._Utilities
{
    public class TestUtils
    {
        internal const string ApiKeyFailedToPull = "couldnotpullapikey";

        private static readonly List<string> BodyCensors = new()
        {
            "api_keys",
            "children",
            "client_ip",
            "credentials",
            "email",
            "key",
            "keys",
            "phone_number",
            "phone",
            "test_credentials"
        };

        private static readonly List<string> HeaderCensors = new()
        {
            "Authorization",
            "User-Agent"
        };

        private static readonly List<string> QueryCensors = new()
        {
            "card[number]",
            "card[cvc]"
        };

        public enum ApiKey
        {
            Test,
            Production,
            Partner,
            Referral,
            Mock
        }

        public static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "") => Path.GetDirectoryName(sourceFilePath);

        internal static string GetApiKey(ApiKey apiKey)
        {
            string keyName = "";
            switch (apiKey)
            {
                case ApiKey.Test:
                    keyName = "EASYPOST_TEST_API_KEY";
                    break;
                case ApiKey.Production:
                    keyName = "EASYPOST_PROD_API_KEY";
                    break;
                case ApiKey.Partner:
                    keyName = "PARTNER_USER_PROD_API_KEY";
                    break;
                case ApiKey.Referral:
                    keyName = "REFERRAL_USER_PROD_API_KEY";
                    break;
                case ApiKey.Mock:
                    keyName = "EASYPOST_MOCK_API_KEY"; // does not exist, will trigger to use ApiKeyFailedToPull
                    break;
                default:
                    throw new Exception(Constants.ErrorMessages.InvalidApiKeyType);
            }

            return Environment.GetEnvironmentVariable(keyName) ?? ApiKeyFailedToPull; // if can't pull from environment, will use a fake key. Won't matter on replay.
        }

        internal static Client GetClient(string apiKey, HttpClient? vcrClient = null) => new(apiKey, customHttpClient: vcrClient);

        internal static string ReadFile(string path)
        {
            string filePath = Path.Combine(GetSourceFileDirectory(), path);
            return File.ReadAllText(filePath);
        }

        public class VCR
        {
            // Cassettes folder will always been in the same directory as this TestUtils.cs file
            private const string CassettesFolder = "cassettes";

            private readonly string _apiKey;

            private readonly string _testCassettesFolder;

            private readonly EasyVCR.VCR _vcr;

            public VCR(string? testCassettesFolder = null, ApiKey apiKey = ApiKey.Test)
            {
                Censors censors = new("<REDACTED>");
                censors.CensorHeadersByKeys(HeaderCensors);
                censors.CensorQueryParametersByKeys(QueryCensors);
                censors.CensorBodyElementsByKeys(BodyCensors);

                AdvancedSettings advancedSettings = new()
                {
                    MatchRules = MatchRules.DefaultStrict,
                    Censors = censors,
                    SimulateDelay = false,
                    ManualDelay = 0,
                    ValidTimeFrame = TimeFrame.Months6,
                    WhenExpired = ExpirationActions.Warn
                };
                _vcr = new EasyVCR.VCR(advancedSettings);

                _apiKey = GetApiKey(apiKey);

                _testCassettesFolder = Path.Combine(GetSourceFileDirectory(), CassettesFolder); // create "cassettes" folder in same directory as test files

                string netVersionFolder = "net";
#if NET462
                netVersionFolder = "netstandard";
#endif

                _testCassettesFolder = Path.Combine(_testCassettesFolder, netVersionFolder); // create .NET version-specific folder in "cassettes" folder

                if (testCassettesFolder != null)
                {
                    _testCassettesFolder = Path.Combine(_testCassettesFolder, testCassettesFolder); // create test group folder in .NET version-specific folder
                }

                // if folder doesn't exist, create it
                if (!Directory.Exists(_testCassettesFolder))
                {
                    Directory.CreateDirectory(_testCassettesFolder);
                }
            }

            internal bool IsRecording() => _vcr.Mode == Mode.Record;

            internal Client SetUpTest(string cassetteName, string? overrideApiKey = null)
            {
                // override api key if needed
                string apiKey = overrideApiKey ?? _apiKey;

                // set up cassette
                Cassette cassette = new(_testCassettesFolder, cassetteName, new CassetteOrder.Alphabetical());

                // add cassette to vcr
                _vcr.Insert(cassette);

                string filePath = Path.Combine(_testCassettesFolder, cassetteName + ".json");
                if (!File.Exists(filePath))
                {
                    // if cassette doesn't exist, switch to record mode
                    _vcr.Record();
                }
                else
                {
                    // if cassette exists, switch to replay mode
                    _vcr.Replay();
                }

                // get EasyPost client
                return GetClient(apiKey, _vcr.Client);
            }
        }

        public class MockRequestMatchRules
        {
            internal Method Method { get; set; }

            internal string ResourceRegex { get; set; }

            public MockRequestMatchRules(Method method, string resourceRegex)
            {
                Method = method;
                ResourceRegex = resourceRegex;
            }
        }

        public class MockRequestResponseInfo
        {
            internal HttpStatusCode StatusCode { get; set; }

            internal string? Content { get; set; }

            public MockRequestResponseInfo(HttpStatusCode statusCode, string? content = null, object? data = null)
            {
                StatusCode = statusCode;
                Content = content ?? JsonSerialization.ConvertObjectToJson(data);
            }
        }

        public class MockRequest
        {
            public MockRequestMatchRules MatchRules { get; }

            public MockRequestResponseInfo ResponseInfo { get; }

            internal MockRequest(MockRequestMatchRules matchRules, MockRequestResponseInfo responseInfo)
            {
                MatchRules = matchRules;
                ResponseInfo = responseInfo;
            }
        }

        internal class MockClient : Client
        {
            private readonly List<MockRequest> _mockRequests = new();

            internal override async Task<RestResponse<T>> ExecuteRequest<T>(RestRequest request)
            {
                var mockRequest = FindMatchingMockRequest(request);

                if (mockRequest == null)
                {
                    throw new Exception($"No matching mock request found for: {request.Method.ToString().ToUpper()} {request.Resource}");
                }

                return new RestResponse<T>
                {
                    Content = mockRequest.ResponseInfo.Content,
                    StatusCode = mockRequest.ResponseInfo.StatusCode,
                    Data = mockRequest.ResponseInfo.Content != null ? JsonSerialization.ConvertJsonToObject<T>(mockRequest.ResponseInfo.Content) : default
                };
            }

            internal override async Task<RestResponse> ExecuteRequest(RestRequest request)
            {
                var mockRequest = FindMatchingMockRequest(request);

                if (mockRequest == null)
                {
                    throw new Exception("No matching mock request found");
                }

                return new RestResponse
                {
                    Content = mockRequest.ResponseInfo.Content,
                    StatusCode = mockRequest.ResponseInfo.StatusCode
                };
            }

            internal MockClient(EasyPostClient client) : base(client.Configuration.ApiKey, client.Configuration.ApiBase, client.Configuration.HttpClient)
            {
            }

            internal void AddMockRequest(MockRequest mockRequest)
            {
                _mockRequests.Add(mockRequest);
            }

            internal void AddMockRequests(IEnumerable<MockRequest> mockRequests)
            {
                _mockRequests.AddRange(mockRequests);
            }

            private MockRequest FindMatchingMockRequest(RestRequest request)
            {
                return _mockRequests.FirstOrDefault(
                    mock => mock.MatchRules.Method == request.Method &&
                            EndpointMatches(request.Resource, mock.MatchRules.ResourceRegex));
            }

            private static bool EndpointMatches(string endpoint, string pattern)
            {
                try
                {
                    return Regex.IsMatch(endpoint,
                        pattern,
                        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Singleline,
                        TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
        }
    }
}
