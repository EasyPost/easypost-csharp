using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.Beta
{
    public static class EndShippers
    {
        public class Update : EasyPostParameters
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

        public class Create : EasyPostParameters
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
