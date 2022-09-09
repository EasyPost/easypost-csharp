// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class JsonError : EasyPostError
    {
        protected JsonError(string message) : base(message)
        {
        }
    }

    public class JsonDeserializationError : JsonError
    {
        public JsonDeserializationError() : base("Failed to deserialize JSON data.")
        {
        }
    }

    public class JsonSerializationError : JsonError
    {
        public JsonSerializationError() : base("Failed to serialize JSON data.")
        {
        }
    }
}
