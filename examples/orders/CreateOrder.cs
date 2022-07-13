using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Order order = await Order.Create(
    new Dictionary<string, object>() {
        {
            "to_address", new Dictionary<string, object>()
            {
                {
                    "id", "adr_..."
                }
            }
        },
        {
            "from_address", new Dictionary<string, object>()
            {
                {
                    "id", "adr_..."
                }
            }
        },
        { "shipments", new List<Dictionary<string, object>>() { firstShipment, secondShipment } }
    }
);
