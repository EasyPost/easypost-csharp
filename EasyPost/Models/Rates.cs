using System.Threading.Tasks;
using EasyPost.Interfaces;

namespace EasyPost.Models
{
    public class Rates : Service
    {
        public Rates(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Retrieve a Rate from its id.
        /// </summary>
        /// <param name="id">String representing a Rate. Starts with "rate_".</param>
        /// <returns>EasyPost.Rate instance.</returns>
        public async Task<Rate> Retrieve(string id)
        {
            return await Get<Rate>($"rates/{id}");
        }
    }
}
