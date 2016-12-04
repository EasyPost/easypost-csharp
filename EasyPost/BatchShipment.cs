namespace EasyPost {
    public class BatchShipment : Resource {
        public string id { get; set; }
        public string batch_status { get; set; }
        public string batch_message { get; set; }
        public string tracking_code { get; set;}
    }
}
