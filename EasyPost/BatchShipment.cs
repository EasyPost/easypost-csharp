namespace EasyPost {
    public class BatchShipment : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public string id { get; set; }
        public string batch_status { get; set; }
        public string batch_message { get; set; }
        public string tracking_code { get; set;}
#pragma warning restore IDE1006 // Naming Styles
    }
}
