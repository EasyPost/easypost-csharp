using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace EasyPost.Utilities
{
    public static class Cryptography
    {
        private static readonly uint[] Lookup32 = CreateLookup32();

        /// <summary>
        ///     Convert a string to a byte array using a specific encoding (defaults to UTF-8)
        /// </summary>
        /// <param name="str">String to convert to byte array.</param>
        /// <param name="encoding">Encoding to use. Default: UTF-8</param>
        /// <returns>Byte array</returns>
        public static byte[] AsByteArray(this string str, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            return encoding.GetBytes(str);
        }

        /// <summary>
        ///     Convert a byte array to a hex string.
        /// </summary>
        /// <param name="bytes">Byte array to convert to hex string.</param>
        /// <returns>Hex string</returns>
        public static string AsHexString(this byte[] bytes)
        {
            // Fastest safe way to convert a byte array to hex string,
            // per https://stackoverflow.com/a/624379/13343799

            uint[] lookup32 = Lookup32;
            char[] result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                uint val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }

            return new string(result).ToLower();
        }

        /// <summary>
        ///     Convert a string to a hex string using a specific encoding (defaults to UTF-8)
        /// </summary>
        /// <param name="str">String to convert to hex string.</param>
        /// <param name="encoding">Encoding to use. Default: UTF-8</param>
        /// <returns>Hex string</returns>
        public static string AsHexString(this string str, Encoding? encoding = null)
        {
            byte[] bytes = str.AsByteArray(encoding);

            return bytes.AsHexString();
        }

        /// <summary>
        ///     Convert a byte array to a string using a specific encoding (defaults to UTF-8)
        /// </summary>
        /// <param name="bytes">Byte array to convert to string.</param>
        /// <param name="encoding">Encoding to use. Default: UTF-8</param>
        /// <returns>String</returns>
        public static string AsString(this byte[] bytes, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            return encoding.GetString(bytes);
        }

        /// <summary>
        ///     Calculate the HMAC-SHA256 hex digest of a byte array.
        /// </summary>
        /// <param name="data">Data to calculate hex digest for.</param>
        /// <param name="secret">Key used to calculate data hex digest.</param>
        /// <param name="normalizationForm">Normalization type to use when normalizing key. Default: No normalization.</param>
        /// <returns>Hex digest of data.</returns>
        public static string CalculateHMACSHA256HexDigest(this byte[] data, string secret, NormalizationForm? normalizationForm = null)
        {
            if (normalizationForm != null)
            {
                secret = secret.Normalize(normalizationForm.Value);
            }

            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);

            using HMACSHA256 hmac = new HMACSHA256(keyBytes);
            byte[] hash = hmac.ComputeHash(data);
            return hash.AsHexString();
        }

        /// <summary>
        ///     Calculate the HMAC-SHA256 hex digest of a string.
        /// </summary>
        /// <param name="data">Data to calculate hex digest for.</param>
        /// <param name="key">Key used to calculate data hex digest.</param>
        /// <param name="normalizationForm">Normalization type to use when normalizing key. Default: No normalization.</param>
        /// <returns>Hex digest of data.</returns>
        public static string CalculateHMACSHA256HexDigest(this string data, string key, NormalizationForm? normalizationForm = null)
        {
            byte[] dataBytes = data.Replace("\n", "").Replace(" ", string.Empty).AsByteArray();

            return dataBytes.CalculateHMACSHA256HexDigest(key, normalizationForm);
        }

        /// <summary>
        ///     Check whether two signatures match. This is safe against timing attacks.
        /// </summary>
        /// <param name="signature1">First signature.</param>
        /// <param name="signature2">Second signature.</param>
        /// <returns>Whether the two signatures match.</returns>
        public static bool SignaturesMatch(byte[] signature1, byte[] signature2)
        {
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

        /// <summary>
        ///     Check whether two signatures match. This is safe against timing attacks.
        /// </summary>
        /// <param name="signature1">First signature.</param>
        /// <param name="signature2">Second signature.</param>
        /// <returns>Whether the two signatures match.</returns>
        public static bool SignaturesMatch(string signature1, string signature2)
        {
            signature1 = signature1.ToLower();
            signature2 = signature2.ToLower();

            byte[] signatureBytes1 = signature1.AsByteArray();
            byte[] signatureBytes2 = signature2.AsByteArray();

            return SignaturesMatch(signatureBytes1, signatureBytes2);
        }

        /// <summary>
        ///     Construct a lookup table of hex values.
        /// </summary>
        /// <returns>Lookup table of hex values</returns>
        private static uint[] CreateLookup32()
        {
            uint[] result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }

            return result;
        }
    }
}
