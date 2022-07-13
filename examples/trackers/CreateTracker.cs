using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Tracker tracker = await Tracker.Create("USPS", "9400110898825022579493");
