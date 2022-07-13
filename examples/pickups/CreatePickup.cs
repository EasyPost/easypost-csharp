using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

parameters = new Dictionary<string, object>() {
  { "is_account_address", false },
  { "address", address },
  { "shipment", shipment },
  { "min_datetime", DateTime.Now },
  { "max_datetime", DateTime.Now },
  { "instructions", "Special pickup instructions" }
};

Pickup pickup = await Pickup.Create(parameters);
