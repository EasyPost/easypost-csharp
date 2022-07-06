using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Parcel parcel = await Parcel.Retrieve("prcl_...");
