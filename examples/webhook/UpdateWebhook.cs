using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Webhook webhook = await Webhook.Retrieve("hook_...");
await webhook.Update();
