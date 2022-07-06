using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

var listParams = new Dictionary<string, object>() {
  { "page_size", 2 }, { "start_datetime", "2016-01-02T08:50:00Z" }
};

ShipmentCollection shipmentCollection = await Shipment.All(listParams);
ShipmentCollection nextShipmentCollection = await shipmentCollection.Next();
