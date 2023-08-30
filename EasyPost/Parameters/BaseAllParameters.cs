using System;
using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters;

/// <summary>
///     Base class for parameter sets used in `All` methods.
/// </summary>
public abstract class BaseAllParameters<TMatchInputType> : BaseParameters<TMatchInputType> where TMatchInputType : EphemeralEasyPostObject
{
    /// <summary>
    ///     Construct a new <see cref="BaseAllParameters{TMatchInputType}"/>-based instance from a <see cref="Dictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="dictionary">The dictionary to parse.</param>
    /// <returns>A BaseAllParameters-subtype object.</returns>
#pragma warning disable CA1000
    public static BaseAllParameters<TMatchInputType> FromDictionary(Dictionary<string, object>? dictionary)
#pragma warning restore CA1000
    {
        throw new NotImplementedException();
    }
}
