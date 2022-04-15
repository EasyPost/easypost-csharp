using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using EasyPost.Scotch;

namespace EasyPost.Tests
{
    public enum VCRApiKey
    {
        Test,
        Production
    }

    public static class VCR
    {
        public static string GlobalApiKey { get; set; }

        private static string CassetteFolder { get; set; }

        private static bool _HideCredentials { get; set; }

        private static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "")
        {
            return Path.GetDirectoryName(sourceFilePath);
        }

        private static string GetCassettePath(string cassetteName)
        {
            string cassetteFolder = Path.Combine(GetSourceFileDirectory(), "cassettes");
            Directory.CreateDirectory(cassetteFolder);
            if (CassetteFolder != null)
            {
                cassetteFolder = Path.Combine(cassetteFolder, CassetteFolder);
                Directory.CreateDirectory(cassetteFolder);
            }

            return Path.Combine(cassetteFolder, $"{cassetteName}.json");
        }

        private static string GetApiKey(VCRApiKey apiKey)
        {
            string keyName = "";
            switch (apiKey)
            {
                case VCRApiKey.Test:
                    keyName = "EASYPOST_TEST_API_KEY";
                    break;
                case VCRApiKey.Production:
                    keyName = "EASYPOST_PROD_API_KEY";
                    break;
            }

            return Environment.GetEnvironmentVariable(keyName) ?? "couldnotpullapikey"; // if can't pull from environment, will use a fake key. Won't matter on replay.
        }

        private static HttpClient GetClient(string cassetteName, ScotchMode mode, bool? hideCredentials = null)
        {
            return HttpClients.NewHttpClient(GetCassettePath(cassetteName), mode, hideCredentials.GetValueOrDefault(_HideCredentials));
        }

        public static void SetUp(string apiKey = null, string cassetteFolder = null, bool hideCredentials = false)
        {
            CassetteFolder = cassetteFolder;
            GlobalApiKey = apiKey;
            _HideCredentials = hideCredentials;
        }

        public static void SetUp(VCRApiKey apiKey, string cassetteFolder = null, bool hideCredentials = false)
        {
            SetUp(GetApiKey(apiKey), cassetteFolder, hideCredentials);
        }

        public static void Record(string cassetteName, string apiKey = null, bool? hideCredentials = null)
        {
            ClientManager.SetCurrent(GetRecordingClientFunction(apiKey ?? GlobalApiKey, cassetteName, hideCredentials.GetValueOrDefault(_HideCredentials)));
        }

        public static void Replay(string cassetteName, string apiKey = null, bool recordIfCassetteMissing = true, bool? hideCredentials = null)
        {
            if (!File.Exists(GetCassettePath(cassetteName)))
            {
                if (!recordIfCassetteMissing)
                {
                    throw new FileNotFoundException($"Cassette {cassetteName} not found. Please record it first.");
                }

                Record(cassetteName, apiKey, hideCredentials);
                return;
            }

            ClientManager.SetCurrent(GetReplayingClientFunction(apiKey ?? GlobalApiKey, cassetteName, hideCredentials.GetValueOrDefault(_HideCredentials)));
        }

        public static void Bypass(string cassetteName = null, string apiKey = null, bool? hideCredentials = null)
        {
            ClientManager.SetCurrent(GetNonVCRClientFunction(apiKey ?? GlobalApiKey));
        }

        public static Func<Client> GetRecordingClientFunction(string apiKey, string cassetteName, bool? hideCredentials = null)
        {
            return delegate { return new Client(new ClientConfiguration(apiKey), GetClient(cassetteName, ScotchMode.Recording, hideCredentials.GetValueOrDefault(_HideCredentials))); };
        }

        public static Func<Client> GetReplayingClientFunction(string apiKey, string cassetteName, bool? hideCredentials = null)
        {
            return delegate { return new Client(new ClientConfiguration(apiKey), GetClient(cassetteName, ScotchMode.Replaying, hideCredentials.GetValueOrDefault(_HideCredentials))); };
        }

        public static Func<Client> GetNonVCRClientFunction(string apiKey)
        {
            return delegate { return new Client(new ClientConfiguration(apiKey)); };
        }
    }
}
