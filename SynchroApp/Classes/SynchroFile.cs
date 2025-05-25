using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class used to store file path and a hash.
    /// </summary>
    public class SynchroFile : IEquatable<SynchroFile>
    {
        #region Private Fields

        /// <summary>
        /// Path to a file.
        /// </summary>
        private string path;

        /// <summary>
        /// Hash in a readable string format.
        /// </summary>
        private string hash;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Return a file path.
        /// </summary>
        public string Path => this.path;

        public string Hash => this.hash;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="path">Path to a file.</param>
        /// <param name="hash">File hash to convert to string and store it.</param>
        public SynchroFile(string path, byte[] hash)
        {
            this.path = path;
            this.hash = Helpers.ConvertHashToString(hash);
        }

        #endregion Public Constructors

        #region Public Methods

        public bool Equals(SynchroFile? other)
        {
            return this.path == other.path && this.hash == other.hash;
        }

        public override string ToString()
        {
            return "Path:" + this.path + " Hash:" + this.hash;
        }

        #endregion Public Methods
    }
}