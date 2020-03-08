using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EasyPost {
    [Serializable]
    public class HttpException : Exception {
        public int StatusCode;
        public string Code;
        public List<Error> Errors;

        public HttpException(int statusCode, string code, string message, List<Error> errors) : base(message) {
            StatusCode = statusCode;
            Code = code;
            Errors = errors;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            info.AddValue("StatusCode", StatusCode);
            info.AddValue("Code", Code);
            info.AddValue("Errors", Errors);
        }
    }

    [Serializable]
    public class ResourceAlreadyCreated : Exception { }

    [Serializable]
    public class ClientNotConfigured : Exception { }
}
