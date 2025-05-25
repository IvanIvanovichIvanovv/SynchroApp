using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class used to store methods used various situations.
    /// </summary>
    public static class Helpers
    {
        #region Public Methods

        /// <summary>
        /// Gets the hash of a file under specified path.
        /// </summary>
        /// <param name="fileName">Path to file from which we get the hash.</param>
        /// <returns>Hash in an byte array</returns>
        public static byte[] GetHash(string fileName)
        {
            MD5 md5 = MD5.Create();
            FileStream stream = File.OpenRead(fileName);
            byte[] hash = md5.ComputeHash(stream);
            stream.Close();
            return hash;
        }

        public static string ConvertHashToString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        #endregion Public Methods
    }
}