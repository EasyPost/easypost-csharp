using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class InsuranceTests : UnitTest
    {
        public InsuranceTests() : base("insurance")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRefresh()
        {
            UseVCR("refresh");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            Dictionary<string, object> parameters = Fixtures.BasicInsurance;
            parameters.Add("tracking_code", shipment.TrackingCode);

            Insurance insurance = await Client.Insurance.Create(parameters);

            Insurance refreshedInsurance = await insurance.Refresh();

            Assert.Equal(insurance.Id, refreshedInsurance.Id);
        }

        #endregion

        #endregion
    }
}
