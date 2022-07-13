using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Batch batch = await Batch.Create(new Dictionary<string, object>() {
  { "reference", "MyReference" },
  { "shipments", new List<Dictionary<string, object>>() {
    new Dictionary<string, object>() { "id", "shp_..." }
  } }
});
