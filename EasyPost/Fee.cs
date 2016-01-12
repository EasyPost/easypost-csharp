namespace EasyPost {
    public class Fee : IResource {
        public double amount { get; set; }
        public bool charged { get; set; }
        public bool refunded { get; set; }
    }
}
