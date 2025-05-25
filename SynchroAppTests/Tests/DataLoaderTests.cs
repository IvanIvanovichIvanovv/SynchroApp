using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchroApp.Classes;
using System.IO;

[TestClass]
public class DataLoaderTests
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
        File.WriteAllText(Path.Combine(this.tempDir, "test.txt"), "content");
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
    public void LoadData_ReturnsTrue_WhenFolderIsValid()
    {
        FilesData filesData = new FilesData(this.tempDir);
        DataLoader loader = new DataLoader(filesData);

        bool result = loader.LoadData();

        Assert.IsTrue(result);
        Assert.AreEqual(1, filesData.FilesList.Count);
    }

    #endregion Public Methods
}