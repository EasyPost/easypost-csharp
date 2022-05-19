using System;
using System.IO;
using System.Runtime.CompilerServices;
using EasyVCR;

namespace EasyPost.Tests
{
    public class TestUtils
    {
        private const string ApiKeyFailedToPull = "couldnotpullapikey";

        private const string CassettesFolder = "cassettes";

        public enum ApiKey
        {
            Test,
            Production
        }

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
                default:
                    throw new Exception("Invalid ApiKey type");
            }

            return Environment.GetEnvironmentVariable(keyName) ?? ApiKeyFailedToPull; // if can't pull from environment, will use a fake key. Won't matter on replay.
        }

        private static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "")
        {
            return Path.GetDirectoryName(sourceFilePath);
        }

        public class VCR
        {
            private readonly string _apiKey;

            private readonly string _testCassettesFolder;
            private readonly EasyVCR.VCR _vcr;

            public VCR(string testCassettesFolder = null, ApiKey apiKey = ApiKey.Test)
            {
                AdvancedSettings advancedSettings = new AdvancedSettings
                {
                    MatchRules = MatchRules.DefaultStrict,
                    Censors = Censors.DefaultSensitive,
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

            public void SetUpTest(string cassetteName, string overrideApiKey = null)
            {
                // override api key if needed
                string apiKey = overrideApiKey ?? _apiKey;

                // set up cassette
                Cassette cassette = new Cassette(_testCassettesFolder, cassetteName, new CassetteOrder.Alphabetical());

                string filePath = Path.Combine(_testCassettesFolder, cassetteName + ".json");

                // add cassette to vcr
                _vcr.Insert(cassette);

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

                // set up EasyPost client
                ClientManager.SetCurrent(() => new Client(new ClientConfiguration(apiKey), _vcr.Client));
            }
        }
    }
}
