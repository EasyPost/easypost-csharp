namespace EasyPost.Exceptions.General
{
    public class ValidationError : EasyPostError
    {
        protected ValidationError(string message) : base(message)
        {
        }
    }
}
