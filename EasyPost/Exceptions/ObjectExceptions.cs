using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class PropertyMissing : Exception
    {
        private readonly string _property;

        public override string Message
        {
            get { return $"Missing {_property}"; }
        }

        internal PropertyMissing(string property)
        {
            _property = property;
        }
    }
}
