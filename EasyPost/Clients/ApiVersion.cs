using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyPost.Clients
{
    /// <summary>
    ///     Available EasyPost API versions.
    /// </summary>
    public enum ApiVersion
    {
        V2,
        Latest,
        Beta
    }

    /// <summary>
    ///     Additional details for each EasyPost API version.
    /// </summary>
    internal class ApiVersionDetails : IComparable
    {
        // ApiVersion enum above needed since attributes can only take in enums (due to compilation)
        // We derive these "fake" enums below from the real enum
        // We need a matching "enum" below for each ApiVersion enum.
        private static readonly List<ApiVersionDetails> ApiVersionDetailsList = new List<ApiVersionDetails>
        {
            {
                new ApiVersionDetails(ApiVersion.Beta, "beta", 0, "Beta")
            },
            {
                new ApiVersionDetails(ApiVersion.V2, "v2", 2, "V2")
            },
            {
                new ApiVersionDetails(ApiVersion.Latest, "v2", 2, "Latest")
            },
        };
        internal ApiVersion ApiVersionEnum { get; }

        internal string Name { get; }
        internal int Number { get; }
        internal string Prefix { get; }

        private ApiVersionDetails(ApiVersion apiVersionEnum, string prefix, int number, string? name = null)
        {
            Name = name ?? apiVersionEnum.ToString();
            Prefix = prefix;
            Number = number;
            ApiVersionEnum = apiVersionEnum;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
            {
                return 1;
            }

            ApiVersionDetails other = (obj as ApiVersionDetails)!; // avoid double casting
            if (other == null!)
            {
                throw new ArgumentException("An ApiVersionDetails object is required for comparison", nameof(obj));
            }

            return CompareTo(other);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((ApiVersionDetails)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Prefix.GetHashCode();
                hashCode = (hashCode * 397) ^ Number;
                hashCode = (hashCode * 397) ^ (int)ApiVersionEnum;
                return hashCode;
            }
        }

        private int CompareTo(ApiVersionDetails? other)
        {
            if (other is null)
            {
                return 1;
            }

            if (Number < other.Number)
            {
                return -1;
            }

            if (Number > other.Number)
            {
                return 1;
            }

            return 0;
        }

        private bool Equals(ApiVersionDetails other) => Name == other.Name && Prefix == other.Prefix && Number == other.Number && ApiVersionEnum == other.ApiVersionEnum;

        public static bool operator ==(ApiVersionDetails left, ApiVersionDetails right)
        {
            return left.CompareTo(right) == 0;
        }

        public static bool operator >(ApiVersionDetails left, ApiVersionDetails right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ApiVersionDetails left, ApiVersionDetails right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator !=(ApiVersionDetails left, ApiVersionDetails right)
        {
            return left.CompareTo(right) != 0;
        }

        public static bool operator <(ApiVersionDetails left, ApiVersionDetails right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ApiVersionDetails left, ApiVersionDetails right)
        {
            return left.CompareTo(right) <= 0;
        }

        internal static ApiVersionDetails GetApiVersionDetails(ApiVersion apiVersionEnum)
        {
            ApiVersionDetails? matchingApiVersionDetails = ApiVersionDetailsList.FirstOrDefault(apiVersionDetails => apiVersionDetails.ApiVersionEnum == apiVersionEnum);

            if (matchingApiVersionDetails == null!)
            {
                throw new ArgumentOutOfRangeException(nameof(apiVersionEnum), apiVersionEnum, null);
            }

            return matchingApiVersionDetails;
        }
    }
}
