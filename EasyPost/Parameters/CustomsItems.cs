using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class CustomsItems
    {
        public sealed class Create : RequestParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
