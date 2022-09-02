using EasyPost.Utilities;

namespace EasyPost._base
{
    public class ApiVersion : ValueEnum
    {
        public static readonly ApiVersion Beta = new ApiVersion(2, "beta");
        public static readonly ApiVersion Current = new ApiVersion(1, "v2");

        private ApiVersion(int id, object value) : base(id, value)
        {
        }
    }
}
