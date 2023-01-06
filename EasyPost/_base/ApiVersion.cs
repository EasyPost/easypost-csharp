using EasyPost.Utilities;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public class ApiVersion : ValueEnum
    {
        public static readonly ApiVersion Beta = new(2, "beta");
        public static readonly ApiVersion Current = new(1, "v2");

        private ApiVersion(int id, object value)
            : base(id, value)
        {
        }
    }
}
