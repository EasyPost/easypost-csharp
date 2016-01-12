using System;

namespace EasyPost {
    public class HttpException : Exception {
        public int StatusCode;
        public string Code;

        public HttpException(int statusCode, string code, string message)
            : base(message) {
            StatusCode = statusCode;
            Code = code;
        }
    }

    public class ResourceAlreadyCreated : Exception { }
}
