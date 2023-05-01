using System;
using System.Collections.Generic;

namespace EasyPost.BetaFeatures.Parameters;

/// <summary>
///     Base class for parameter sets used in `All` methods.
/// </summary>
public abstract class BaseAllParameters : BaseParameters
{
    public static TParameters FromDictionary<TParameters>(Dictionary<string, object>? dictionary)
        where TParameters : BaseAllParameters
    {
        TParameters parameters = (TParameters)Activator.CreateInstance(typeof(TParameters))!;
        return dictionary == null ? parameters : parameters.FromDictionaryProtected<TParameters>(dictionary);
    }

    protected abstract TParameters FromDictionaryProtected<TParameters>(Dictionary<string, object> dictionary)
        where TParameters : BaseAllParameters;
}
