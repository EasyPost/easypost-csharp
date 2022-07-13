using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

CustomsInfo customsInfo = await CustomsInfo.Retrieve("cstinfo_...");
