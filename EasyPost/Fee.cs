namespace EasyPost
{
    public class Fee : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public string type { get; set; }
        public double amount { get; set; }
        public bool charged { get; set; }
        public bool refunded { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}