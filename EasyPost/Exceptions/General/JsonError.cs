namespace EasyPost.Exceptions.General
{
    public class JsonError : EasyPostError
    {
        internal JsonError(string message) : base(message)
        {
        }
    }

    public class JsonDeserializationError : JsonError
    {
        internal JsonDeserializationError() : base("Failed to deserialize JSON data.")
        {
        }
    }

    public class JsonSerializationError : JsonError
    {
        internal JsonSerializationError() : base("Failed to serialize JSON data.")
        {
        }
    }
}
