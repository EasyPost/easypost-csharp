using System;
using EasyPost._base;

namespace EasyPost.BetaFeatures.Parameters;

/// <summary>
///     Base class for parameter sets used in `Update` methods.
/// </summary>
public abstract class BaseUpdateParameters<TObject> : BaseParameters where TObject : EasyPostObject
{
#pragma warning disable CA1000
    public static BaseUpdateParameters<TObject> FromObject(TObject obj)
#pragma warning restore CA1000
    {
        throw new NotImplementedException();
    }
}
