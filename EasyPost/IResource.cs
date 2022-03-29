using System.Collections.Generic;

namespace EasyPost
{
    public interface IResource
    {
        Dictionary<string, object?> AsDictionary();

        string? AsJson();

        void Merge(object source);
    }
}
