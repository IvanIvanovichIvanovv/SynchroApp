using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class used to synchronize files and directories between source and replica folders.
    /// </summary>
    public class DataSynchronizer
    {
        #region Private Fields

        /// <summary>
        /// <see cref="SynchroApp.Classes.FilesData">FilesData</see> object to synchronize with replica.
        /// </summary>
        private FilesData sourceData;

        /// <summary>
        /// <see cref="SynchroApp.Classes.FilesData">FilesData</see> object to synchronize with original source..
        /// </summary>
        private FilesData replicaData;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceData">Original source data.</param>
        /// <param name="replicaData">Replicated data.</param>
        public DataSynchronizer(FilesData sourceData, FilesData replicaData)
        {
            this.sourceData = sourceData;
            this.replicaData = replicaData;
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Creates new directories in replica folder which should be cloned form source folder.
        /// </summary>
        /// <param name="newDirectories">List of directories to create in replica folder.</param>
        private void CreateNewDirectories(List<string> newDirectories)
        {
            foreach (string dir in newDirectories)
            {
                string newDir = this.replicaData.SourceFolder + dir;
                try
                {
                    Directory.CreateDirectory(newDir);
                    Logger.LogDirectoryAdded(newDir);
                }
                catch (Exception e)
                {
                    Logger.LogMessage($"[ERROR] Failed to create new directory {newDir}: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Removes directories from replica folder which are no longer present in original source folder.
        /// </summary>
        /// <param name="directoriesToRemove">List of directories to remove from replica folder.</param>
        private void RemoveDirectories(List<string> directoriesToRemove)
        {
            foreach (string dir in directoriesToRemove)
            {
                string delDir = this.replicaData.SourceFolder + dir;

                try
                {
                    Directory.Delete(delDir, true);

                    Logger.LogDirectoryRemoved(delDir);
                }
                catch (Exception e)
                {
                    Logger.LogMessage($"[ERROR] Failed to remove directory {delDir}: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Copies new files from source to replica folder.
        /// </summary>
        /// <param name="newFiles">List of files to copy from source to replica folder.</param>
        private void CopyNewFiles(List<SynchroFile> newFiles)
        {
            foreach (SynchroFile file in newFiles)
            {
                string copySourceFile = this.sourceData.SourceFolder + file.Path;
                string copyReplicaFile = this.replicaData.SourceFolder + file.Path;
                try
                {
                    File.Copy(copySourceFile, copyReplicaFile);

                    Logger.LogFileAdded(copyReplicaFile);
                }
                catch (Exception e)
                {
                    Logger.LogMessage($"[ERROR] Failed to copy new file {copyReplicaFile}: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Removes files from replica folder which are no longer present in source.
        /// </summary>
        /// <param name="filesToRemove">List of files to remove from replica folder.</param>
        private void RemoveFiles(List<SynchroFile> filesToRemove)
        {
            foreach (SynchroFile file in filesToRemove)
            {
                string remFile = this.replicaData.SourceFolder + file.Path;
                try
                {
                    File.Delete(remFile);
                    Logger.LogFileRemoved(remFile);
                }
                catch (Exception e)
                {
                    Logger.LogMessage($"[ERROR] Failed to remove file {remFile}: {e.Message}");
                }
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Synchronizes directories and files between source and replica folder.
        /// </summary>
        /// <param name="dataComparator">Comparator which provides information about differences between source and replica folder.</param>
        public void SynchronizeData(DataComparator dataComparator)
        {
            this.RemoveFiles(dataComparator.RemovedFiles());
            this.RemoveDirectories(dataComparator.RemovedDirectories());
            this.CreateNewDirectories(dataComparator.NewDirectories());
            this.CopyNewFiles(dataComparator.NewFiles());
        }

        #endregion Public Methods
    }
}