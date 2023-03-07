using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class BatchServiceTests : UnitTest
    {
        public BatchServiceTests() : base("batch_service")
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

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } });

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

            Batch batch = await Client.Batch.CreateAndBuy(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.OneCallBuyShipment } } });

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

            BatchCollection batchCollection = await Client.Batch.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Batch> batches = batchCollection.Batches;

            Assert.True(batches.Count <= Fixtures.PageSize);
            foreach (Batch item in batches)
            {
                Assert.IsType<Batch>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Batch batch = await Client.Batch.Create(new Dictionary<string, object> { { "shipments", new List<Dictionary<string, object>> { Fixtures.BasicShipment } } });

            Batch retrievedBatch = await Client.Batch.Retrieve(batch.Id);

            Assert.IsType<Batch>(retrievedBatch);
            // Must compare IDs since elements of batch (i.e. status) may be different
            Assert.Equal(batch.Id, retrievedBatch.Id);
        }

        #endregion

        #endregion
    }
}
