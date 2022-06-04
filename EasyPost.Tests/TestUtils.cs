using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyVCR;

namespace EasyPost.Tests
{
    public class TestUtils
    {
        private const string ApiKeyFailedToPull = "couldnotpullapikey";

        private const string CassettesFolder = "cassettes";

        private static readonly List<string> HeaderCensors = new List<string>
        {
            "Authorization",
            "User-Agent",
            "X-Client-User-Agent"
        };

        private static readonly List<string> QueryCensors = new List<string>
        {
            "card[number]",
            "card[cvc]"
        };

        private static readonly List<string> BodyCensors = new List<string>
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


        public enum ApiKey
        {
            Test,
            Production
        }

        internal static BaseClient GetClient(string apiKey, ClientVersion clientVersion, HttpClient vcrClient = null)
        {
            switch (clientVersion)
            {
                case ClientVersion.V2:
                    return new V2Client(apiKey, vcrClient);
                case ClientVersion.Beta:
                    return new BetaClient(apiKey, vcrClient);
                default:
                    throw new Exception("Invalid client version");
            }
        }

        private static string GetApiKey(ApiKey apiKey)
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
                default:
                    throw new Exception("Invalid ApiKey type");
            }

            return Environment.GetEnvironmentVariable(keyName) ?? ApiKeyFailedToPull; // if can't pull from environment, will use a fake key. Won't matter on replay.
        }

        private static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "") => Path.GetDirectoryName(sourceFilePath);

        internal class VCR
        {
            private readonly string _apiKey;

            private readonly string _testCassettesFolder;
            private readonly EasyVCR.VCR _vcr;

            internal bool IsRecording()
            {
                return _vcr.Mode == Mode.Record;
            }

            internal VCR(string testCassettesFolder = null, ApiKey apiKey = ApiKey.Test)
            {
                Censors censors = new Censors("<REDACTED>");
                censors.HideHeaders(HeaderCensors);
                censors.HideQueryParameters(QueryCensors);
                censors.HideBodyParameters(BodyCensors);

                AdvancedSettings advancedSettings = new AdvancedSettings
                {
                    MatchRules = MatchRules.DefaultStrict,
                    Censors = censors,
                    SimulateDelay = false,
                    ManualDelay = 0,
                };
                _vcr = new EasyVCR.VCR(advancedSettings);

                _apiKey = GetApiKey(apiKey);

                _testCassettesFolder = Path.Combine(GetSourceFileDirectory(), CassettesFolder); // create "cassettes" folder in same directory as test files

                string netVersionFolder = null;
#if NET6_0
                    netVersionFolder = "net60";
#elif NET5_0
                    netVersionFolder = "net50";
#elif NETCOREAPP3_1
                    netVersionFolder = "netcore3.1";
#elif NET462
                netVersionFolder = "net462";
#endif

                if (netVersionFolder != null)
                {
                    _testCassettesFolder = Path.Combine(_testCassettesFolder, netVersionFolder); // create .NET version-specific folder in "cassettes" folder
                }

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

            internal BaseClient SetUpTest(string cassetteName, ClientVersion clientVersion, string overrideApiKey = null)
            {
                // override api key if needed
                string apiKey = overrideApiKey ?? _apiKey;

                // set up cassette
                Cassette cassette = new Cassette(_testCassettesFolder, cassetteName, new CassetteOrder.Alphabetical());

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
                return GetClient(apiKey, clientVersion, _vcr.Client);
            }
        }
    }
}
