using System.Threading.Tasks;

namespace EasyPost {
    public partial class ScanFormList {
        /// <summary>
        /// Get the next page of scan forms based on the original parameters passed to ScanForm.List().
        /// </summary>
        /// <returns>A new EasyPost.ScanFormList instance.</returns>
        public async Task<ScanFormList> NextAsync() {
            return await Task.Run(() => Next()).ConfigureAwait(false);
        }
    }
}