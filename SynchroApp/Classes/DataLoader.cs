using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class used to load directories and files data.
    /// </summary>
    public class DataLoader
    {
        /// <summary>
        /// <see cref="SynchroApp.Classes.FilesData">FilesData</see> object to store folder data.
        /// </summary>

        #region Private Fields

        private FilesData filesData;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="filesData">Object to store information about files and directories.</param>
        public DataLoader(FilesData filesData)
        {
            if (Directory.Exists(filesData.SourceFolder))
            {
                this.filesData = filesData;
            }
            else
            {
                Logger.LogMessage($"[ERROR] Specified path is unavailable: {filesData.SourceFolder}");
                System.Environment.Exit(2);
            }
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Loads files and directories information and stores them in <see cref="SynchroApp.Classes.FilesData">FilesData</see> object.
        /// </summary>
        /// <param name="srcPath">Source data folder to load.</param>
        /// <param name="filesData">FilesData object to store files and directories information.</param>
        private void LoadData(string srcPath, FilesData filesData)
        {
            string[] fileEntries = Directory.GetFiles(srcPath);
            foreach (string fileName in fileEntries)
            {
                filesData.FilesList.Add(new SynchroFile(fileName.Replace(filesData.SourceFolder, ""), Helpers.GetHash(fileName)));
            }

            string[] subdirectoryEntries = Directory.GetDirectories(srcPath);

            foreach (string subdirectory in subdirectoryEntries)
            {
                filesData.DirectoriesList.Add(subdirectory.Replace(filesData.SourceFolder, ""));
                LoadData(subdirectory, filesData);
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Loads files an directories information and stores them <see cref="SynchroApp.Classes.FilesData">FilesData</see> object assigned in constructor.
        /// </summary>
        /// <returns>True if operation was succesful, false if error occured.</returns>
        public bool LoadData()
        {
            try
            {
                LoadData(this.filesData.SourceFolder, this.filesData);
            }
            catch (Exception ex)
            {
                Logger.LogMessage("[ERROR] Cannot load data");
                return false;
            }
            return true;
        }

        #endregion Public Methods
    }
}