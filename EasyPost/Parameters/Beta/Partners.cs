using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters.Beta
{
    public static class Partners
    {
        public sealed class CreateReferral : RequestParameters
        {
            public CreateReferral(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
