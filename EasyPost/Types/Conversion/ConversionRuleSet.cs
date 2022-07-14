using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyPost.Clients;

namespace EasyPost.Types.Conversion
{
    public abstract class BiDirectionalConversionRuleSet : IEnumerable
    {
        private readonly SortedDictionary<int, BiDirectionalConversionRules> _rules = new SortedDictionary<int, BiDirectionalConversionRules>();

        internal BiDirectionalConversionRuleSet()
        {
        }

        internal void Add(ApiVersion apiVersion, BiDirectionalConversionRules rules)
        {
            ApiVersionDetails toApiVersionDetails = ApiVersionDetails.GetApiVersionDetails(apiVersion);
            _rules.Add(toApiVersionDetails.Number, rules);
        }

        internal IEnumerable<Func<object, object>?> GetBackwardConversionFunctions(ApiVersionDetails toApiVersionDetails)
        {
            return _rules.Reverse().Where(rule => rule.Key > toApiVersionDetails.Number).Select(rule => rule.Value.BackwardRule).ToList();
        }

        internal IEnumerable<Func<object, object>?> GetForwardConversionFunctions(ApiVersionDetails fromApiVersionDetails)
        {
            return _rules.Where(rule => rule.Key >= fromApiVersionDetails.Number).Select(rule => rule.Value.ForwardRule).ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rules.GetEnumerator();
        }
    }

    internal abstract class BiDirectionalConversionRules
    {
        internal Func<object, object>? BackwardRule { get; set; }
        internal Func<object, object>? ForwardRule { get; set; }
    }
}
