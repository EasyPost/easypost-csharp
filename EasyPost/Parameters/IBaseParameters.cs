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
    /// <summary>
    ///     Base interface for all EasyPost API parameters.
    /// </summary>
    public interface IBaseParameters
    {
        /// <summary>
        ///     Convert this object to a <see cref="Dictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary();

        /// <summary>
        ///     Convert this object to a sub-<see cref="Dictionary{TKey,TValue}"/> based on the parent parameter object type.
        /// </summary>
        /// <param name="parentParameterObjectType"></param>
        /// <returns></returns>
        public Dictionary<string, object> ToSubDictionary(Type parentParameterObjectType);
    }
}
