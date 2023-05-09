using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EasyPost.Utilities
{
    /// <summary>
    ///     Cryptography-related utility methods.
    /// </summary>
#pragma warning disable CA1724 // Naming conflicts with System.Security.Cryptography.Cryptography
    public static class Cryptography
    {
        /// <summary>
        ///     Get a lookup table of hex values.
        /// </summary>
        private static readonly uint[] Lookup32 = CreateLookup32();

        /// <summary>
        ///     Convert a byte array to a string using a specific encoding.
        /// </summary>
        /// <param name="bytes">Byte array to convert to string.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use. Default: <see cref="Encoding.UTF8"/></param>
        /// <returns>String equivalent of input byte array.</returns>
        public static string AsString(this byte[] bytes, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            return encoding.GetString(bytes);
        }

        /// <summary>
        ///     Convert a string to a byte array using a specific encoding.
        /// </summary>
        /// <param name="str">String to convert to byte array.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use. Default: <see cref="Encoding.UTF8"/></param>
        /// <returns>Byte array equivalent of input string.</returns>
        public static byte[] AsByteArray(this string str, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            return encoding.GetBytes(str);
        }

        /// <summary>
        ///     Convert a byte array to a hex string.
        /// </summary>
        /// <param name="bytes">Byte array to convert to hex string.</param>
        /// <returns>Hex string equivalent of input byte array.</returns>
        private static string AsHexString(this IReadOnlyList<byte> bytes)
        {
            // Fastest safe way to convert a byte array to hex string,
            // per https://stackoverflow.com/a/624379/13343799
            uint[] lookup32 = Lookup32;
            char[] result = new char[bytes.Count * 2];
            for (int i = 0; i < bytes.Count; i++)
            {
                uint val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[(2 * i) + 1] = (char)(val >> 16);
            }

            return new string(result).ToLowerInvariant();
        }

        /// <summary>
        ///     Convert a string to a hex string using a specific encoding.
        /// </summary>
        /// <param name="str">String to convert to hex string.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use. Default: <see cref="Encoding.UTF8"/></param>
        /// <returns>Hex string equivalent of input string.</returns>
        public static string AsHexString(this string str, Encoding? encoding = null)
        {
            byte[] bytes = str.AsByteArray(encoding);

            return bytes.AsHexString();
        }

        /// <summary>
        ///     Calculate the HMAC-SHA256 hex digest of a byte array.
        /// </summary>
        /// <param name="data">Data to calculate hex digest for.</param>
        /// <param name="secret">Key used to calculate data hex digest.</param>
        /// <param name="normalizationForm"><see cref="NormalizationForm"/> to use when normalizing key. Default: No normalization.</param>
        /// <returns>Hex digest of input data.</returns>
        // ReSharper disable once IdentifierTypo
        // ReSharper disable once InconsistentNaming
        public static string CalculateHMACSHA256HexDigest(this byte[] data, string secret, NormalizationForm? normalizationForm = null)
        {
            if (normalizationForm != null)
            {
                secret = secret.Normalize(normalizationForm.Value);
            }

            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);

            using HMACSHA256 hmac = new(keyBytes);
            byte[] hash = hmac.ComputeHash(data);

            return hash.AsHexString();
        }

        /// <summary>
        ///     Check whether two signatures match. This is safe against timing attacks.
        /// </summary>
        /// <param name="signature1">First signature.</param>
        /// <param name="signature2">Second signature.</param>
        /// <returns><c>true</c> if the two signatures match, <c>false</c> otherwise.</returns>
        public static bool SignaturesMatch(byte[] signature1, byte[]? signature2)
        {
            // short-circuit if second signature is null
            if (signature2 == null)
            {
                return false;
            }

            // short-circuit if signatures are not the same length
            if (signature1.Length != signature2.Length)
            {
                return false;
            }

            bool err = false;
            for (int i = 0; i < signature1.Length; i++)
            {
                if (signature1[i] != signature2[i])
                {
                    err = true;
                }
            }

            return !err;
        }

        /// <inheritdoc cref="SignaturesMatch(byte[],byte[])"/>
        public static bool SignaturesMatch(string signature1, string? signature2)
        {
            byte[] signatureBytes1 = signature1.AsByteArray();
            byte[]? signatureBytes2 = signature2?.AsByteArray();

            return SignaturesMatch(signatureBytes1, signatureBytes2);
        }

        /// <summary>
        ///     Construct a lookup table of hex values.
        /// </summary>
        /// <returns>Lookup table of hex values.</returns>
        private static uint[] CreateLookup32()
        {
            uint[] result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
#pragma warning disable CA1305
                string s = i.ToString("X2");
#pragma warning restore CA1305

                // ReSharper disable once RedundantCast
                // ReSharper disable once ArrangeRedundantParentheses
#pragma warning disable IDE0004
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
#pragma warning restore IDE0004
            }

            return result;
        }
    }
#pragma warning restore CA1724 // Naming conflicts with System.Security.Cryptography.Cryptography
}
