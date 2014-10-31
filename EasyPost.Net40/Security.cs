using System.Net;

namespace EasyPost {
    internal class Security {
        public static SecurityProtocolType GetProtocol() {
            return SecurityProtocolType.Tls;
        }
    }
}