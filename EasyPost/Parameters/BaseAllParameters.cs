using System;
using System.Collections.Generic;

namespace EasyPost.Parameters;

/// <summary>
///     Base class for parameter sets used in `All` methods.
/// </summary>
public abstract class BaseAllParameters : BaseParameters
{
    /// <summary>
    ///     Construct a new <see cref="BaseAllParameters"/>-based instance from a <see cref="Dictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="dictionary">The dictionary to parse.</param>
    /// <returns>A BaseAllParameters-subtype object.</returns>
    public static BaseAllParameters FromDictionary(Dictionary<string, object>? dictionary)
    {
        throw new NotImplementedException();
    }
}
