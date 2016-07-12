namespace EasyPost {
  public class Message : Resource {
    public string type { get; set; }
    public string carrier { get; set; }
    public string message { get; set; }
  }
}