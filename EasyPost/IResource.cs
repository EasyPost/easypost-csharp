// <copyright file="IResource.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace EasyPost
{
    public interface IResource
    {
        void Merge(object source);

        Dictionary<string, object> AsDictionary();
    }
}
