using System;
using System.IO;
using System.Security.Cryptography;
using Telegram.Bot.Types;

namespace PatzminiHD.CSLib.Math
{
    /// <summary>
    /// Contains Crypto related methods, like for computing hashes
    /// </summary>
    public class Crypto
    {
        #region MD5
        /// <summary>
        /// Compute the MD5 Hash of a byte[]
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The MD5 Hash</returns>
        public static byte[] GetMD5Hash(byte[] data)
        {
            return MD5.Create().ComputeHash(data);
        }
        /// <summary>
        /// Compute the MD5 Hash of a file
        /// </summary>
        /// <param name="filename">The path to the file</param>
        /// <returns>The MD5 Hash</returns>
        public static byte[] GetMD5Hash(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                return MD5.Create().ComputeHash(stream);
            }
        }
        /// <summary>
        /// Get the string representation of the MD5 Hash of a byte[] (all characters are lowercase)
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The string representation of the MD5 Hash</returns>
        public static string GetMD5HashString(byte[] data)
        {
            return BitConverter.ToString(GetMD5Hash(data)).Replace("-", "").ToLower();
        }/// <summary>
        /// Get the string representation of the MD5 Hash of a file (all characters are lowercase)
        /// </summary>
        /// <param name="filename">The path to the file</param>
        /// <returns>The string representation of the MD5 Hash</returns>
        public static string GetMD5HashString(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var hash = MD5.Create().ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        #endregion
        #region SHA256
        /// <summary>
        /// Compute the SHA256 Hash of a byte[]
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The SHA256 Hash</returns>
        public static byte[] GetSHA256Hash(byte[] data)
        {
            return SHA256.Create().ComputeHash(data);
        }
        /// <summary>
        /// Compute the SHA256 Hash of a file
        /// </summary>
        /// <param name="filename">The path to the file</param>
        /// <returns>The SHA256 Hash</returns>
        public static byte[] GetSHA256Hash(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                return SHA256.Create().ComputeHash(stream);
            }
        }
        /// <summary>
        /// Get the string representation of the SHA256 Hash of a byte[] (all characters are lowercase)
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The string representation of the SHA256 Hash</returns>
        public static string GetSHA256HashString(byte[] data)
        {
            return BitConverter.ToString(GetSHA256Hash(data)).Replace("-", "").ToLower();
        }/// <summary>
         /// Get the string representation of the SHA256 Hash of a file (all characters are lowercase)
         /// </summary>
         /// <param name="filename">The path to the file</param>
         /// <returns>The string representation of the SHA256 Hash</returns>
        public static string GetSHA256HashString(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var hash = SHA256.Create().ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        #endregion
        #region SHA512
        /// <summary>
        /// Compute the SHA512 Hash of a byte[]
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The SHA512 Hash</returns>
        public static byte[] GetSHA512Hash(byte[] data)
        {
            return SHA512.Create().ComputeHash(data);
        }
        /// <summary>
        /// Compute the SHA512 Hash of a file
        /// </summary>
        /// <param name="filename">The path to the file</param>
        /// <returns>The SHA512 Hash</returns>
        public static byte[] GetSHA512Hash(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                return SHA512.Create().ComputeHash(stream);
            }
        }
        /// <summary>
        /// Get the string representation of the SHA512 Hash of a byte[] (all characters are lowercase)
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The string representation of the SHA512 Hash</returns>
        public static string GetSHA512HashString(byte[] data)
        {
            return BitConverter.ToString(GetSHA512Hash(data)).Replace("-", "").ToLower();
        }/// <summary>
         /// Get the string representation of the SHA512 Hash of a file (all characters are lowercase)
         /// </summary>
         /// <param name="filename">The path to the file</param>
         /// <returns>The string representation of the SHA512 Hash</returns>
        public static string GetSHA512HashString(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var hash = SHA512.Create().ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        #endregion
    }
}
