using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Refunds
    {
        public sealed class Create : RequestParameters
        {
            // TODO: What are the parameters?

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }
    }
}
