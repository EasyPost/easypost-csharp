using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters.Beta
{
    public static class Partners
    {
        public class CreateReferral : ApiParameters
        {
            public CreateReferral(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
