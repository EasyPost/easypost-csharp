using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests
{
    public class ClientTests : UnitTest
    {
        public ClientTests() : base("client")
        {
        }

        #region Tests

        [Fact]
        [Testing.Function]
        public void TestClient()
        {
            const string apiBase = "https://www.example.com";
            TimeSpan timeout = TimeSpan.FromMilliseconds(5000);
            HttpClient httpClient = new();

            Client client = new(new ClientConfiguration(FakeApikey)
            {
                ApiBase = apiBase,
                Timeout = timeout,
                CustomHttpClient = httpClient,
            });

            Assert.Equal(FakeApikey, client.ApiKeyInUse);
            Assert.Equal(apiBase, client.ApiBaseInUse);
            Assert.Equal(timeout, client.Timeout);
            Assert.Equal(httpClient, client.CustomHttpClient);
        }

        [Fact]
        [Testing.Logic]
        public void TestThreadSafety()
        {
            const string key1 = "key1";
            const string key2 = "key2";
            const string key3 = "key3";

            Client client1 = new(new ClientConfiguration(key1));
            Client client2 = new(new ClientConfiguration(key2));
            Client client3 = new(new ClientConfiguration(key3));

            Thread thread1 = new(() => Thread(client1, key1));
            Thread thread2 = new(() => Thread(client2, key2));
            Thread thread3 = new(() => Thread(client3, key3));

            // Start all threads, purposely out of order
            thread2.Start();
            thread3.Start();
            thread1.Start();
            return;

            static void Thread(EasyPostClient client, string keyToMatch) => Assert.Equal(keyToMatch, client.ApiKeyInUse);
        }

        #endregion

        private const string FakeApikey = "fake_api_key";

        [Fact]
        public void TestBaseUrlOverride()
        {
            Client normalClient = new(new ClientConfiguration(FakeApikey));
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                ApiBase = "https://www.example.com",
            });

            Assert.Equal("https://api.easypost.com", normalClient.ApiBaseInUse);
            Assert.Equal("https://www.example.com", overrideClient.ApiBaseInUse);
        }

        [Fact]
        public void TestTimeoutOverride()
        {
            Client normalClient = new(new ClientConfiguration(FakeApikey));
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                Timeout = TimeSpan.FromMilliseconds(1),
            });

            Assert.Equal(TimeSpan.FromSeconds(60), normalClient.Timeout);
            Assert.Equal(TimeSpan.FromMilliseconds(1), overrideClient.Timeout);
        }

        [Fact]
        public void TestHttpClientOverride()
        {
            Client normalClient = new(new ClientConfiguration(FakeApikey));

            HttpClient httpClient = new();
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                CustomHttpClient = httpClient,
            });

            Assert.NotEqual(httpClient, normalClient.CustomHttpClient);
            Assert.Equal(httpClient, overrideClient.CustomHttpClient);
        }

        [Fact]
        public async void TestHttpClientCustomProxy()
        {
            const string proxyAddress = "localhost:8888";

            // Define a custom proxy in a custom HttpClientHandler in a custom HttpClient
            HttpClientHandler handler = new()
            {
                UseProxy = true,
                Proxy = new WebProxy($"http://{proxyAddress}"),
            };
            HttpClient httpClient = new(handler: handler);

            Client client = new(new ClientConfiguration(FakeApikey)
            {
                CustomHttpClient = httpClient,
            });

            Assert.Equal(httpClient, client.CustomHttpClient);

            // Assert that the proxy is set in the HttpClient by attempting to make a request (should fail due to invalid proxy address)
            try
            {
                await client.Address.Create(new Parameters.Address.Create());
                Assert.Fail("Expected HttpRequestException");
            }
            catch (HttpRequestException e)
            {
                Assert.Equal($"Connection refused ({proxyAddress})", e.Message);
            }
        }

        [Fact]
        public async Task TestRequestHooks()
        {
            int preRequestCallbackCallCount = 0;
            int postRequestCallbackCallCount = 0;
            var requestGuid = Guid.Empty;

            Hooks hooks = new()
            {
                OnRequestExecuting = (_, args) =>
                {
                    // Modifying the HttpRequestMessage in this action does not impact the HttpRequestMessage being executed (passed by value, not reference)
                    preRequestCallbackCallCount++;
                    Assert.True(args.RequestTimestamp > 0);
                    requestGuid = args.Id;
                },
                OnRequestResponseReceived = (_, args) =>
                {
                    postRequestCallbackCallCount++;
                    Assert.True(args.RequestTimestamp > 0);
                    Assert.True(args.ResponseTimestamp > 0);
                    Assert.True(args.ResponseTimestamp >= args.RequestTimestamp);
                    Assert.Equal(requestGuid, args.Id);
                },
            };

            UseVCRWithCustomClient("request_hooks", (_, httpClient) =>
                new Client(new ClientConfiguration(FakeApikey)
                {
                    CustomHttpClient = httpClient,
                    Hooks = hooks,
                })
            );

            // Make a request, doesn't matter what it is (catch the exception due to invalid API key)
            await Assert.ThrowsAsync<UnauthorizedError>(async () => await Client.Address.Create(new Parameters.Address.Create()));

            // Assert that the pre-request callback was called
            Assert.Equal(1, preRequestCallbackCallCount);
            // Assert that the post-request callback was called
            Assert.Equal(1, postRequestCallbackCallCount);
        }

        [Fact]
        public async Task TestMultipleRequestHookCallbacks()
        {
            bool preRequestCallback1Called = false;
            bool preRequestCallback2Called = false;

            bool postRequestCallback1Called = false;
            bool postRequestCallback2Called = false;

            Hooks hooks = new();
            hooks.OnRequestExecuting += (_, _) => preRequestCallback1Called = true;
            hooks.OnRequestExecuting += (_, _) => preRequestCallback2Called = true;
            hooks.OnRequestResponseReceived += (_, _) => postRequestCallback1Called = true;
            hooks.OnRequestResponseReceived += (_, _) => postRequestCallback2Called = true;

            UseVCRWithCustomClient("multiple_request_hooks", (_, httpClient) =>
                new Client(new ClientConfiguration(FakeApikey)
                {
                    CustomHttpClient = httpClient,
                    Hooks = hooks,
                })
            );

            // Make a request, doesn't matter what it is (catch the exception due to invalid API key)
            await Assert.ThrowsAsync<UnauthorizedError>(async () => await Client.Address.Create(new Parameters.Address.Create()));

            // Assert that the pre-request callbacks were called
            Assert.True(preRequestCallback1Called);
            Assert.True(preRequestCallback2Called);
            // Assert that the post-request callbacks were called
            Assert.True(postRequestCallback1Called);
            Assert.True(postRequestCallback2Called);
        }

        [Fact]
        public async Task TestRequestHooksUnsubscribing()
        {
            int preRequestCallbackCallCount = 0;

            void PreRequestCallback(object? sender, OnRequestExecutingEventArgs args)
            {
                preRequestCallbackCallCount++;
            }

            Hooks hooks = new();

            UseVCRWithCustomClient("request_hooks_unsubscribing", (apiKey, httpClient) =>
                new Client(new ClientConfiguration(FakeApikey)
                {
                    CustomHttpClient = httpClient,
                    Hooks = hooks,
                })
            );

            // Subscribe to the pre-request callback
            Client.Hooks.OnRequestExecuting += PreRequestCallback;

            // Make a request, doesn't matter what it is (catch the exception due to invalid API key)
            await Assert.ThrowsAsync<UnauthorizedError>(async () => await Client.Address.Create(new Parameters.Address.Create()));

            // Assert that the pre-request callback was called
            Assert.Equal(1, preRequestCallbackCallCount);

            // Unsubscribe from the pre-request callback
            Client.Hooks.OnRequestExecuting -= PreRequestCallback;

            // Make a request, doesn't matter what it is (catch the exception due to invalid API key)
            await Assert.ThrowsAsync<UnauthorizedError>(async () => await Client.Address.Create(new Parameters.Address.Create()));

            // Assert that the pre-request callback was not called again
            Assert.Equal(1, preRequestCallbackCallCount);
        }

        [Fact]
        public async Task TestCancellationToken()
        {
            CancellationTokenSource cancelTokenSource = new();
            CancellationToken token = cancelTokenSource.Token;

            Hooks hooks = new()
            {
                OnRequestExecuting = (_, _) =>
                {
                    // Use the cancellation token to cancel the request
                    cancelTokenSource.Cancel();
                },
            };

            UseVCRWithCustomClient("cancellation_token", (_, httpClient) =>
                new Client(new ClientConfiguration(FakeApikey)
                {
                    CustomHttpClient = httpClient,
                    Hooks = hooks,
                })
            );

            // Make a request, doesn't matter what it is
            // Should throw a TimeoutError because the request was cancelled
            // If it throws a UnauthorizedError, then the cancellation token was not used (request went through and failed due to invalid API key)
            // this will not record a cassette because the request should be cancelled before it is sent
            await Assert.ThrowsAsync<TimeoutError>(async () => await Client.Address.Create(new Parameters.Address.Create(), token));
        }
    }
}
