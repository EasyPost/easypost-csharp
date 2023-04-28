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
        return dictionary == null ? parameters : parameters._FromDictionary<TParameters>(dictionary);
    }

    protected abstract TParameters _FromDictionary<TParameters>(Dictionary<string, object> dictionary)
        where TParameters : BaseAllParameters;
}
