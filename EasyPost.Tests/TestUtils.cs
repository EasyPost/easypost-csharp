using System;
using System.IO;
using System.Runtime.CompilerServices;
using EasyPost.EasyVCR;

namespace EasyPost.Tests
{
    public class TestUtils
    {
        private const string ApiKeyFailedToPull = "couldnotpullapikey";

        private const string CassettesFolder = "cassettes";

        private static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "")
        {
            return Path.GetDirectoryName(sourceFilePath);
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

        public enum ApiKey
        {
            Test,
            Production
        }

        public class VCR
        {
            private readonly EasyVCR.VCR _vcr;

            private readonly string _apiKey;

            private readonly string _testCassettesFolder;

            public VCR(string testCassettesFolder = null, ApiKey apiKey = ApiKey.Test)
            {
                AdvancedSettings advancedSettings = new AdvancedSettings
                {
                    MatchRules = MatchRules.Default,
                    Censors = Censors.DefaultSensitive,
                    SimulateDelay = false,
                    ManualDelay = 0,
                };
                _vcr = new EasyVCR.VCR(advancedSettings);
                _vcr.RecordIfNeeded(); // always in auto mode

                _apiKey = GetApiKey(apiKey);

                _testCassettesFolder = Path.Combine(GetSourceFileDirectory(), CassettesFolder);

                if (testCassettesFolder != null)
                {
                    _testCassettesFolder = Path.Combine(_testCassettesFolder, testCassettesFolder);
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

                // add cassette to vcr
                _vcr.Insert(cassette);

                // set up EasyPost client
                ClientManager.SetCurrent(() => new Client(new ClientConfiguration(apiKey), _vcr.Client));
            }
        }
    }
}
