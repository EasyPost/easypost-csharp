using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Clients;

namespace EasyPost.Types.Conversion
{
    internal static class CompatibilityConversions
    {
        internal static ICompatibilityConverter CreateConverter(BiDirectionalConversionRuleSet conversionRuleSet, ApiVersionDetails olderApiVersionDetails)
        {
            if (olderApiVersionDetails.ApiVersionEnum == ApiVersion.Latest)
            {
                return new BlankCompatibilityConverter();
            }

            return new CompatibilityConverter(conversionRuleSet, olderApiVersionDetails);
        }
    }

    internal class BlankCompatibilityConverter : ICompatibilityConverter
    {
        public IEasyPostObject ConvertBackwards(object obj)
        {
            return (IEasyPostObject)obj;
        }

        public IEasyPostObject ConvertForwards(object obj)
        {
            return (IEasyPostObject)obj;
        }
    }

    internal class CompatibilityConverter : ICompatibilityConverter
    {
        private readonly IEnumerable<Func<object, object>?> _backwardConversionFuncs;
        private readonly IEnumerable<Func<object, object>?> _forwardConversionFuncs;

        public CompatibilityConverter(BiDirectionalConversionRuleSet conversionRuleSet, ApiVersionDetails olderApiVersionDetails)
        {
            _forwardConversionFuncs = conversionRuleSet.GetForwardConversionFunctions(olderApiVersionDetails);
            _backwardConversionFuncs = conversionRuleSet.GetBackwardConversionFunctions(olderApiVersionDetails);
        }

        public IEasyPostObject ConvertBackwards(object obj)
        {
            // Keep converting the object backwards until we get to the target API version (anything greater than)
            // e.g.
            // 5 (func) -> 4
            // 4 (func) -> 3
            // If we're currently on 5, and we want to get to a 4-compatible version
            // we execute the function attached to 5 to get a 4-compatible object.
            // we don't need to execute the function attached to 4.
            obj = _backwardConversionFuncs.Where(func => func != null).Aggregate(obj, (current, func) => func!(current));
            return (IEasyPostObject)obj;
        }

        public IEasyPostObject ConvertForwards(object obj)
        {
            obj = _forwardConversionFuncs.Where(func => func != null).Aggregate(obj, (current, func) => func!(current));
            return (IEasyPostObject)obj;
        }
    }

    internal interface ICompatibilityConverter
    {
        public IEasyPostObject ConvertBackwards(object obj);
        public IEasyPostObject ConvertForwards(object obj);
    }
}
