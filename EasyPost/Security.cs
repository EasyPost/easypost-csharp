using System.Net;

namespace EasyPost {
    internal class Security {
        public static SecurityProtocolType GetProtocol() {
#if NET45
            return SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#else
            return SecurityProtocolType.Tls;
#endif
        }
    }
}
