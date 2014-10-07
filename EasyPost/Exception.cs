using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class HttpException : Exception {
        public int StatusCode;

        public HttpException(int statusCode, string message)
            : base(message) {
            StatusCode = statusCode;
        }
    }

    public class ResourceAlreadyCreated : Exception { }
}
