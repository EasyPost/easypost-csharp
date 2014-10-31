using System.Net;

namespace EasyPost {
    internal class Security {
        public static SecurityProtocolType GetProtocol() {
#if NET4
            return SecurityProtocolType.Tls;
#else
            return SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#endif
        }
    }
}
