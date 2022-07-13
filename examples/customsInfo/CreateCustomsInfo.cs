using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Dictionary<string, object> item = new Dictionary<string, object>() {
  { "description", "TShirt" }, { "quantity", 1 }, { "weight", 8 }, { "origin_country", "US" }
};

CustomsInfo info = await CustomsInfo.Create(new Dictionary<string, object>() {
  { "customs_certify", true }, { "eel_pfc", "NOEEI 30.37(a)" },
  { "customs_items", new List<Dictionary<string, object>>() { item } }
});
