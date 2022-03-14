using System;
using System.Diagnostics;

namespace EasyPost.Tests.NetFramework
{
    public enum TestSuiteApiKey
    {
        Test,
        Production
    }

    public static class TestSuite
    {
        private static string GetApiKey(TestSuiteApiKey apiKey)
        {
            string keyName = "";
            switch (apiKey)
            {
                case TestSuiteApiKey.Test:
                    keyName = "EASYPOST_TEST_API_KEY";
                    break;
                case TestSuiteApiKey.Production:
                    keyName = "EASYPOST_PROD_API_KEY";
                    break;
            }

            return Environment.GetEnvironmentVariable(keyName) ?? "couldnotpullapikey"; // if can't pull from environment, will use a fake key. Won't matter on replay.
        }

        public static void SetUp(string apiKey)
        {
            ClientManager.SetCurrent(GetClientFunction(apiKey));

            // Allow TLS 1.2 for the tests to work
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
        }

        public static void SetUp(TestSuiteApiKey apiKey)
        {
            SetUp(GetApiKey(apiKey));
        }

        public static Func<Client> GetClientFunction(string apiKey)
        {
            return delegate { return new Client(new ClientConfiguration(apiKey)); };
        }
    }
}
