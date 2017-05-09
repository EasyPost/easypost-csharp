using System;

namespace EasyPost {
    public class HttpException : Exception {
        public int StatusCode;
        public string Code;
        public Error Error;

        public HttpException(int statusCode, string code, string message)
            : base(message) {
            StatusCode = statusCode;
            Code = code;
            Error = Error.Load<Error>(message);
        }
    }

    public class ResourceAlreadyCreated : Exception { }

    public class ClientNotConfigured : Exception { }
}
