using System;

namespace EasyPost {
    /// <summary>
    /// Provides the ability to manage delegated construction of client connections for requests.
    /// </summary>
    public static class ClientManager {
        private static Func<Client> Current
        {
            get { return IsThreadStatic ? ThreadCurrent : AllCurrent; }
            set {
                if (IsThreadStatic)
                    ThreadCurrent = value;
                else
                    AllCurrent = value;
            }
        }
        
        /// <summary>
        /// Thread static holder for current client. If IsThreadStatic is set to true, this
        /// will be used for the active thread.
        /// </summary>
        [ThreadStatic] private static Func<Client> ThreadCurrent;

        /// <summary>
        /// Default client holder. Even if IsThreadStatic set to true after setting current client,
        /// this will be filled. So anytime you can return to that just by setting IsThreadStatic to false.
        /// </summary>
        private static Func<Client> AllCurrent;

        /// <summary>
        /// Determines if current client is thread static or all static.
        /// </summary>
        private static bool IsThreadStatic = false;

        internal static Client Build() {
            if (Current == null)
                throw new ClientNotConfigured();
            return Current();
        }

        public static void SetCurrent(string apiKey) {
            SetCurrent(() => new Client(new ClientConfiguration(apiKey)));
        }

        public static void SetCurrent(Func<Client> getClient) {
            Current = getClient;
        }

        /// <summary>
        /// Sets or unsets the active static client only for thread or for all threads.
        /// </summary>
        /// <param name="setThreadStatic">[bool] Set true for ThreadStatic</param>
        public static void SetThreadStatic(bool setThreadStatic = true)
        {
            IsThreadStatic = setThreadStatic;
        }

        public static void Unconfigure() {
            Current = null;
        }
    }
}