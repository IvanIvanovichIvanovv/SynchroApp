using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class used to compare original "source Data" files and directories with clone "replica Data" files and directories.
    /// </summary>
    public class DataComparator
    {
        #region Private Fields

        /// <summary>
        /// <see cref="SynchroApp.Classes.FilesData">FilesData</see> object used to compare with replica.
        /// </summary>
        private FilesData sourceData;

        /// <summary>
        /// <see cref="SynchroApp.Classes.FilesData">FilesData</see> object used to compare with original source.
        /// </summary>
        private FilesData replicaData;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="srcData">original files</param>
        /// <param name="replicaData">cloned files</param>
        public DataComparator(FilesData srcData, FilesData replicaData)
        {
            this.sourceData = srcData;
            this.replicaData = replicaData;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Checks if there are any new directories in source folder which are not present in the replica folder.
        /// </summary>
        /// <returns>List of new directories.</returns>
        public List<string> NewDirectories()
        {
            List<string> directoriesToAdd = new();
            foreach (string dir in this.sourceData.DirectoriesList)
            {
                if (!this.replicaData.DirectoriesList.Contains(dir))
                {
                    directoriesToAdd.Add(dir);
                }
            }
            return directoriesToAdd;
        }

        /// <summary>
        /// Checks if any directories in replica folder are no longer present in the original source folder.
        /// </summary>
        /// <returns>List of directories to remove from replica folder.</returns>
        public List<string> RemovedDirectories()
        {
            List<string> directoriesToRemove = new();
            foreach (string dir in this.replicaData.DirectoriesList)
            {
                if (!this.sourceData.DirectoriesList.Contains(dir))
                {
                    directoriesToRemove.Insert(0, dir);
                }
            }
            return directoriesToRemove;
        }

        /// <summary>
        /// Checks if there are any new files in source folder which are not present in the replica folder.
        /// </summary>
        /// <returns>List of new files.</returns>
        public List<SynchroFile> NewFiles()
        {
            List<SynchroFile> filesToAdd = new();
            foreach (SynchroFile synchroFile in this.sourceData.FilesList)
            {
                if (!this.replicaData.FilesList.Contains(synchroFile))
                {
                    filesToAdd.Add(synchroFile);
                }
            }
            return filesToAdd;
        }

        /// <summary>
        /// Checks if any files in replica folder are no longer present in the original source folder.
        /// </summary>
        /// <returns>List of files to remove from replica folder.</returns>
        public List<SynchroFile> RemovedFiles()
        {
            List<SynchroFile> filesToRemove = new();
            foreach (SynchroFile synchroFile in this.replicaData.FilesList)
            {
                if (!this.sourceData.FilesList.Contains(synchroFile))
                {
                    filesToRemove.Add(synchroFile);
                }
            }
            return filesToRemove;
        }

        #endregion Public Methods
    }
}