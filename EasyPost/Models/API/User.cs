using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Models.API
{
    public class User : BaseUser, IUserParameter
    {
        internal User()
        {
        }
    }
}
