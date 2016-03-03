namespace EasyPost {
  public class Message : IResource {
    public string type { get; set; }
    public string carrier { get; set; }
    public string message { get; set; }
  }
}