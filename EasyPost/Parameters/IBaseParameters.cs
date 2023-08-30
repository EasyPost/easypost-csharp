using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters
{
    public interface IBaseParameters
    {
        public Dictionary<string, object> ToDictionary();

        public Dictionary<string, object> ToSubDictionary(Type parentParameterObjectType);
    }
}
