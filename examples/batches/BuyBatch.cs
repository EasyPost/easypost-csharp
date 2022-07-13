using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Batch batch = await Batch.Retrieve("batch_...");
await batch.Buy();
