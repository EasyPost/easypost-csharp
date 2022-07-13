using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

CustomsItem item = await CustomsItem.Create(new Dictionary<string, object>() {
  { "description", "TShirt" },
  { "quantity", 1 },
  { "weight", 8 },
  { "value", 10.0 },
  { "origin_country", "us" },
  { "hs_tariff_number", "123456" }
});
