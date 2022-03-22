using System;

namespace EasyPost
{
    /// <summary>
    ///     Provides the ability to manage delegated construction of client connections for requests.
    /// </summary>
    public static class ClientManager
    {
        private static Func<Client>? getCurrent;

        /// <summary>
        ///     Set/Reset the client with a new API key.
        /// </summary>
        /// <param name="apiKey">API key for the client to use.</param>
        public static void SetCurrent(string apiKey) => SetCurrent(() => new Client(new ClientConfiguration(apiKey)));

        /// <summary>
        ///     Configure the function used to retrieve the current client.
        /// </summary>
        /// <param name="getClient">A function used to retrieve the current client.</param>
        public static void SetCurrent(Func<Client> getClient) => getCurrent = getClient;

        /// <summary>
        ///     Remove the function used to retrieve the current client.
        /// </summary>
        public static void Unconfigure() => getCurrent = null;

        /// <summary>
        ///     Build an EasyPost.Client.
        /// </summary>
        /// <returns>An EasyPost.Client instance.</returns>
        /// <exception cref="ClientNotConfigured">No function set to retrieve the current client.</exception>
        internal static Client Build()
        {
            if (getCurrent == null)
            {
                throw new ClientNotConfigured();
            }

            return getCurrent();
        }
    }
}
