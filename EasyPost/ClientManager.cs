using System;

namespace EasyPost {
    /// <summary>
    /// Provides ability to 
    /// </summary>
    public static class ClientManager {
        internal static Client Current;

        public static void SetDefault() {
            SetCurrent(() => new Client());
        }

        public static void SetCurrent(string ApiKey) {
            SetCurrent(() => new Client(new ClientConfiguration(ApiKey)));
        }

        public static void SetCurrent(Client client) {
            SetCurrent(() => client);
        }

        public static void SetCurrent(Func<Client> getClient) {
            Current = getClient();
        }
    }
}