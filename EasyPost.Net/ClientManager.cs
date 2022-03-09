using System;

namespace EasyPost
{
    /// <summary>
    ///     Provides the ability to manage delegated construction of client connections for requests.
    /// </summary>
    public static class ClientManager
    {
        private static bool Multithreaded { get; set; }

        public static bool RunMultiThreaded
        {
            get { return Multithreaded; }
            set
            {
                if (!value && Multithreaded) // if we're turning off multithreading, clear the cache
                {
                    ThreadManager.DeregisterAllThreads();
                }
                Multithreaded = value;
            }
        }

        private static Func<Client>? GetCurrent { get; set; }

        /// <summary>
        ///     Set/Reset the client with a new API key.
        /// </summary>
        /// <param name="apiKey">API key for the client to use.</param>
        public static void SetCurrent(string apiKey) => SetCurrent(() => new Client(new ClientConfiguration(apiKey)));

        /// <summary>
        ///     Configure the function used to retrieve the current client.
        /// </summary>
        /// <param name="getClient">A function used to retrieve the current client.</param>
        public static void SetCurrent(Func<Client> getClient) => GetCurrent = getClient;

        /// <summary>
        ///     Remove the function used to retrieve the current client.
        /// </summary>
        public static void Unconfigure() => GetCurrent = null;

        /// <summary>
        ///     Build an EasyPost.Client.
        /// </summary>
        /// <returns>An EasyPost.Client instance.</returns>
        /// <exception cref="ClientNotConfigured">No function set to retrieve the current client.</exception>
        internal static Client Build()
        {
            if (Multithreaded)
            {
                return new Client(new ClientConfiguration(ThreadManager.GetApiKeyForCurrentThread()));
            }

            if (GetCurrent == null)
            {
                throw new ClientNotConfigured();
            }

            return GetCurrent();
        }
    }
}
