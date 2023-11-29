using EasyPost.Models.API;
using EasyPost.Models.Shared;
using Xunit;

namespace EasyPost.Integration;

public class Extensions
{
    /// <summary>
    ///     Test that an end-user can inherit and override the <see cref="PaginatedCollection{T}.BuildNextPageParameters{TParameters}(IEnumerable{T}, int?)"/> method.
    ///     If this test can be compiled, then the <see cref="PaginatedCollection{T}.BuildNextPageParameters{TParameters}(IEnumerable{T}, int?)"/> method is publicly accessible.
    /// </summary>
    public class CustomPaginatedCollection : PaginatedCollection<Tracker> // Using Tracker as a placeholder for any type
    {
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Tracker> entries, int? pageSize = null) => throw new NotImplementedException();
    }

    /// <summary>
    ///     This test simply exists to ensure this file is compiled when the test suite is run.
    /// </summary>
    [Fact]
    public void Compile()
    {
        Assert.True(true);
    }
}
