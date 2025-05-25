using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchroApp.Classes;
using System.IO;

[TestClass]
public class FilesDataTests
{
    #region Private Fields

    private string tempDir;

    #endregion Private Fields

    #region Public Methods

    [TestInitialize]
    public void Setup()
    {
        this.tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(this.tempDir);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (Directory.Exists(this.tempDir))
        {
            Directory.Delete(this.tempDir, true);
        }
    }

    [TestMethod]
    public void Constructor_Initializes_WhenPathExists()
    {
        FilesData data = new FilesData(this.tempDir);

        Assert.AreEqual(this.tempDir, data.SourceFolder);
    }

    [TestMethod]
    public void ClearData_EmptiesFileAndDirLists()
    {
        FilesData data = new FilesData(this.tempDir);
        data.FilesList.Add(new SynchroFile("file", new byte[0]));
        data.DirectoriesList.Add("subdir");

        data.ClearData();

        Assert.AreEqual(0, data.FilesList.Count);
        Assert.AreEqual(0, data.DirectoriesList.Count);
    }

    #endregion Public Methods
}