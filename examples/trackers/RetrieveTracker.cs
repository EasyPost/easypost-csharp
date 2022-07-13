using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Tracker tracker = await Tracker.Retrieve("trk_...");
