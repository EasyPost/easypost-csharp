using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost.Tests._Utilities
{
    /// <summary>
    ///     Base class for all unit tests.
    ///     Sets up all available client versions for VCR and non-VCR requests.
    /// </summary>
    public class UnitTest : IDisposable
    {
        private readonly TestUtils.VCR? _vcr;

        protected Client Client;

        private string? _cleanupId;

        /// <summary>
        ///     An asynchronous function executed after every test.
        ///     Function should have a string (ID) passed in, and should return a boolean.
        /// </summary>
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        protected Func<string, Task<bool>>? CleanupFunction { get; set; }

#pragma warning disable CS8618
        // Warning is disabled because the IDE will warn the Client is not initialized and should be nullable.
        // Client is set later in the process, and will not be null when used by the unit tests execution.
        // To do it the way the IDE wants would require a lot of null checks in the unit tests.
        // This is not worth the effort, as the IDE warning is not a real issue.
        protected UnitTest(string groupName, TestUtils.ApiKey apiKey = TestUtils.ApiKey.Test) => _vcr = new TestUtils.VCR(groupName, apiKey);
#pragma warning restore CS8618

        /// <summary>
        ///     Called automatically by xUnit after each unit test is run.
        ///     Executes the CleanupFunction (passes in the _cleanupId) if set.
        /// </summary>
        /// <exception cref="Exception">Could not execute the declared clean-up function.</exception>
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
            catch
            {
#pragma warning disable CA2201
                throw new Exception("Could not execute clean-up function.");
#pragma warning restore CA2201
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Set the ID that will be passed into the CleanupFunction after each unit test
        /// </summary>
        /// <param name="id"></param>
        protected void CleanUpAfterTest(string id) => _cleanupId = id;

        protected bool IsRecording() => _vcr != null && _vcr.IsRecording();

        /// <summary>
        ///     Skip running cleanup after the unit test
        ///     Helpful to avoid double deletion confusion for VCR
        /// </summary>
        protected void SkipCleanUpAfterTest() => _cleanupId = null;

        /// <summary>
        ///     Set up all clients to make live calls.
        /// </summary>
        /// <param name="apiKey"></param>
        protected void UseLive(string apiKey) => Client = TestUtils.GetClient(apiKey);

        /// <summary>
        ///     Set up all clients to use the VCR.
        /// </summary>
        /// <param name="cassetteName"></param>
        /// <param name="overrideApiKey"></param>
        // ReSharper disable once InconsistentNaming
        protected void UseVCR(string cassetteName, string? overrideApiKey = null) => Client = _vcr?.SetUpTest(cassetteName, overrideApiKey)!;

        protected virtual IEnumerable<TestUtils.MockRequest> MockRequests =>  new List<TestUtils.MockRequest>();

        /// <summary>
        ///     Set up all clients to make mock requests.
        /// </summary>
        /// <param name="mockRequestsOverride">List of mock requests to use instead of the global list.</param>
        protected void UseMockClient(IEnumerable<TestUtils.MockRequest>? mockRequestsOverride = null)
        {
            // set up the mock client
            Client = new TestUtils.MockClient(new Client(TestUtils.GetApiKey(TestUtils.ApiKey.Mock))); // API key doesn't matter for mock client, since no real requests are made);

            // add the mock requests to the mock client
            ((TestUtils.MockClient)Client).AddMockRequests(mockRequestsOverride ?? MockRequests);
        }
    }
}
