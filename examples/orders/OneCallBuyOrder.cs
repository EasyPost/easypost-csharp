using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Dictionary<string, object> firstShipment = new Dictionary<string, object>() {
    { "parcel", new Dictionary<string, object>() {
        { "predefined_package", "FedExBox" }, { "weight", 10.4 } }
    }
};

Dictionary<string, object> secondShipment = new Dictionary<string, object>() {
    { "parcel", new Dictionary<string, object>() {
        { "predefined_package", "FedExBox" }, { "weight", 17.5 } }
    }
};

Order order = await Order.Create(new Dictionary<string, object>() {
  { "carrier_accounts", new Dictionary<string, string>() { { id  } } },
  { "service", NextDayAir },
  { "to_address", toAddress },
  { "from_address", fromAddress },
  { "shipments", new List<Dictionary<string, object>>() { firstShipment, secondShipment } }
});
