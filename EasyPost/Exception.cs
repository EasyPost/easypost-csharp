using System;
using System.Collections.Generic;

namespace EasyPost {
    public class HttpException : Exception {
        public int StatusCode;
        public string Code;
        public List<Error> Errors;

        public HttpException(int statusCode, string code, string message, List<Error> errors) : base(message) {
            StatusCode = statusCode;
            Code = code;
            Errors = errors;
        }
    }

    public class ResourceAlreadyCreated : Exception { }

    public class ClientNotConfigured : Exception { }
}
