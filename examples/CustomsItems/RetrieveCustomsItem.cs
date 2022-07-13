using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

CustomsItem customsItem = await CustomsItem.Retrieve("cstitem_...");
