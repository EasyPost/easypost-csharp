using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Pickup pickup = await Pickup.Retrieve("pickup_...");
await pickup.Buy("UPS", pickup.pickup_rates[0].service);
