using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Parcel parcel = await Parcel.Create(new Dictionary<string, object>() {
  { "length", 10 }, { "width", 20 }, { "height", 5 }, { "weight", 1.8 }
});
