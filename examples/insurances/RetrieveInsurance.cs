using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Insurance insurance = await Insurance.Retrieve("ins_...");
