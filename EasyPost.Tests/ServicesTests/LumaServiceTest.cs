using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class LumaServiceTests : UnitTest
    {
        public LumaServiceTests() : base("luma_service")
        {
        }

        [Fact]
        [Testing.Function]
        public async Task TestGetPromise()
        {
            UseVCR("get_promise");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;
            shipmentData["ruleset_name"] = Fixtures.LumaRulesetName;
            shipmentData["planned_ship_date"] = Fixtures.LumaPlannedShipDate;

            LumaInfo response = await Client.Luma.GetPromise(shipmentData);

            Assert.NotNull(response.LumaSelectedRate);
        }
    }
}
