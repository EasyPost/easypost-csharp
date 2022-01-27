// IResource.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;

namespace EasyPost
{
    public interface IResource
    {
        Dictionary<string, object> AsDictionary();

        void Merge(object source);
    }
}
