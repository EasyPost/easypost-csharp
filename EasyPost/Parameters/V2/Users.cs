using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Users
    {
        public class Update : EasyPostParameters
        {
            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class UpdateBrand : EasyPostParameters
        {
            public UpdateBrand(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }

        public class Create : EasyPostParameters
        {
            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
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
