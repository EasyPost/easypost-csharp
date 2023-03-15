using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class BatchServiceTests : UnitTest
    {
        public BatchServiceTests() : base("batch_service_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Dictionary<string, object> data = new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } };

            BetaFeatures.Parameters.Batches.Create parameters = Fixtures.Parameters.Batches.Create(data);

            Batch batch = await Client.Batch.Create(parameters);

            Assert.IsType<Batch>(batch);
            Assert.StartsWith("batch_", batch.Id);
            Assert.NotNull(batch.Shipments);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateAndBuy()
        {
            UseVCR("create_and_buy");

            Dictionary<string, object> data = new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } };

            BetaFeatures.Parameters.Batches.Create parameters = Fixtures.Parameters.Batches.Create(data);

            Batch batch = await Client.Batch.CreateAndBuy(parameters);

            Assert.IsType<Batch>(batch);
            Assert.StartsWith("batch_", batch.Id);
            Assert.Equal(1, batch.NumShipments);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Batches.All parameters = Fixtures.Parameters.Batches.All(data);

            BatchCollection batchCollection = await Client.Batch.All(parameters);

            List<Batch> batches = batchCollection.Batches;

            Assert.True(batches.Count <= Fixtures.PageSize);
            foreach (Batch item in batches)
            {
                Assert.IsType<Batch>(item);
            }
        }

        #endregion

        #endregion
    }
}
