using System.Threading.Tasks;

namespace EasyPost {
    public partial class Tracker {
        public async static Task<Tracker> CreateAsync(string carrier, string trackingCode) {
            return await Task.Run(() => Create(carrier, trackingCode)).ConfigureAwait(false);
        }
    }
}