using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost
{
    public class InvalidRequest : Exception {
        public InvalidRequest() {}
        public InvalidRequest(string message) : base(message) {}
    }

    public class ResourceNotFound : Exception {}
    public class ResourceAlreadyCreated : Exception {}
}
