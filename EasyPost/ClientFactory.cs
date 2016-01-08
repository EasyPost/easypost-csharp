using System;

namespace EasyPost
{
    /// <summary>
    /// Provides construction of REST client objects
    /// </summary>
    public interface IClientFactory {
        Client Build();
    }

    /// <summary>
    /// Provides well-known factory location to support loose management of dependencies as relates to the instantiation of REST clients
    /// </summary>
    public static class ClientFactory {
        private static Func<IClientFactory> current;
        
        static ClientFactory() {
            current = () => new DefaultClientFactory();
        }

        public static IClientFactory Current {
            get { return current(); } 
        }

        public static void SetCurrent(IClientFactory factory) {
            if (factory == null) throw new ArgumentNullException("factory");
            SetCurrent(() => factory);
        }

        public static void SetCurrent(Func<IClientFactory> getCurrentFactory) {
            if (getCurrentFactory == null) throw new ArgumentNullException("getCurrentFactory");
            current = getCurrentFactory;
        }
    }

    /// <summary>
    /// Default implementation of a REST client factory using the empty/default constructor
    /// </summary>
    public class DefaultClientFactory : IClientFactory {
        public Client Build() {
            return new Client();
        }
    }
}