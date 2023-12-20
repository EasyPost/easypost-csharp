// ReSharper disable once RedundantUsingDirective
using System.Net.Http;
using System.Runtime.CompilerServices;
using EasyVCR;

// ReSharper disable once CheckNamespace
namespace EasyPost.Integration.Utilities
{
    public class Utils
    {
        internal const string ApiKeyFailedToPull = "couldnotpullapikey";

        private static readonly List<string> BodyCensors =
        [
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
        ];

        private static readonly List<string> HeaderCensors =
        [
            "Authorization",
            "User-Agent"
        ];

        private static readonly List<string> QueryCensors =
        [
            "card[number]",
            "card[cvc]"
        ];

        public enum ApiKey
        {
            Test,
            Production,
            Partner,
            Referral,
            Mock,
        }

        public static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "") => Path.GetDirectoryName(sourceFilePath)!;

        internal static string GetApiKey(ApiKey apiKey)
        {
            string keyName = apiKey switch
            {
                ApiKey.Test => "EASYPOST_TEST_API_KEY",
                ApiKey.Production => "EASYPOST_PROD_API_KEY",
                ApiKey.Partner => "PARTNER_USER_PROD_API_KEY",
                ApiKey.Referral => "REFERRAL_CUSTOMER_PROD_API_KEY",
                ApiKey.Mock => "EASYPOST_MOCK_API_KEY", // does not exist, will trigger to use ApiKeyFailedToPull
#pragma warning disable CA2201
                var _ => throw new Exception(Constants.ErrorMessages.InvalidApiKeyType)
#pragma warning restore CA2201
            };

            return Environment.GetEnvironmentVariable(keyName) ?? ApiKeyFailedToPull; // if can't pull from environment, will use a fake key. Won't matter on replay.
        }

        // ReSharper disable once InconsistentNaming
        internal static Client GetBasicVCRClient(string apiKey, HttpClient? vcrClient = null) => new(new ClientConfiguration(apiKey)
        {
            CustomHttpClient = vcrClient,
        });

        internal static string ReadFile(string path)
        {
            string filePath = Path.Combine(GetSourceFileDirectory(), path);
            return File.ReadAllText(filePath);
        }

        internal static string NetVersion
        {
            get
            {
                string netVersion = "net";
#if NET462
                netVersion = "netstandard";
#endif

                return netVersion;
            }
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

                string netVersionFolder = NetVersion;

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

            internal Client SetUpTest(string cassetteName, Func<string, HttpClient, Client> getClientFunc, string? overrideApiKey = null)
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
                return getClientFunc(apiKey, _vcr.Client);
            }

            internal Client SetUpTest(string cassetteName, string? overrideApiKey = null)
            {
                return SetUpTest(cassetteName, GetBasicVCRClient, overrideApiKey);
            }
        }
    }
}
