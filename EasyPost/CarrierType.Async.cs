using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public partial class CarrierType {
        public static async Task<List<CarrierType>> AllAsync() {
            return await Task.Run(() => All()).ConfigureAwait(false);
        }
    }
}