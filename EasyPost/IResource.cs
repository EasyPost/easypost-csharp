using System.Collections.Generic;

namespace EasyPost
{
    public interface IResource
    {
        Dictionary<string, object> AsDictionary();

        void Merge(object source);
    }
}
