using System.Collections.Generic;
using System.Net.Http;

namespace Scotch
{
    public enum ScotchMode
    {
        None = 0,
        Recording = 1,
        Replaying = 2
    }

    public static class HttpClients
    {
        private static List<string> _credentialHeadersToHide
        {
            get
            {
                return new List<string>
                {
                    {
                        "Authorization"
                    }
                };
            }
        }

        public static HttpClient NewHttpClientWithHandler(HttpMessageHandler innerHandler, string cassettePath, ScotchMode mode, bool hideCredentials = false)
        {
            switch (mode)
            {
                case ScotchMode.Recording:
                    return new HttpClient(new RecordingHandler(innerHandler, cassettePath, hideCredentials ? _credentialHeadersToHide : null));
                case ScotchMode.Replaying:
                    return new HttpClient(new ReplayingHandler(innerHandler, cassettePath));
                case ScotchMode.None:
                default:
                    return new HttpClient(innerHandler);
            }
        }

        public static HttpClient NewHttpClient(string cassettePath, ScotchMode mode, bool hideCredentials = false)
        {
            return NewHttpClientWithHandler(new HttpClientHandler(), cassettePath, mode, hideCredentials);
        }
    }
}
