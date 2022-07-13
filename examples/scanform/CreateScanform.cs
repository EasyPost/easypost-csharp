using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Shipment shipment = await Shipment.Create("...");
List<Shipment> shipments = new List<Shipment>() {
    shipment
};
ScanForm scanForm = await ScanForm.Create(shipments);
