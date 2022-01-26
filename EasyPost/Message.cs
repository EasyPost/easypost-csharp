namespace EasyPost {
  public class Message : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public string type { get; set; }
        public string carrier { get; set; }
        public string message { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}