using System.Threading.Tasks;

namespace EasyPost.Models.Base
{
    public interface IPaginatedCollection
    {
        /// <summary>
        ///     Get the next page of reports based on the original parameters passed to ReportList.All().
        /// </summary>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        public Task<IPaginatedCollection> Next();
    }
}
