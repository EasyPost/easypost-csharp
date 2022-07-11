using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.Beta
{
    public static class Partners
    {
        public class CreateReferral : EasyPostParameters
        {
            public CreateReferral(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }
    }
}
