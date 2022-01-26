using System.Collections.Generic;

namespace EasyPost {
    public interface IResource {
        void Merge(object source);
        Dictionary<string, object> AsDictionary();
    }
}
