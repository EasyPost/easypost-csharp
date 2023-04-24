using System.Net.Http;

namespace EasyPost.Http
{
    /// <summary>
    ///     Enum representing the available HTTP methods.
    /// </summary>
    public class Method
    {
        internal HttpMethod HttpMethod { get; }

        internal RestSharp.Method RestSharpMethod { get; }

        /// <summary>
        ///     HTTP GET method.
        /// </summary>
        public static readonly Method Get = new Method(HttpMethod.Get, RestSharp.Method.Get);
        /// <summary>
        ///     HTTP POST method.
        /// </summary>
        public static readonly Method Post = new Method(HttpMethod.Post, RestSharp.Method.Post);
        /// <summary>
        ///     HTTP PUT method.
        /// </summary>
        public static readonly Method Put = new Method(HttpMethod.Put, RestSharp.Method.Put);
        /// <summary>
        ///     HTTP DELETE method.
        /// </summary>
        public static readonly Method Delete = new Method(HttpMethod.Delete, RestSharp.Method.Delete);
        /// <summary>
        ///     HTTP PATCH method.
        /// </summary>
        public static readonly Method Patch = new Method(new HttpMethod("PATCH"), RestSharp.Method.Patch);

        private Method(HttpMethod httpMethod, RestSharp.Method restSharpMethod)
        {
            HttpMethod = httpMethod;
            RestSharpMethod = restSharpMethod;
        }
    }
}
