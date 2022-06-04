using System;
using System.Threading.Tasks;
using EasyPost.Clients;
using Xunit;

namespace EasyPost.Tests
{
    /// <summary>
    ///     Base class for all unit tests.
    ///     Sets up all available client versions for VCR and non-VCR requests.
    /// </summary>
    public class UnitTest : IDisposable
    {
        protected BetaClient BetaClient;

        protected V2Client V2Client;

        private readonly TestUtils.VCR _vcr;

        private string _cleanupId = null;

        protected UnitTest(string groupName, TestUtils.ApiKey apiKey = TestUtils.ApiKey.Test)
        {
            _vcr = new TestUtils.VCR(groupName, apiKey);
        }

        /// <summary>
        ///     An asynchronous function executed after every test.
        ///     Function should have a string (ID) passed in, and should return a boolean.
        /// </summary>
        protected Func<string, Task<bool>> CleanupFunction { get; set; }

        /// <summary>
        ///     Set the ID that will be passed into the CleanupFunction after each unit test
        /// </summary>
        /// <param name="id"></param>
        protected void CleanUpAfterTest(string id)
        {
            _cleanupId = id;
        }

        /// <summary>
        ///     Skip running cleanup after the unit test
        ///     Helpful to avoid double deletion confusion for VCR
        /// </summary>
        protected void SkipCleanUpAfterTest()
        {
            _cleanupId = null;
        }

        /// <summary>
        ///     Set up all clients to make live calls.
        /// </summary>
        /// <param name="apiKey"></param>
        protected void UseLive(string apiKey)
        {
            V2Client = (V2Client)TestUtils.GetClient(apiKey, ClientVersion.V2);
            BetaClient = (BetaClient)TestUtils.GetClient(apiKey, ClientVersion.Beta);
        }

        /// <summary>
        ///     Set up all clients to use the VCR.
        /// </summary>
        /// <param name="cassetteName"></param>
        /// <param name="overrideApiKey"></param>
        protected void UseVCR(string cassetteName, string overrideApiKey = null)
        {
            V2Client = (V2Client)_vcr.SetUpTest(cassetteName, ClientVersion.V2, overrideApiKey);
            BetaClient = (BetaClient)_vcr.SetUpTest(cassetteName, ClientVersion.Beta, overrideApiKey);
        }

        protected bool IsRecording()
        {
            return _vcr != null && _vcr.IsRecording();
        }

        public void Dispose()
        {
            if (CleanupFunction == null)
            {
                return;
            }

            if (_cleanupId == null)
            {
                return;
            }

            try
            {
                CleanupFunction.Invoke(_cleanupId).GetAwaiter().GetResult();
                _cleanupId = null;
            }
            catch (Exception exception)
            {
                throw new Exception("Could not execute clean-up function.");
            }
        }
    }
}
