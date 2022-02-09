namespace EasyPost
{
    public class Fee : Resource
    {
        public double amount { get; set; }
        public bool charged { get; set; }
        public bool refunded { get; set; }
        public string type { get; set; }
    }
}
