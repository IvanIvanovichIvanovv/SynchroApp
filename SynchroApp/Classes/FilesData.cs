using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class to store directories and file information under specified path.
    /// </summary>
    public class FilesData
    {
        #region Public Properties

        /// <summary>
        /// Path to folder containing data to store.
        /// </summary>
        public string SourceFolder { get; private set; } = String.Empty;

        /// <summary>
        /// List of directories under source folder and all subdirectories.
        /// </summary>
        public List<string> DirectoriesList { get; private set; } = new();

        /// <summary>
        /// List of files under source foler and all subdirectories.
        /// </summary>
        public List<SynchroFile> FilesList { get; private set; } = new();

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceFolder">Path to data source folder.</param>
        public FilesData(string sourceFolder)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Logger.LogMessage($"[ERROR] Specified path does not exist.");
                System.Environment.Exit(2);
            }
            SourceFolder = sourceFolder;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Clears directories and files data.
        /// </summary>
        public void ClearData()
        {
            DirectoriesList.Clear();
            FilesList.Clear();
        }

        #endregion Public Methods
    }
}