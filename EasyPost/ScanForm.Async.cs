using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class ScanForm {
        /// <summary>
        /// Get a paginated list of scan forms.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created after this id.
        ///   * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///   * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///   * {"page_size", int} Max size of list. Default to 20.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <returns>Instance of EasyPost.ScanForm</returns>
        public static async Task<ScanFormList> ListAsync(Dictionary<string, object> parameters = null) {
            return await Task.Run(() => List(parameters)).ConfigureAwait(false);
        }
    }
}