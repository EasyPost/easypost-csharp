using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters.Beta
{
    public static class EndShippers
    {
        public sealed class Update : ApiParameters
        {
            [Parameter(Necessity.Optional, "name")]
            public string? Name { internal get; set; }

            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }

        public sealed class Create : ApiParameters
        {
            [Parameter(Necessity.Required, "name")]
            public string? Name { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
