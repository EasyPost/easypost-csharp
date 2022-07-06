using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

listParams = new Dictionary<string, object>() {"page_size", 5};

AddressCollection addressCollection = await Address.All(listParams);
