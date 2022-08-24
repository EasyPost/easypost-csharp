using System;

namespace EasyPost.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum |
                    AttributeTargets.Interface | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Delegate,
        Inherited = false)]
    public sealed class DeprecationDetails : Attribute
    {
        public string? DeprecatedInVersion { get; }

        public string Details
        {
            get
            {
                string message = $"{Why}";
                if (Replacement != null)
                {
                    message += $"\n\nUse {Replacement.FullName} instead.";
                }

                message += $"\n\nDeprecated in version {DeprecatedInVersion} and will be removed in version {RemoveInVersion}.";
                return message;
            }
        }

        public string? RemoveInVersion { get; }

        public Type? Replacement { get; }
        public string? Why { get; }

        public DeprecationDetails(string why, string deprecatedInVersion, string removeInVersion, Type? replacement = null)
        {
            Why = why;
            DeprecatedInVersion = deprecatedInVersion;
            RemoveInVersion = removeInVersion;
            Replacement = replacement;
        }
    }
}
